using AutoMapper;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.Resolvers.SpecializedResolvers;
using Castle.Windsor;
using Castle.Windsor.MsDependencyInjection;
using FluentValidation;
using MediatR;
using MicroserviceTraining.Framework.Behaviors;
using MicroserviceTraining.Framework.ExceptionMiddleware;
using MicroserviceTraining.Framework.IOC;
using MicroserviceTraining.Framework.IOC.Filters;
using MicroserviceTraining.Framework.Middleware;
using MicroserviceTraining.Framework.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using StackExchange.Redis;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace MicroserviceTraining.Framework.ConfigurationExtensions
{
    public static class ConfigurationExtensions
    {
        public static IServiceCollection AddWindsor(this IServiceCollection services)
        {
            services.AddSingleton<IWindsorContainer>(IocFacility.Container);

            return services;
        }

        public static IWindsorContainer AddMediaTR(this IWindsorContainer container, string assemblyName = "")
        {
            var assembly = Assembly.Load(assemblyName);

            container.Kernel.Resolver.AddSubResolver(new CollectionResolver(container.Kernel));
            container.Kernel.AddHandlersFilter(new ContravariantFilter());

            container.Register(Classes.FromAssembly(assembly).BasedOn(typeof(IRequestHandler<,>)).WithServiceAllInterfaces().LifestyleTransient());
            container.Register(Classes.FromAssembly(assembly).BasedOn(typeof(INotificationHandler<>)).WithServiceAllInterfaces().LifestyleTransient());
            container.Register(Component.For<IMediator>().ImplementedBy<Mediator>());
            container.Register(Component.For<ServiceFactory>().UsingFactoryMethod<ServiceFactory>(k => (type =>
            {
                var enumerableType = type
                    .GetInterfaces()
                    .Concat(new[] { type })
                    .FirstOrDefault(t => t.IsGenericType && t.GetGenericTypeDefinition() == typeof(IEnumerable<>));

                return enumerableType != null ? k.ResolveAll(enumerableType.GetGenericArguments()[0]) : k.Resolve(type);
            })));

            //register validators
            AssemblyScanner.FindValidatorsInAssembly(assembly)
                                .ForEach(item => container.Register(Component.For(item.InterfaceType).ImplementedBy(item.ValidatorType).LifestyleTransient()));

            container.Register(Component.For(typeof(IPipelineBehavior<,>)).ImplementedBy(typeof(HandlerLogging<,>)).LifestyleTransient());
            container.Register(Component.For(typeof(IPipelineBehavior<,>)).ImplementedBy(typeof(HandlerValidation<,>)).LifestyleTransient());

            return container;
        }

        public static IHostBuilder ConfigureAll<TStartup>(this IHostBuilder host) where TStartup : class
        {
            host.UseServiceProviderFactory(new WindsorServiceProviderFactory());

            return host;
        }

        public static IApplicationBuilder ConfigureAll(this IApplicationBuilder applicationBuilder)
        {
            applicationBuilder.UseMiddleware<ExceptionHandler>();
            applicationBuilder.UseMiddleware<ApiRequestMiddleware>();

            return applicationBuilder;
        }

        public static IWindsorContainer AddAutoMapper(this IWindsorContainer container, string assemblyName = "")
        {
            var assembly = Assembly.Load(assemblyName);

            container.Register(Classes.FromAssemblyInThisApplication(assembly).BasedOn<Profile>().WithServiceBase());
            container.Register(Component.For<AutoMapper.IConfigurationProvider>().UsingFactoryMethod(kernel =>
            {
                return new MapperConfiguration(configuration =>
                {
                    kernel.ResolveAll<Profile>().ToList().ForEach(configuration.AddProfile);
                });
            }).LifestyleSingleton());

            container.Register(
                Component.For<IMapper>().UsingFactoryMethod(kernel =>
                    new Mapper(kernel.Resolve<AutoMapper.IConfigurationProvider>(), kernel.Resolve)));
            return container;
        }

        public static IServiceCollection AddRedis(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddStackExchangeRedisCache(options =>
            {
                RedisSettings redisSettings = new RedisSettings();
                configuration.GetSection("RedisSettings").Bind(redisSettings);
                var configurationOptions = ConfigurationOptions.Parse(redisSettings.ConnectionString, true);
                options.ConfigurationOptions = configurationOptions;
            });

            return services;
        }

        public static IWindsorContainer AddMySqlContext<TDbContext>(this IWindsorContainer container, IConfiguration configuration) where TDbContext: DbContext
        {
            string connString = configuration.GetConnectionString("MySqlConnStr");

            var dbContextOptions = new DbContextOptionsBuilder<TDbContext>()
                                                                .UseMySql(connString, ServerVersion.AutoDetect(connString))
                                                                .UseQueryTrackingBehavior(QueryTrackingBehavior.TrackAll).Options;

            container.Register(Component.For<TDbContext>().LifestyleScoped());
            container.Register(Component.For(typeof(DbContextOptions<TDbContext>)).Instance(dbContextOptions).LifestyleSingleton());

            return container;
        }
    }
}

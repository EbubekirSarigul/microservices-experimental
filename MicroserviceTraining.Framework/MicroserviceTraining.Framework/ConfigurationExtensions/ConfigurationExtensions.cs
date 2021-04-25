using AutoMapper;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.Resolvers.SpecializedResolvers;
using Castle.Windsor;
using Castle.Windsor.MsDependencyInjection;
using FluentValidation;
using MediatR;
using MicroserviceTraining.Framework.Behaviors;
using MicroserviceTraining.Framework.Cache;
using MicroserviceTraining.Framework.Cache.Abstraction;
using MicroserviceTraining.Framework.Commands;
using MicroserviceTraining.Framework.ExceptionMiddleware;
using MicroserviceTraining.Framework.IOC;
using MicroserviceTraining.Framework.IOC.Filters;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
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

        public static IWindsorContainer AddMediaTR(this IWindsorContainer container)
        {
            container.Kernel.Resolver.AddSubResolver(new CollectionResolver(container.Kernel));
            container.Kernel.AddHandlersFilter(new ContravariantFilter());

            container.Register(Classes.FromAssemblyContaining(typeof(BaseCommand<>)).BasedOn(typeof(IRequestHandler<,>)).WithServiceAllInterfaces().LifestyleTransient());
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
            AssemblyScanner.FindValidatorsInAssembly(typeof(BaseCommand<>).Assembly)
                                .ForEach(item => container.Register(Component.For(item.InterfaceType).ImplementedBy(item.ValidatorType).LifestyleTransient()));

            container.Register(Component.For(typeof(IPipelineBehavior<,>)).ImplementedBy(typeof(HandlerLogging<,>)));
            container.Register(Component.For(typeof(IPipelineBehavior<,>)).ImplementedBy(typeof(HandlerValidation<,>)));
            //container.Register(Component.For(typeof(IPipelineBehavior<,>)).ImplementedBy(typeof(HandlerTransaction<,>)));

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

            return applicationBuilder;
        }

        public static IWindsorContainer AddAutoMapper(this IWindsorContainer container, Assembly assembly)
        {
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

        public static IWindsorContainer AddRedis(this IWindsorContainer container, IConfiguration configuration)
        {
            RedisSettings redisSettings = new RedisSettings();

            configuration.GetSection("RedisSettings").Bind(redisSettings);

            container.Register(Component.For<ICacheProvider>().ImplementedBy<RedisCacheProvider>().LifestyleSingleton());

            return container;
        }
    }
}

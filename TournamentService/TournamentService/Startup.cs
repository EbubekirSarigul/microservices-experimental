using Castle.MicroKernel.Registration;
using FluentValidation;
using MediatR;
using MicroserviceTraining.Framework.ConfigurationExtensions;
using MicroserviceTraining.Framework.IntegrationEvents.ConfigurationExtensions;
using MicroserviceTraining.Framework.IOC;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Tournament.Core.BackgroundServices;
using Tournament.Core.Commands.CreateTournament;
using Tournament.Core.Commands.UpdateTournament;
using Tournament.Core.DomainEventHandlers;
using Tournament.Core.IntegrationEventHandlers.PaymentCompleted;
using Tournament.Core.Models;
using Tournament.Core.Queries;
using Tournament.Data.Context;
using Tournament.Data.Repositories;

namespace TournamentService
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            string connString = Configuration.GetConnectionString("MySqlConnStr");

            var dbContextOptions = new DbContextOptionsBuilder<TournamentContext>()
                                                                .UseMySql(connString, ServerVersion.AutoDetect(connString))
                                                                .UseQueryTrackingBehavior(QueryTrackingBehavior.TrackAll).Options;

            IocFacility.Container.Register(Component.For<TournamentContext>().LifestyleTransient());
            IocFacility.Container.Register(Component.For(typeof(DbContextOptions<TournamentContext>)).Instance(dbContextOptions).LifestyleSingleton());

            IocFacility.Container
                            .AddMediaTR()
                            .AddKafka(Configuration)
                            .AddAutoMapper(typeof(TournamentModel).Assembly);

            IocFacility.Container.Register(Classes.FromAssemblyContaining(typeof(CreateTournamentCommand)).BasedOn(typeof(IRequestHandler<,>)).WithServiceAllInterfaces().LifestyleTransient());
            IocFacility.Container.Register(Classes.FromAssemblyContaining(typeof(GetTournamentsQuery)).BasedOn(typeof(IRequestHandler<,>)).WithServiceAllInterfaces().LifestyleTransient());
            IocFacility.Container.Register(Classes.FromAssemblyContaining(typeof(NewTournamentAddedDomainEventHandler)).BasedOn(typeof(INotificationHandler<>)).LifestyleTransient());
            IocFacility.Container.Register(Classes.FromAssemblyContaining(typeof(TournamentDateChangedDomainEventHandler)).BasedOn(typeof(INotificationHandler<>)).LifestyleTransient());
            IocFacility.Container.Register(Classes.FromAssemblyContaining(typeof(TournamentEntryPriceChangedDomainEventHandler)).BasedOn(typeof(INotificationHandler<>)).LifestyleTransient());
            IocFacility.Container.Register(Classes.FromAssemblyContaining(typeof(PaymentCompletedIntegrationEventHandler)).BasedOn(typeof(INotificationHandler<>)).LifestyleTransient());

            services.AddScoped<ITournamentRepository, TournamentRepository>();
            services.AddTransient<IValidator<CreateTournamentCommand>, CreateTournamentCommandValidation>();
            services.AddTransient<IValidator<UpdateTournamentCommand>, UpdateTournamentCommandValidation>();
            services.AddHostedService<EventConsumerService>();

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "TournamentService", Version = "v1" });
            });

            services.AddWindsor();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "TournamentService v1"));
            }

            app.ConfigureAll();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}

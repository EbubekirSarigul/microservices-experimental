using MicroserviceTraining.Framework.IOC;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using MicroserviceTraining.Framework.ConfigurationExtensions;
using MicroserviceTraining.Framework.IntegrationEvents.ConfigurationExtensions;
using MediatR;
using Castle.MicroKernel.Registration;
using Player.Core.IntegrationEventHandlers.NewTournamentAdded;
using MicroserviceTraining.Framework.IntegrationEvents.Abstractions;
using Player.Core;
using MicroserviceTraining.Framework.Sms.Abstractions;
using MicroserviceTraining.Framework.Sms.Implementations;
using Player.Data.Context;
using Microsoft.EntityFrameworkCore;
using Player.Data.Repositories;
using Player.Core.BackgroundServices;
using MicroserviceTraining.Framework.IntegrationEvents.EventBuses.Kafka;

namespace PlayerService
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
            var dbContextOptions = new DbContextOptionsBuilder<PlayerContext>()
                                                                .UseMySql(connString, ServerVersion.AutoDetect(connString))
                                                                .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking).Options;

            IocFacility.Container.Register(Component.For<PlayerContext>().LifestyleScoped());
            IocFacility.Container.Register(Component.For(typeof(DbContextOptions<PlayerContext>)).Instance(dbContextOptions).LifestyleSingleton());

            IocFacility.Container
                .AddMediaTR()
                .AddKafka(Configuration);

            IocFacility.Container.Register(Classes.FromAssemblyContaining(typeof(NewTournamentAddedIntegrationEventHandler)).BasedOn(typeof(INotificationHandler<>)).LifestyleTransient());

            IocFacility.Container.Register(Component.For<ISmsProvider>().ImplementedBy<MockSmsProvider>().LifestyleTransient());
            IocFacility.Container.Register(Component.For<IPlayerRepository>().ImplementedBy<PlayerRepository>().LifestyleScoped());

            services.AddHostedService<EventConsumerService>();

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "PlayerService", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "PlayerService v1"));
            }

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

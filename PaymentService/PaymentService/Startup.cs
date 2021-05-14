using Castle.MicroKernel.Registration;
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
using Payment.Core.BackgroundServices;
using Payment.Core.Commands.Payment;
using Payment.Core.DomainEventHandlers;
using Payment.Core.IntegrationEventHandlers;
using Player.Data.Context;
using Player.Data.Repositories;

namespace PaymentService
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

            var dbContextOptions = new DbContextOptionsBuilder<PaymentContext>()
                                                                .UseMySql(connString, ServerVersion.AutoDetect(connString))
                                                                .UseQueryTrackingBehavior(QueryTrackingBehavior.TrackAll).Options;

            IocFacility.Container.Register(Component.For<PaymentContext>().LifestyleTransient());
            IocFacility.Container.Register(Component.For(typeof(DbContextOptions<PaymentContext>)).Instance(dbContextOptions).LifestyleSingleton());

            IocFacility.Container
                .AddMediaTR("Payment.Core")
                .AddKafka(Configuration);

            services.AddScoped<IPaymentRepository, PaymentRepository>();
            services.AddHostedService<EventConsumerService>();

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "PaymentService", Version = "v1" });
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
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "PaymentService v1"));
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

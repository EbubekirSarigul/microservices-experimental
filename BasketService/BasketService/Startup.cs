using MicroserviceTraining.Framework.IOC;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using MicroserviceTraining.Framework.ConfigurationExtensions;
using Castle.MicroKernel.Registration;
using Basket.Core.Commands.AddItem;
using MediatR;
using Basket.Core.Repository;
using FluentValidation;
using StackExchange.Redis;
using MicroserviceTraining.Framework.IntegrationEvents.ConfigurationExtensions;
using Basket.Core.Commands.Checkout;
using Basket.Core.IntegrationEventHandlers.PaymentCompleted;
using Basket.Core.BackgroundServices;

namespace BasketService
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
            IocFacility.Container
                .AddKafka(Configuration)
                .AddMediaTR("Basket.Core");

            IocFacility.Container.Register(Component.For<IBasketRepository>().ImplementedBy<BasketRepository>().LifestyleTransient());

            services.AddRedis(Configuration);
            services.AddHostedService<EventConsumerService>();

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "BasketService", Version = "v1" });
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
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "BasketService v1"));
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

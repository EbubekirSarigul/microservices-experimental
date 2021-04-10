using Castle.MicroKernel.Registration;
using Castle.Windsor;
using MicroserviceTraining.Framework.IntegrationEvents.Abstractions;
using MicroserviceTraining.Framework.IntegrationEvents.EventBuses.Kafka;
using MicroserviceTraining.Framework.IntegrationEvents.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MicroserviceTraining.Framework.IntegrationEvents.ConfigurationExtensions
{
    public static class ConfigurationExtensions
    {
        public static IWindsorContainer AddKafka(this IWindsorContainer container, IConfiguration configuration)
        {
            KafkaConfiguration kafkaConfiguration = new KafkaConfiguration();

            configuration.GetSection("KafkaConfiguration").Bind(kafkaConfiguration);

            container.Register(Component.For<IntegrationEventService>().LifestyleTransient(),
                               Component.For<KafkaProducer>().LifestyleSingleton(),
                               Component.For<IEventBus>().ImplementedBy<EventBusKafka>().LifestyleSingleton(),
                               Component.For<KafkaConsumer>().LifestyleSingleton(),
                               Component.For<ISubscriptionManager>().ImplementedBy<SubscriptionManager>().LifestyleSingleton(),
                               Component.For<KafkaConfiguration>().Instance(kafkaConfiguration).LifestyleSingleton());

            return container;
        }

        public static IServiceCollection AddKafka(this IServiceCollection services, IConfiguration configuration)
        {
            KafkaConfiguration kafkaConfiguration = new KafkaConfiguration();

            configuration.GetSection("KafkaConfiguration").Bind(kafkaConfiguration);

            services.AddSingleton<IEventBus, EventBusKafka>();
            services.AddSingleton<ISubscriptionManager, SubscriptionManager>();
            services.AddSingleton<KafkaProducer>();
            services.AddSingleton<KafkaConsumer>();
            services.AddTransient<IntegrationEventService>();
            services.AddSingleton<KafkaConfiguration>(kafkaConfiguration);

            return services;
        }
    }
}

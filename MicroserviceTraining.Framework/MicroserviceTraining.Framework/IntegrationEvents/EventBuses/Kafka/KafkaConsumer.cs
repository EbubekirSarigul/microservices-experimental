using Castle.MicroKernel.Lifestyle;
using Confluent.Kafka;
using MediatR;
using MicroserviceTraining.Framework.Constants;
using MicroserviceTraining.Framework.Extensions;
using MicroserviceTraining.Framework.IntegrationEvents.Abstractions;
using MicroserviceTraining.Framework.IOC;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MicroserviceTraining.Framework.IntegrationEvents.EventBuses.Kafka
{
    public class KafkaConsumer
    {
        private readonly ConsumerConfig _consumerConfig;

        public delegate Task MessageConsumedDelegate(string topic, Message<Null, string> message);
        public event MessageConsumedDelegate OnMessageConsumed;

        private readonly ILogger<KafkaConsumer> _logger;
        private readonly ISubscriptionManager _subscriptionManager;
        private readonly IMediator _mediator;

        public KafkaConsumer(ILogger<KafkaConsumer> logger, KafkaConfiguration configuration, ISubscriptionManager subscriptionManager, IMediator mediator)
        {
            ConsumerConfig consumerConfig = new ConsumerConfig
            {
                BootstrapServers = configuration.Servers,
                GroupId = configuration.ConsumerGroupId,
                AutoOffsetReset = AutoOffsetReset.Latest,
                Acks = Acks.Leader
            };

            _consumerConfig = consumerConfig;
            _logger = logger;
            _subscriptionManager = subscriptionManager;
            _mediator = mediator;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Kafka consumer begins consuming.");

            OnMessageConsumed += EventMessageConsumed;
            await Consume(cancellationToken);
        }

        public async Task Consume(CancellationToken cancellationToken)
        {
            using (var consumer = new ConsumerBuilder<Null, string>(_consumerConfig).Build())
            {
                var _topics = _subscriptionManager.GetTopics();
                consumer.Subscribe(_topics);

                _logger.LogInformation($"Topics subscribed: {string.Join("' ", _topics)}");

                while (!cancellationToken.IsCancellationRequested)
                {
                    try
                    {
                        var consumeResult = consumer.Consume(cancellationToken);
                        await OnMessageConsumed(consumeResult.Topic, consumeResult.Message);
                    }
                    catch (Exception consumeException)
                    {
                        _logger.LogCritical($"Unhandled exception while consuming events: {consumeException.ToString()}");
                        await Task.Delay(1000, cancellationToken);
                    }
                }

                consumer.Close();
            }
        }

        public async Task EventMessageConsumed(string topic, Message<Null, string> message)
        {
            try
            {
                _logger.LogDebug($"Event consumed. Topic: {topic}, Message: {message?.Value}");

                string key = Constant.IntegrationEventPrefix + topic;

                var eventType = _subscriptionManager.GetEventType(topic);
                var @event = JsonConvert.DeserializeObject(message.Value, eventType);

                using (IocFacility.Container.BeginScope())
                {
                    await _mediator.Publish(@event);
                }
            }
            catch (Exception handlerException)
            {
                _logger.LogCritical($"Unhandled exception while proccessing consumed event: {handlerException.ToString()}");
            }
        }
    }
}

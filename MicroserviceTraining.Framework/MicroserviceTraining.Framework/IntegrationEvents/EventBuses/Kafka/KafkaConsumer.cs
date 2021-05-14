using Castle.MicroKernel.Lifestyle;
using Confluent.Kafka;
using MediatR;
using MicroserviceTraining.Framework.Constants;
using MicroserviceTraining.Framework.IntegrationEvents.Abstractions;
using MicroserviceTraining.Framework.IOC;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace MicroserviceTraining.Framework.IntegrationEvents.EventBuses.Kafka
{
    public class KafkaConsumer
    {
        private readonly ConsumerConfig _consumerConfig;

        public delegate Task MessageConsumedDelegate(string topic, Message<Null, string> message);
        public event MessageConsumedDelegate OnMessageConsumed;
        private readonly ISubscriptionManager _subscriptionManager;
        private readonly IMediator _mediator;

        public KafkaConsumer(KafkaConfiguration configuration, ISubscriptionManager subscriptionManager, IMediator mediator)
        {
            ConsumerConfig consumerConfig = new ConsumerConfig
            {
                BootstrapServers = configuration.Servers,
                GroupId = configuration.ConsumerGroupId,
                AutoOffsetReset = AutoOffsetReset.Latest,
                Acks = Acks.Leader
            };

            _consumerConfig = consumerConfig;

            _subscriptionManager = subscriptionManager;
            _mediator = mediator;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            OnMessageConsumed += EventMessageConsumed;
            await Consume(cancellationToken);
        }

        public async Task Consume(CancellationToken cancellationToken)
        {
            using (var consumer = new ConsumerBuilder<Null, string>(_consumerConfig).Build())
            {
                var _topics = _subscriptionManager.GetTopics();
                consumer.Subscribe(_topics);

                while (!cancellationToken.IsCancellationRequested)
                {
                    try
                    {
                        var consumeResult = consumer.Consume(cancellationToken);
                        await OnMessageConsumed(consumeResult.Topic, consumeResult.Message);
                    }
                    catch (Exception consumeException)
                    {
                        //todo...
                        string message = consumeException.ToString();
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
                string key = Constant.IntegrationEventPrefix + topic;

                var eventType = _subscriptionManager.GetEventType(topic);
                var @event = JsonConvert.DeserializeObject(message.Value, eventType);

                using (IocFacility.Container.BeginScope()) // it is not a web request, so a scope should begin.
                {
                    await _mediator.Publish(@event);
                }
            }
            catch (Exception handlerException)
            {
                //todo...
            }
        }
    }
}

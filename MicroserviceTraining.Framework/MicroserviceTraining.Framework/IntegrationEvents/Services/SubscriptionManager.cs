using MicroserviceTraining.Framework.Constants;
using MicroserviceTraining.Framework.IntegrationEvents.Abstractions;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace MicroserviceTraining.Framework.IntegrationEvents.Services
{
    public class SubscriptionManager : ISubscriptionManager
    {
        readonly ConcurrentBag<string> _topics;
        readonly ConcurrentDictionary<string, Type> _eventTypes;
        private readonly ILogger<SubscriptionManager> _logger;

        public SubscriptionManager(ILogger<SubscriptionManager> logger)
        {
            _topics = new ConcurrentBag<string>();
            _eventTypes = new ConcurrentDictionary<string, Type>();
            _logger = logger;
        }

        public void Subscribe<TEvent>(string topic) where TEvent : IntegrationEvent
        {
            _logger.LogInformation($"Subscribing to {topic}. Event: {typeof(TEvent).Name}");

            string key = Constant.IntegrationEventPrefix + topic;
            _topics.Add(topic);
            _eventTypes.TryAdd(key, typeof(TEvent));
        }

        public ICollection<string> GetTopics()
        {
            return _topics.ToList();
        }

        public Type GetEventType(string topic)
        {
            string key = Constant.IntegrationEventPrefix + topic;
            if (_eventTypes.TryGetValue(key, out Type type))
            {
                return type;
            }

            return default(Type);
        }
    }
}

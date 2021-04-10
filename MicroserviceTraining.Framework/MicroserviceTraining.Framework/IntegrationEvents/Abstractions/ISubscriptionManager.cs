using System;
using System.Collections.Generic;

namespace MicroserviceTraining.Framework.IntegrationEvents.Abstractions
{
    public interface ISubscriptionManager
    {
        void Subscribe<TEvent>(string topic) where TEvent : IntegrationEvent;

        ICollection<string> GetTopics();

        Type GetEventType(string topic);
    }
}

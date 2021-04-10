using MediatR;
using System;
using System.Collections.Generic;

namespace MicroserviceTraining.Framework.Data
{
    public abstract class Entity
    {
        public Entity()
        {
            _id = Guid.NewGuid();
        }

        Guid _id;

        public Guid Id
        {
            get
            {
                return _id;
            }
            protected set
            {
                _id = value;
            }
        }

        private List<INotification> _domainEvents;
        public IReadOnlyCollection<INotification> DomainEvents => _domainEvents?.AsReadOnly();

        public void AddDomainEvent(INotification eventItem)
        {
            _domainEvents = _domainEvents ?? new List<INotification>();
            _domainEvents.Add(eventItem);
        }
    }
}

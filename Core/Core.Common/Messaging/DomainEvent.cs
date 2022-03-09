using System;

namespace Core.Common.Messaging
{
    public abstract class DomainEvent : Event
    {
        protected DomainEvent(Guid aggregateId) =>      
            AggregateId = aggregateId;
       
    }
}

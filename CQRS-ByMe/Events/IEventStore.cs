using System;
using System.Collections.Generic;
using CQRS_ByMe.Domain;
using CQRS_ByMe.Domain.Mementos;

namespace CQRS_ByMe.Events
{
    public interface IEventStore
    {
        IEnumerable<IEvent> GetEvents(Guid aggregateId);
        void Save(BaseAggregateRoot aggregate);
        BaseMemento GetMemento(Guid aggregateId);
        void SaveMemento(BaseMemento memento);
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using CQRS_ByMe.Domain;
using CQRS_ByMe.Domain.Mementos;
using CQRS_ByMe.EventBus;

namespace CQRS_ByMe.Events
{
    public class InMemoryEventStore : IEventStore
    {
        private static readonly IList<IEvent> _events = new List<IEvent>(); 

        private static readonly List<BaseMemento> _mementos = new List<BaseMemento>(); 

        private readonly IEventBus _eventBus = new EventBus.EventBus();
        public IEnumerable<IEvent> GetEvents(Guid aggregateId)
        {
            return _events.Where(x => x.Id == aggregateId).OrderBy(x => x.Version);
        }

        public IEnumerable<IEvent> GetAllEvents()
        {
            return _events;
        }

        public void Save(BaseAggregateRoot aggregate)
        {
            foreach (var @event in aggregate.GetUncommittedEvents())
            {
                _events.Add(@event);
            }
            foreach (var @event in aggregate.GetUncommittedEvents().OrderBy(x=>x.Version))
            {
                var e = (dynamic) Convert.ChangeType(@event, @event.GetType());
                _eventBus.Publish(e);
            }
          
            aggregate.ClearEvents();
        }

        public BaseMemento GetMemento(Guid aggregateId)
        {
            return _mementos.SingleOrDefault(x => x.Id == aggregateId);
        }

        public void SaveMemento(BaseMemento memento)
        {
            if (_mementos.Any(x => x.Id == memento.Id))
            {
                _mementos.RemoveAll(x => x.Id == memento.Id);
            }
            _mementos.Add(memento);
        }
    }
}
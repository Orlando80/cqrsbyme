using System;
using System.Collections.Generic;
using CQRS_ByMe.Domain.Mementos;
using CQRS_ByMe.Events;

namespace CQRS_ByMe.Domain
{
    public abstract class BaseAggregateRoot : IOriginator
    {
        public Guid Id { get; protected set; }
        public int Version { get; protected set; }

        private readonly IList<IEvent> _events;

        public abstract IDictionary<Type, Action<IEvent>> RegisteredHandlers { get; }

        public void ClearEvents()
        {
            _events.Clear();
        }

        public IEnumerable<IEvent> GetUncommittedEvents()
        {
            return _events;
        }

        protected BaseAggregateRoot()
        {
            _events = new List<IEvent>();
        }

        public void ApplyChanges(IEvent @event, bool isReplay = false)
        {
            if (isReplay)
            {
                RegisteredHandlers[@event.GetType()](@event);
            }
            else
            {
                @event.Version++;
                _events.Add(@event);
                Version = @event.Version;
            }

        }

        public abstract void ApplyMemento(BaseMemento memento);

        public abstract BaseMemento GenerateMemento();
    }

}
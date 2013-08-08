using System;
using System.Linq;
using CQRS_ByMe.Domain;
using CQRS_ByMe.Events;

namespace CQRS_ByMe.Repositories
{
    public class Repository<T> : IRepository<T> where T : BaseAggregateRoot, new ()
    {
        readonly IEventStore _eventStore = new InMemoryEventStore();

        public T GetById(Guid id)
        {
            var memento = _eventStore.GetMemento(id);
            int version = 0;
            var aggregate = new T();
            if (memento != null)
            {
                aggregate.ApplyMemento(memento);
                version = memento.Version;
            }
            var history = _eventStore.GetEvents(id).Where( x=> x.Version > version);
         
            foreach (var @event in history)
            {
                aggregate.ApplyChanges(@event, isReplay: true);
            }
            return aggregate;
        }

        public void Save(T aggregateRoot)
        {
            if (aggregateRoot.Version % 5 == 0)
            {
                _eventStore.SaveMemento(aggregateRoot.GenerateMemento());
            }
            _eventStore.Save(aggregateRoot);
        }
    }
}
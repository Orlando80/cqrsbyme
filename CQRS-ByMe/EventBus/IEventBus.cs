using System;
using System.Collections.Generic;
using System.Linq;
using CQRS_ByMe.Events;

namespace CQRS_ByMe.EventBus
{
    public interface IEventBus
    {
        void Publish<T>(T @event) where T : IEvent;
    }

    public class EventBus : IEventBus
    {
        public void Publish<T>(T @event) where T : IEvent
        {
            var handler = new EventFactory().GetInstance<T>(@event);
            handler.Handle(@event);
        }
    }

    public interface IEventFactory
    {
        IEventHandler<T> GetInstance<T>(T @event) where T : IEvent;
    }

    public class EventFactory : IEventFactory
    {
        public IEventHandler<T> GetInstance<T>(T @event) where T : IEvent
        {
            if (@event.GetType() == typeof (LastNameChangedEvent))
                return (dynamic)new LastNameChangedEventHandler();
            if (@event.GetType() == typeof (AgeChangedEvent))
                return (dynamic) new AgeChangedEventHandler();

            return @event.GetType() == typeof(ItemCreatedEvent) 
                ? (dynamic)new ItemCreatedEventHandler() 
                : new NameChangedEventHandler();
        }
    }

    public interface IEventHandler<in T> where T : IEvent
    {
        void Handle(T @event);
    }

    public class ItemCreatedEventHandler : IEventHandler<ItemCreatedEvent>
    {
        private readonly InMemoryReportRepository _reporting = new InMemoryReportRepository();
        public void Handle(ItemCreatedEvent @event)
        {
            _reporting.Add(new CustomerDto
                {
                    Id = @event.Id,
                    Name = @event.Name,
                    LastName = @event.LastName,
                    Age = @event.Age
                });
        }
    }

    public class NameChangedEventHandler : IEventHandler<NameChangedEvent>
    {
        private readonly InMemoryReportRepository _reporting = new InMemoryReportRepository();

        public void Handle(NameChangedEvent @event)
        {
            var customer = _reporting.GetById(@event.Id);
            customer.Name = @event.Name;
        }
    }
    public class LastNameChangedEventHandler : IEventHandler<LastNameChangedEvent>
    {
        private readonly InMemoryReportRepository _reporting = new InMemoryReportRepository();

        public void Handle(LastNameChangedEvent @event)
        {
            var customer = _reporting.GetById(@event.Id);
            customer.LastName = @event.LastName;
        }
    }
    public class AgeChangedEventHandler : IEventHandler<AgeChangedEvent>
    {
        private readonly InMemoryReportRepository _reporting = new InMemoryReportRepository();

        public void Handle(AgeChangedEvent @event)
        {
            var customer = _reporting.GetById(@event.Id);
            customer.Age = @event.Age;
        }
    }

    public class InMemoryReportRepository
    {
        private static readonly IList<CustomerDto> _customers = new List<CustomerDto>();

        public void Add(CustomerDto customer)
        {
            _customers.Add(customer);
        }

        public CustomerDto GetById(Guid id)
        {
            return _customers.FirstOrDefault(x => x.Id == id);
           
        }

        public IEnumerable<CustomerDto> GetAll()
        {
            return _customers;
        }
    }

    public class CustomerDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
    }
}
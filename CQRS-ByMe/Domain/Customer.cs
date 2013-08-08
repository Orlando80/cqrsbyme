using System;
using System.Collections.Generic;
using CQRS_ByMe.Domain.Mementos;
using CQRS_ByMe.Events;

namespace CQRS_ByMe.Domain
{
    public class Customer : BaseAggregateRoot
    {
        public Customer()
        {
        }

        public Customer(Guid id, string name, string lastName, int age)
        {
            ApplyChanges(new ItemCreatedEvent(id, 0, name, lastName, age));
        }

        public void ChangeName(string name)
        {
            ApplyChanges(new NameChangedEvent(Id, Version, name));
        }

        public string Name { get; private set; }

        public string LastName { get; private set; }

        public int Age { get; private set; }

        public override IDictionary<Type, Action<IEvent>> RegisteredHandlers
        {
            get
            {
                return new Dictionary<Type, Action<IEvent>>
                    {
                        {
                            typeof (ItemCreatedEvent), e =>
                                {
                                    Name = ((ItemCreatedEvent) e).Name;
                                    LastName = ((ItemCreatedEvent) e).LastName;
                                    Age = ((ItemCreatedEvent) e).Age;
                                    Id = e.Id;
                                    Version = e.Version;
                                }
                        },
                        {
                            typeof(NameChangedEvent), e =>
                                {
                                    Name = ((NameChangedEvent) e).Name;
                                    Id = e.Id;
                                    Version = e.Version;
                                }
                        },
                        {
                            typeof(LastNameChangedEvent), e =>
                                {
                                    LastName = ((LastNameChangedEvent) e).LastName;
                                    Id = e.Id;
                                    Version = e.Version;
                                }
                        },
                        {
                            typeof(AgeChangedEvent), e =>
                                {
                                    Age = ((AgeChangedEvent)e).Age;
                                    Id = e.Id;
                                    Version = e.Version;
                                }
                        }
                    };
            }
        }

        public override void ApplyMemento(BaseMemento memento)
        {
            var customerMemento = (CustomerMemento) memento;
            Id = customerMemento.Id;
            Version = customerMemento.Version;
            Name = customerMemento.Name;
            LastName = customerMemento.LastName;
            Age = customerMemento.Age;
        }

        public override BaseMemento GenerateMemento()
        {
            return new CustomerMemento
                {
                    Id = Id,
                    Version = Version,
                    Name =Name,
                    LastName = LastName,
                    Age = Age
                };
        }

        public void ChangeLastName(string lastName)
        {
            ApplyChanges(new LastNameChangedEvent(Id,Version, lastName));
        }

        public void ChangeAge(int age)
        {
            ApplyChanges(new AgeChangedEvent(Id,Version,age));

        }
    }

    public interface IOriginator
    {
        void ApplyMemento(BaseMemento memento);
        BaseMemento GenerateMemento();
    }
}
using System;

namespace CQRS_ByMe.Events
{
    public class NameChangedEvent : IEvent
    {
        public NameChangedEvent(Guid id, int version, string name)
        {
            Id = id;
            Version = version;
            Name = name;
        }

        public string Name { get; set; }
        public Guid Id { get; set; }
        public int Version { get; set; }
    }

    public class LastNameChangedEvent : IEvent
    {
        public Guid Id { get; set; }
        public int Version { get; set; }
        public string LastName { get; set; }

        public LastNameChangedEvent(Guid id, int version, string lastName)
        {
            Id = id;
            Version = version;
            LastName = lastName;
        }
    }

    public class AgeChangedEvent : IEvent
    {
        public Guid Id { get; set; }
        public int Version { get; set; }
        public int Age { get; set; }

        public AgeChangedEvent(Guid id, int version, int age)
        {
            Id = id;
            Version = version;
            Age = age;
        }
    }
}
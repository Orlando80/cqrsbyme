using System;

namespace CQRS_ByMe.Events
{
    public class ItemCreatedEvent : IEvent
    {
        public Guid Id { get; set; }
        public int Version { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public ItemCreatedEvent(Guid id, int version, string name, string lastName, int age)
        {
            Id = id;
            Version = version;
            Name = name;
            LastName = lastName;
            Age = age;
        }
    }
}
using System;
using CQRS_ByMe.Bus;

namespace CQRS_ByMe.Commands
{
    public class CreateItemCommand : ICommand
    {
        public CreateItemCommand(Guid id, string name, string lastName, int age)
        {
            Id = id;
            Name = name;
            LastName = lastName;
            Age = age;
        }
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public string LastName { get; private set; }
        public int Age { get; private set; }
    }
}
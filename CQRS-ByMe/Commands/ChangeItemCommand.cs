using System;
using CQRS_ByMe.Bus;

namespace CQRS_ByMe.Commands
{
    public class ChangeItemCommand : ICommand
    {
        public Guid Id { get; private set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }

        public ChangeItemCommand(Guid id, string name,string lastName, int age)
        {
            Id = id;
            Name = name;
            LastName = lastName;
            Age = age;
        }

    }
}
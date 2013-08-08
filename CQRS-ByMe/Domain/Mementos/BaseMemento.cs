using System;

namespace CQRS_ByMe.Domain.Mementos
{
    public class BaseMemento
    {
        public Guid Id { get; set; }
        public int Version { get; set; }
    }
}
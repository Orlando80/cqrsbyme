using System;

namespace CQRS_ByMe.Bus
{
    public interface ICommand
    {
        Guid Id { get; }
    }
}
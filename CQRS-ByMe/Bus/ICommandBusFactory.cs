using CQRS_ByMe.CommandHandler;
using CQRS_ByMe.Commands;

namespace CQRS_ByMe.Bus
{
    public interface ICommandBusFactory
    {
        ICommandHandler<T> GetHandler<T>() where T : ICommand;
    }
}
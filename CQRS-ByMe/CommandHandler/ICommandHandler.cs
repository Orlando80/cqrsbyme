using CQRS_ByMe.Bus;

namespace CQRS_ByMe.CommandHandler
{
    public interface ICommandHandler<in T> where T : ICommand
    {
        void Execute(T  message);
    }
}
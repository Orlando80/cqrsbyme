using CQRS_ByMe.CommandHandler;
using CQRS_ByMe.Commands;
using CQRS_ByMe.Domain;
using CQRS_ByMe.Repositories;

namespace CQRS_ByMe.Bus
{
    public class CommandBusFactory : ICommandBusFactory
    {
        public ICommandHandler<T> GetHandler<T>() where T : ICommand
        {
            return typeof(T) == typeof (CreateItemCommand)
                ? (ICommandHandler<T>)new CreateItemCommandHandler(new Repository<Customer>())
                : (ICommandHandler<T>)new ChangeItemCommandHandler(new Repository<Customer>());
        }
    }
}
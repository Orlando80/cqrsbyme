using CQRS_ByMe.Commands;
using CQRS_ByMe.Domain;
using CQRS_ByMe.Repositories;

namespace CQRS_ByMe.CommandHandler
{
    public class CreateItemCommandHandler : ICommandHandler<CreateItemCommand>
    {
        private readonly IRepository<Customer> _repository;

        public CreateItemCommandHandler(IRepository<Customer> repository)
        {
            _repository = repository;
        }

        public void Execute(CreateItemCommand message)
        {
            var customer = new Customer(message.Id, message.Name, message.LastName, message.Age);
            _repository.Save(customer);
        }
    }
}
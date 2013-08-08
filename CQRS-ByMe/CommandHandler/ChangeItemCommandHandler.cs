using CQRS_ByMe.Commands;
using CQRS_ByMe.Domain;
using CQRS_ByMe.Repositories;

namespace CQRS_ByMe.CommandHandler
{
    public class ChangeItemCommandHandler : ICommandHandler<ChangeItemCommand>
    {
        private readonly IRepository<Customer> _repository;

        public ChangeItemCommandHandler(IRepository<Customer> repository)
        {
            _repository = repository;
        }

        public void Execute(ChangeItemCommand message)
        {
            var customer = _repository.GetById(message.Id);
            if(customer.Name != message.Name)
                customer.ChangeName(message.Name);
            if (customer.LastName != message.LastName)
                customer.ChangeLastName(message.LastName);
            if (customer.Age != message.Age)
                customer.ChangeAge(message.Age);
            _repository.Save(customer);
        }
    }
}
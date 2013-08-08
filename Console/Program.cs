using System;
using CQRS_ByMe.Bus;
using CQRS_ByMe.Commands;
using CQRS_ByMe.Domain;
using CQRS_ByMe.EventBus;
using CQRS_ByMe.Repositories;

namespace Console
{
    class Program
    {
        static ICommandBus _bus = new CommandBus(new CommandBusFactory()); 
        static void Main(string[] args)
        {
            var id = Guid.NewGuid();
            _bus.Send(new CreateItemCommand(id, "Orlando", "Perri", 18));
            _bus.Send(new ChangeItemCommand(id,"Other name"));
            //var back = new Repository<Customer>().GetById(id);

            var reporting = new InMemoryReportRepository();
            var customer =reporting.GetById(id);
            System.Console.ReadLine();
        }
    }
}

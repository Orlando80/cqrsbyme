using System;

namespace CQRS_ByMe.Bus
{
    public class CommandBus : ICommandBus
    {
        private readonly ICommandBusFactory _commandBusFactory;

        public CommandBus(ICommandBusFactory commandBusFactory)
        {
            _commandBusFactory = commandBusFactory;
        }

        public void Send<T>(T message) where T : class, ICommand 
        {
            var handler = _commandBusFactory.GetHandler<T>();
            if(handler == null)
                throw new Exception("Command not found");
            handler.Execute(message);
        }
    }
}
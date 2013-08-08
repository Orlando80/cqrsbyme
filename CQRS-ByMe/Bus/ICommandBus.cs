namespace CQRS_ByMe.Bus
{
    public interface ICommandBus
    {
        void Send<T>(T message) where T : class, ICommand;
    }
}
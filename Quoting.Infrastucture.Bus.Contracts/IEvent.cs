namespace Quoting.Infrastructure.Bus.Contracts
{
    public interface IEvent
    {
    }
    public interface IEventBuilder : IEvent
    {
        IEvent Build();
    }
}

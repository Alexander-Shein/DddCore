namespace DddCore.Contracts.BLL.Domain.Events
{
    public interface IDomainEventHandler<in T> where T : IDomainEvent
    {
        void Handle(T args);
    }
}
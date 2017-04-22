namespace DddCore.Contracts.BLL.Domain.Events
{
    public interface IDomainEventDispatcher
    {
        /// <summary>
        /// Pass domain event to related handlers
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="domainEvent"></param>
        void Raise<T>(T domainEvent) where T : IDomainEvent;
    }
}
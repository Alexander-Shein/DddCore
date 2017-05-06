namespace DddCore.Contracts.BLL.Domain.Entities
{
    public interface IDomainFactory
    {
        T Create<T, TKey>() where T : IEntity<TKey>;
    }
}
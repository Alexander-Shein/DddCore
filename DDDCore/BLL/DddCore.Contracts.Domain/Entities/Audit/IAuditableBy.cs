namespace DddCore.Contracts.Domain.Entities.Audit
{
    public interface IAuditableBy<TKey> : ICreatedBy<TKey>, IModifiedBy<TKey>
    {
    }
}
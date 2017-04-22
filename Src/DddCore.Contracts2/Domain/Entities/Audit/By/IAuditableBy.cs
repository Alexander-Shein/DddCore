namespace DddCore.Contracts.Domain.Entities.Audit.By
{
    public interface IAuditableBy<TKey> : ICreatedBy<TKey>, IModifiedBy<TKey>
    {
    }
}
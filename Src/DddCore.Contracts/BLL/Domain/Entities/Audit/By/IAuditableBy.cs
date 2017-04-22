namespace DddCore.Contracts.BLL.Domain.Entities.Audit.By
{
    public interface IAuditableBy<TKey> : ICreatedBy<TKey>, IModifiedBy<TKey>
    {
    }
}
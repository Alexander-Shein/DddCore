namespace DddCore.Contracts.BLL.Domain.Models.Audit.By
{
    public interface IAuditableBy<TKey> : ICreatedBy<TKey>, IModifiedBy<TKey>
    {
    }
}
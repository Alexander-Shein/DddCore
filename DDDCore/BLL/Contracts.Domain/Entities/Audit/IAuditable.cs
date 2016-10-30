namespace Contracts.Domain.Entities.Audit
{
    public interface IAuditable<TKey> : IAuditableAt, IAuditableBy<TKey>
    {
    }
}
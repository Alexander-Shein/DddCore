namespace Contracts.Domain.Entities.Audit
{
    public interface IAuditableAt : ICreatedAt, IModifiedAt
    {
    }
}
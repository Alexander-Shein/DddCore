using DddCore.Contracts.Domain.Entities.Audit.At;
using DddCore.Contracts.Domain.Entities.Audit.By;

namespace DddCore.Contracts.Domain.Entities.Audit
{
    public interface IAuditable<TKey> : IAuditableAt, IAuditableBy<TKey>
    {
    }
}
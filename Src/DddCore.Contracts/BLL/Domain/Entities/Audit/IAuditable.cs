using DddCore.Contracts.BLL.Domain.Entities.Audit.At;
using DddCore.Contracts.BLL.Domain.Entities.Audit.By;

namespace DddCore.Contracts.BLL.Domain.Entities.Audit
{
    public interface IAuditable<TKey> : IAuditableAt, IAuditableBy<TKey>
    {
    }
}
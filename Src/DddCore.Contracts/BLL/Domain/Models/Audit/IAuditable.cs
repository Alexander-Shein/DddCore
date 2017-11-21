using DddCore.Contracts.BLL.Domain.Models.Audit.At;
using DddCore.Contracts.BLL.Domain.Models.Audit.By;

namespace DddCore.Contracts.BLL.Domain.Models.Audit
{
    public interface IAuditable<TKey> : IAuditableAt, IAuditableBy<TKey>
    {
    }
}
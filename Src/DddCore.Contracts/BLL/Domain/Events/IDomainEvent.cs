using DddCore.Contracts.BLL.Domain.Entities.Audit.At;

namespace DddCore.Contracts.BLL.Domain.Events
{
    public interface IDomainEvent : ICreatedAt
    {
    }
}
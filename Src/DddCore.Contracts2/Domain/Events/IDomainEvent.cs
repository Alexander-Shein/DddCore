using DddCore.Contracts.Domain.Entities.Audit.At;

namespace DddCore.Contracts.Domain.Events
{
    public interface IDomainEvent : ICreatedAt
    {
    }
}
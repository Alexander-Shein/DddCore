using DddCore.Contracts.BLL.Domain.Entities.Model;

namespace DddCore.Contracts.BLL.Domain.Entities
{
    public interface IEntity<TKey> : ICrudState, IIdentity<TKey>, IDomainEvents, IBusinessRules
    {
    }
}
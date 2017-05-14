using System.Threading.Tasks;
using DddCore.Contracts.BLL.Domain.Entities.BusinessRules;
using DddCore.Contracts.BLL.Domain.Entities.Model;
using DddCore.Contracts.BLL.Domain.Events;
using DddCore.Contracts.BLL.Errors;

namespace DddCore.Contracts.BLL.Domain.Entities
{
    public interface IEntity<TKey> : ICrudState, IIdentity<TKey>, IDomainEvents
    {
        void RaiseEvents(IDomainEventDispatcher domainEventDispatcher);
        Task<OperationResult> ValidateAsync(IBusinessRulesValidatorFactory businessRulesValidatorFactory);
        OperationResult Validate(IBusinessRulesValidatorFactory businessRulesValidatorFactory);
    }
}
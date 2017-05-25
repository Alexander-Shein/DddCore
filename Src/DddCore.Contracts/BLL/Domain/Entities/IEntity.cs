using System.Collections.Generic;
using System.Threading.Tasks;
using DddCore.Contracts.BLL.Domain.Entities.BusinessRules;
using DddCore.Contracts.BLL.Domain.Entities.State;
using DddCore.Contracts.BLL.Domain.Events;
using DddCore.Contracts.BLL.Errors;

namespace DddCore.Contracts.BLL.Domain.Entities
{
    public interface IEntity<TKey> : ICrudState, IIdentity<TKey>
    {
        ICollection<IDomainEvent> Events { get; }
        OperationResult RaiseEvents(IDomainEventDispatcher domainEventDispatcher);

        Task<OperationResult> ValidateAsync(IBusinessRulesValidatorFactory factory);
        OperationResult Validate(IBusinessRulesValidatorFactory factory);
    }
}
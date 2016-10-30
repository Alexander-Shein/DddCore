using Contracts.Domain.Entities.Model;

namespace Contracts.Domain.Entities.BusinessRules
{
    public interface IBusinessRulesValidatorFactory
    {
        IBusinessRulesValidator<T> GetValidator<T>() where T : ICrudState;
    }
}
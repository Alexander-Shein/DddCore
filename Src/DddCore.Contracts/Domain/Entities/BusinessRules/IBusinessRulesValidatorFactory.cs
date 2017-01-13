using DddCore.Contracts.Domain.Entities.Model;

namespace DddCore.Contracts.Domain.Entities.BusinessRules
{
    public interface IBusinessRulesValidatorFactory
    {
        IBusinessRulesValidator<T> GetBusinessRulesValidator<T>() where T : ICrudState;
    }
}
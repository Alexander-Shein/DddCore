using DddCore.Contracts.BLL.Domain.Entities.State;

namespace DddCore.Contracts.BLL.Domain.Entities.BusinessRules
{
    public interface IBusinessRulesValidatorFactory
    {
        IBusinessRulesValidator<T> GetBusinessRulesValidator<T>() where T : ICrudState;
        IBusinessRulesValidator<T> GetBusinessRulesValidator<T>(T instance) where T : ICrudState;
    }
}
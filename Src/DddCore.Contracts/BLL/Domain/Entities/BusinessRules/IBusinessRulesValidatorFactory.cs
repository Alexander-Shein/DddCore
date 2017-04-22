using DddCore.Contracts.BLL.Domain.Entities.Model;

namespace DddCore.Contracts.BLL.Domain.Entities.BusinessRules
{
    public interface IBusinessRulesValidatorFactory
    {
        IBusinessRulesValidator<T> GetBusinessRulesValidator<T>() where T : ICrudState;
    }
}
namespace DddCore.Contracts.BLL.Domain.BusinessRules
{
    public interface IBusinessRulesValidatorFactory
    {
        IBusinessRulesValidator<T> GetBusinessRulesValidator<T>();
        IBusinessRulesValidator<T> GetBusinessRulesValidator<T>(T instance);
    }
}
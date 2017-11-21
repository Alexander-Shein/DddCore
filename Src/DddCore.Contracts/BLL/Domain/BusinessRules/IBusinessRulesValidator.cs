using DddCore.Contracts.BLL.Domain.Services;

namespace DddCore.Contracts.BLL.Domain.BusinessRules
{
    public interface IBusinessRulesValidator<in T>
    {
        Result Validate(T instance);
        Result Validate<TData>(T instance, TData data);

        Result Validate(T instance, string ruleSet);
        Result Validate<TData>(T instance, string ruleSet, TData data);
    }
}
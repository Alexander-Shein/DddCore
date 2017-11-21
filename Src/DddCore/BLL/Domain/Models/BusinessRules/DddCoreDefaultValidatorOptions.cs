using FluentValidation;
using FluentValidation.Resources;

namespace DddCore.BLL.Domain.Models.BusinessRules
{
    public static class DddCoreDefaultValidatorOptions
    {
        public static IRuleBuilderOptions<T, TProperty> WithErrorCode<T, TProperty>(this IRuleBuilderOptions<T, TProperty> rule, int errorCode)
        {
            return rule.Configure(config =>
            {
                config.CurrentValidator.ErrorCodeSource = new StaticStringSource(errorCode.ToString());
            });
        }
    }
}
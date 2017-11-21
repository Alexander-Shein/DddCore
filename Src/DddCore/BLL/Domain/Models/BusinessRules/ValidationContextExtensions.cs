using FluentValidation;

namespace DddCore.BLL.Domain.Models.BusinessRules
{
    public static class ValidationContextExtensions
    {
        public static T GetData<T>(this ValidationContext<T> validationContext) where T : class
        {
            return validationContext.RootContextData[BusinessRulesValidatorBase<T>.Data] as T;
        }
    }
}
using System.Threading.Tasks;
using DddCore.Contracts.Domain.Entities.Model;

namespace DddCore.Contracts.Domain.Entities.BusinessRules
{
    public interface IBusinessRulesValidator<in T> where T : ICrudState
    {
        Task<BusinessRulesValidationResult> ValidateAsync(T instance);
        BusinessRulesValidationResult Validate(T instance);
    }
}
using System.Threading.Tasks;

namespace DddCore.Contracts.Domain.Entities.BusinessRules
{
    public interface IValidatable
    {
        Task<BusinessRulesValidationResult> ValidateAsync();
        BusinessRulesValidationResult Validate();
    }
}
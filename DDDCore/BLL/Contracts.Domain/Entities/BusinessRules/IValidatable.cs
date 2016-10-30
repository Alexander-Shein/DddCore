using System.Threading.Tasks;

namespace Contracts.Domain.Entities.BusinessRules
{
    public interface IValidatable
    {
        Task<BusinessRulesValidationResult> ValidateAsync();
    }
}
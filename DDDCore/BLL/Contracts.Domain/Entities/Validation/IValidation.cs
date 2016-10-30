using System.Threading.Tasks;

namespace Contracts.Domain.Entities.Validation
{
    public interface IValidation
    {
        Task<EntityValidationResult> ValidateAsync();
    }
}
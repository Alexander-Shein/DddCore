using System.Threading.Tasks;
using Contracts.Domain.Entities.Model;

namespace Contracts.Domain.Entities.BusinessRules
{
    public interface IBusinessRulesValidator<in T> where T : ICrudState
    {
        Task<BusinessRulesValidationResult> ValidateAsync(T instance);
    }
}
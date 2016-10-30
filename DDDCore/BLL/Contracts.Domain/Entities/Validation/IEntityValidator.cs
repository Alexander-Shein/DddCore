using System.Threading.Tasks;
using Contracts.Domain.Entities.Model;

namespace Contracts.Domain.Entities.Validation
{
    public interface IEntityValidator<in T> where T : ICrudState
    {
        Task<EntityValidationResult> ValidateAsync(T instance);
    }
}
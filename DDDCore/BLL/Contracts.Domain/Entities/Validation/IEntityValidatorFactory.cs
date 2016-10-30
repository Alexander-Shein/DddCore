using Contracts.Domain.Entities.Model;

namespace Contracts.Domain.Entities.Validation
{
    public interface IEntityValidatorFactory
    {
        IEntityValidator<T> GetEntityValidator<T>() where T : ICrudState;
    }
}
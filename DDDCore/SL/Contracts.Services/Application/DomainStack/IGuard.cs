using System.Threading.Tasks;
using Contracts.Domain.Entities.Validation;

namespace Contracts.Services.Application.DomainStack
{
    public interface IGuard
    {
        void NotNull(object obj);
        void DomainIsValid(params IValidation[] domains);
        Task DomainIsValidAsync(params IValidation[] domains);
    }
}

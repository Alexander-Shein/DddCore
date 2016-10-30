using System.Threading.Tasks;
using Contracts.Domain.Entities.BusinessRules;

namespace Contracts.Services.Application.DomainStack
{
    public interface IGuard
    {
        void NotNull(object obj, string message = "");
        Task DomainIsValidAsync(params IValidatable[] domains);
    }
}

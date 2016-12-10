using System.Threading.Tasks;
using DddCore.Contracts.Domain.Entities.BusinessRules;

namespace DddCore.Contracts.Services.Application.DomainStack
{
    public interface IGuard
    {
        void NotNull(object obj, string message = "");
        Task DomainIsValidAsync(params IValidatable[] domains);
    }
}

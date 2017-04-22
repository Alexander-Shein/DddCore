using System.Threading.Tasks;
using DddCore.Contracts.Domain.Entities.Model;
using DddCore.Contracts.Domain.Errors;

namespace DddCore.Contracts.Domain.Entities.BusinessRules
{
    public interface IBusinessRulesValidator<in T> where T : ICrudState
    {
        Task<OperationResult> ValidateAsync(T instance);
        OperationResult Validate(T instance);
    }
}
using System.Threading.Tasks;
using DddCore.Contracts.BLL.Domain.Entities.Model;
using DddCore.Contracts.BLL.Errors;

namespace DddCore.Contracts.BLL.Domain.Entities.BusinessRules
{
    public interface IBusinessRulesValidator<in T> where T : ICrudState
    {
        Task<OperationResult> ValidateAsync(T instance);
        OperationResult Validate(T instance);
    }
}
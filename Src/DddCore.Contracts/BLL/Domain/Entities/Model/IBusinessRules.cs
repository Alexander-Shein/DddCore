using System.Threading.Tasks;
using DddCore.Contracts.BLL.Errors;

namespace DddCore.Contracts.BLL.Domain.Entities.Model
{
    public interface IBusinessRules
    {
        Task<OperationResult> ValidateAsync(GraphDepth graphDepth = GraphDepth.AggregateRoot);
        OperationResult Validate(GraphDepth graphDepth = GraphDepth.AggregateRoot);
    }
}
using System.Threading.Tasks;
using DddCore.Contracts.BLL.Domain.Entities.Model;
using DddCore.Contracts.BLL.Errors;

namespace DddCore.Contracts.BLL.Domain.Entities
{
    public interface IEntity<TKey> : ICrudState, IIdentity<TKey>, IDomainEvents
    {
        void RaiseEvents();
        Task<OperationResult> ValidateAsync();
        OperationResult Validate();
    }
}
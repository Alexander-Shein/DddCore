using System.Threading.Tasks;

namespace Contracts.Dal.DomainStack
{
    public interface IUnitOfWork
    {
        Task<int> SaveAsync();
    }
}

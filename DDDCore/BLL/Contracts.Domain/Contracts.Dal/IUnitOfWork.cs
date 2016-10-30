using System.Threading;
using System.Threading.Tasks;

namespace Contracts.Dal
{
    public interface IUnitOfWork
    {
        void Save();

        Task<int> SaveAsync();

        Task<int> SaveAsync(CancellationToken cancellationToken);
    }
}

using System.Threading.Tasks;

namespace Contracts.Dal.DomainStack
{
    public interface IUnitOfWork
    {
        Task SaveAsync();
        void Save();
    }
}

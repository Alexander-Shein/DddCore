using System.Threading.Tasks;

namespace DddCore.Contracts.Dal.DomainStack
{
    public interface IUnitOfWork
    {
        Task SaveAsync();
        void Save();
    }
}

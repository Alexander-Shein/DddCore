using System.Threading.Tasks;

namespace DddCore.Contracts.Services.Application.DomainStack.Crud
{
    public interface IDelete<in TKey>
    {
        Task DeleteAsync(TKey key);
    }
}
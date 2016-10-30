using System.Threading.Tasks;

namespace Contracts.Services.Application.DomainStack.Crud
{
    public interface IDelete<in TKey>
    {
        Task DeleteAsync(TKey key);
    }
}
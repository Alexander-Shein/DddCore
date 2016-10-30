using System.Threading.Tasks;

namespace Contracts.Services.Application.DomainStack
{
    public interface IDelete<in TKey>
    {
        Task DeleteAsync(TKey key);
    }
}
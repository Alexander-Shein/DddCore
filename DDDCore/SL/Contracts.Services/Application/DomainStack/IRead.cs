using System.Threading.Tasks;

namespace Contracts.Services.Application.DomainStack
{
    public interface IRead<T, in TKey>
    {
        Task<T> ReadAsync(TKey key, string[] includes = null);
    }
}
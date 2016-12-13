using System.Threading.Tasks;

namespace DddCore.Contracts.Services.Application.DomainStack.Crud
{
    public interface IRead<T, in TKey>
    {
        Task<T> ReadAsync(TKey key, string[] includes = null);
    }
}
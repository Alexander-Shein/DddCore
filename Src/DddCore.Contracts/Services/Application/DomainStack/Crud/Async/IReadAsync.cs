using System.Threading.Tasks;

namespace DddCore.Contracts.Services.Application.DomainStack.Crud.Async
{
    public interface IReadAsync<TViewModel, in TKey>
    {
        /// <summary>
        /// Read ViewModel by key. Includes can contain additional information that we need to return.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="includes"></param>
        /// <returns></returns>
        Task<TViewModel> ReadAsync(TKey key, string[] includes = null);
    }
}
namespace DddCore.Contracts.SL.Services.Application.RestFull
{
    public interface IGet<out TViewModel, in TKey> where TViewModel : IViewModel
    {
        /// <summary>
        /// GET /cars/{carId} HTTP/1.1.
        /// Reads entity by key.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="includes">Includes can contain additional information that we need to return.</param>
        /// <returns></returns>
        TViewModel Get(TKey key, string[] includes = null, string[] extends = null);
    }
}
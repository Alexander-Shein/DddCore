namespace DddCore.Contracts.SL.Services.Application.DomainStack.Crud
{
    public interface IRead<out TViewModel, in TKey>
    {
        /// <summary>
        /// GET /cars/{carId} HTTP/1.1.
        /// Reads entity by key.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="includes">Includes can contain additional information that we need to return.</param>
        /// <returns></returns>
        TViewModel Read(TKey key, string[] includes = null);
    }
}
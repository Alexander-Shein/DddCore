namespace DddCore.Contracts.SL.Services.Application.DomainStack.Crud
{
    public interface IDelete<in TKey>
    {
        /// <summary>
        /// DELETE /cars/{carId} HTTP/1.1.
        /// Deletes entity by key.
        /// </summary>
        /// <param name="key"></param>
        void Delete(TKey key);
    }
}

namespace DddCore.Contracts.Services.Application.DomainStack.Crud
{
    public interface IRead<out TViewModel, in TKey>
    {
        /// <summary>
        /// Read ViewModel by key. Includes can contain additional information that we need to return.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="includes"></param>
        /// <returns></returns>
        TViewModel Read(TKey key, string[] includes = null);
    }
}
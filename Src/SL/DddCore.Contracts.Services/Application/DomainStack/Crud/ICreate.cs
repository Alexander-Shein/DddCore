namespace DddCore.Contracts.Services.Application.DomainStack.Crud
{
    public interface ICreate<out TViewModel, in TInputModel>
    {
        /// <summary>
        /// Gets an InputModel and returns ViewModel. InputModel has no Id property, ViewModel does.
        /// </summary>
        /// <param name="im"></param>
        /// <returns></returns>
        TViewModel Create(TInputModel im);
    }
}

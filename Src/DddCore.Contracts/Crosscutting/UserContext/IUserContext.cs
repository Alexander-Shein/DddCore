namespace DddCore.Contracts.Crosscutting.UserContext
{
    public interface IUserContext<out TKey>
    {
        /// <summary>
        /// ClaimTypes.NameIdentifier from current Identity
        /// </summary>
        TKey Id { get; }

        /// <summary>
        /// Identity.Name
        /// </summary>
        string UserName { get; }

        /// <summary>
        /// Identity.IsAuthenticated
        /// </summary>
        bool IsAuthenticated { get; }
    }
}
namespace DddCore.Contracts.Crosscutting.UserContext
{
    public interface IUserContext<out TKey>
    {
        TKey Id { get; }
        string UserName { get; }
        bool IsAuthenticated { get; }
    }
}

namespace DddCore.Contracts.BLL.Domain.Models
{
    public interface IEntity<out TKey> : IIdentity<TKey>
    {
    }
}
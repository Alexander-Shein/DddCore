namespace DddCore.Contracts.BLL.Domain.Models
{
    public interface IIdentity<out TKey>
    {
        TKey Id { get; }
    }
}
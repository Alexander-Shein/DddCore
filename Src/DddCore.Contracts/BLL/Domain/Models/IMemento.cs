namespace DddCore.Contracts.BLL.Domain.Models
{
    public interface IMemento<TKey>
    {
        TKey Id { get; set; }
    }
}
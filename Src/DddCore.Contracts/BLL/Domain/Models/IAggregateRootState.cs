namespace DddCore.Contracts.BLL.Domain.Models
{
    public interface IAggregateRootState<TKey>
    {
        TKey Id { get; set; }
    }
}
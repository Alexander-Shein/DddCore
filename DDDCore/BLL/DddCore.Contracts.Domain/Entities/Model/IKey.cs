namespace DddCore.Contracts.Domain.Entities.Model
{
    public interface IKey<TKey>
    {
        TKey Id { get; set; }
    }
}
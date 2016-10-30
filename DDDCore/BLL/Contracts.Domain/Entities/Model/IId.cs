namespace Contracts.Domain.Entities.Model
{
    public interface IId<TKey>
    {
        TKey Id { get; set; }
    }
}
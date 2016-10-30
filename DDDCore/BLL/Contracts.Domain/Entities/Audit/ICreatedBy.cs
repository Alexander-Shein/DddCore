namespace Contracts.Domain.Entities.Audit
{
    public interface ICreatedBy<TKey>
    {
        TKey CreatedBy { get; set; }
    }
}
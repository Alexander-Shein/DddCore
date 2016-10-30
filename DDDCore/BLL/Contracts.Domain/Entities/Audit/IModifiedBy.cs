namespace Contracts.Domain.Entities.Audit
{
    public interface IModifiedBy<TKey>
    {
        TKey ModifiedBy { get; set; }
    }
}
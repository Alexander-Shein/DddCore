namespace DddCore.Contracts.Domain.Entities.Audit.By
{
    public interface IModifiedBy<TKey>
    {
        TKey ModifiedBy { get; set; }
    }
}
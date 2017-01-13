namespace DddCore.Contracts.Domain.Entities.Audit.By
{
    public interface ICreatedBy<TKey>
    {
        TKey CreatedBy { get; set; }
    }
}
namespace DddCore.Contracts.Domain.Entities.Model
{
    public interface IIdentity<TKey>
    {
        TKey Id { get; set; }
    }
}
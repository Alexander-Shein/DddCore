namespace DddCore.Contracts.BLL.Domain.Entities
{
    public interface IIdentity<TKey>
    {
        TKey Id { get; set; }
    }
}
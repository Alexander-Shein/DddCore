namespace DddCore.Contracts.BLL.Domain.Entities.Model
{
    public interface IIdentity<TKey>
    {
        TKey Id { get; set; }
    }
}
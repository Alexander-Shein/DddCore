namespace DddCore.Contracts.BLL.Domain.Models.Audit.By
{
    /// <summary>
    /// Use when you need to audit a person who created the entity
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    public interface ICreatedBy<TKey>
    {
        TKey CreatedBy { get; set; }
    }
}
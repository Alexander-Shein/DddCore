namespace Contracts.Domain.Entities.Model
{
    public interface IDeletable
    {
        bool IsDeleted { get; set; }
    }
}
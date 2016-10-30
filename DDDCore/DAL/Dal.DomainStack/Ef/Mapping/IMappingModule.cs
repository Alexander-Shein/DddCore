using System.Data.Entity;

namespace Dal.DomainStack.Ef.Mapping
{
    public interface IMappingModule
    {
        void Map(DbModelBuilder modelBuilder);
    }
}
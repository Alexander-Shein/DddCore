using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Threading.Tasks;
using Contracts.Dal;
using Contracts.Domain.Entities.Model;
using Crosscutting.Infrastructure.Configuration;
using Dal.DomainStack.Ef.Mapping;

namespace Dal.DomainStack.Ef.Context
{
    public class DataContext : DbContext, IDataContext
    {
        static DataContext()
        {
            Database.SetInitializer<DataContext>(null);
        }

        public DataContext() : base(DalConsts.ConnectionString.Oltp)
        {
            DbContextInitializer();
        }

        public void SetLazyLoading(bool onOff)
        {
            Configuration.LazyLoadingEnabled = onOff;
        }

        public void SyncEntityState<T>(T entity) where T : class, ICrudState
        {
            Entry(entity).State = CrudStateHelper.ConvertState(entity.CrudState);
        }

        public DbSet<TEntity> Set<TEntity>() where TEntity : class
        {
            return base.Set<TEntity>();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();

            var modules = AssemblyUtility.GetInstances<IMappingModule>();

            foreach (var module in modules)
            {
                module.Install(new MappingBuilder(modelBuilder));
            }
        }

        void DbContextInitializer()
        {
            Database.Initialize(false);
            SetLazyLoading(false);
            Configuration.ValidateOnSaveEnabled = false;
        }

        public void Save()
        {
            this.BulkSaveChanges();
        }

        public async Task SaveAsync()
        {
            await this.BulkSaveChangesAsync();
        }
    }
}

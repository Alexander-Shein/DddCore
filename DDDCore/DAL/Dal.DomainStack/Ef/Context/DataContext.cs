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

        #region Public Methods

        public void SetLazyLoading(bool onOff)
        {
            Configuration.LazyLoadingEnabled = onOff;
        }

        public void SyncEntityState<T>(T entity) where T : class, ICrudState
        {
            if (!IsAttached(entity))
            {
                Entry(entity).State = CrudStateHelper.ConvertState(entity.CrudState);
            }
        }

        public DbSet<TEntity> Set<TEntity>() where TEntity : class
        {
            return base.Set<TEntity>();
        }

        public void Save()
        {
            this.SaveChanges();
        }

        public async Task SaveAsync()
        {
            await this.BulkSaveChangesAsync();
        }

        #endregion

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

        #region Private Methods

        void DbContextInitializer()
        {
            Database.Initialize(false);
            SetLazyLoading(false);
            Configuration.ValidateOnSaveEnabled = false;
        }

        bool IsAttached<T>(T entity) where T : class, ICrudState
        {
            return Entry(entity).State != EntityState.Detached;
        }

        #endregion
    }
}

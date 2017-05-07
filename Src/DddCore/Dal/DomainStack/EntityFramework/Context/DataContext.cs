using System.Linq;
using System.Threading.Tasks;
using DddCore.Contracts.BLL.Domain.Entities.Model;
using DddCore.Contracts.DAL;
using DddCore.Contracts.DAL.DomainStack;
using DddCore.Crosscutting;
using DddCore.DAL.DomainStack.EntityFramework.Mapping;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace DddCore.DAL.DomainStack.EntityFramework.Context
{
    public class DataContext : DbContext, IDataContext, IUnitOfWork
    {
        #region Private Members

        readonly ConnectionStrings connectionStrings;

        #endregion

        public DataContext(
            IOptions<ConnectionStrings> connectionStrings)
        {
            this.connectionStrings = connectionStrings.Value;
        }

        #region Public Methods

        public void SyncEntityState<T>(T entity) where T : class, ICrudState
        {
            if (!IsAttached(entity))
            {
                Entry(entity).State = CrudStateHelper.ConvertState(entity.CrudState);
            }
        }

        public void Save()
        {
            SaveChanges();
        }

        public async Task SaveAsync()
        {
            await SaveChangesAsync();
        }

        #endregion

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(connectionStrings.Oltp);
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var modules = AssemblyUtility.GetInstancesOf<IMappingModuleInstaller>().ToArray();
            if (!modules.Any()) return;

            var builder = new DddCoreModelBuilder(modelBuilder);
            modules.Do(x => x.Install(builder));
        }

        #region Private Methods

        bool IsAttached<T>(T entity) where T : class, ICrudState
        {
            return Entry(entity).State != EntityState.Detached;
        }

        #endregion
    }
}

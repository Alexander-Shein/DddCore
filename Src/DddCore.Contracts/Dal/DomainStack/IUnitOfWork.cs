using System.Threading.Tasks;

namespace DddCore.Contracts.DAL.DomainStack
{
    public interface IUnitOfWork
    {
        /// <summary>
        /// Save changes to DataBase in single transaction
        /// </summary>
        /// <returns></returns>
        Task SaveAsync();

        /// <summary>
        /// Save changes to DataBase in single transaction
        /// </summary>
        /// <returns></returns>
        void Save();
    }
}

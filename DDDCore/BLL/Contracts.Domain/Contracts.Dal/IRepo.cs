using System;
using System.Threading.Tasks;
using Contracts.Domain.Entity;

namespace Contracts.Dal
{
    public interface IRepo<T> where T : class, IAggregateRootEntityBase
    {
        void Insert(T entity);
        void Update(T entity);
        void Delete(Guid id);

        T Read(Guid id);
        Task<T> ReadAsync(Guid id);
    }
}

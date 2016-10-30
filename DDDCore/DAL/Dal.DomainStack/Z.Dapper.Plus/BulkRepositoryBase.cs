using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.Dal.DomainStack;
using Contracts.Domain.Entities;

namespace Dal.DomainStack.Z.Dapper.Plus
{
    public class BulkRepositoryBase<T, TKey> : IBulkRepository<T, TKey> where T : class, IAggregateRootEntity<TKey>
    {
        public Task BulkPersistEntityGraphAsync(IEnumerable<T> domains)
        {
            //Walk throught object
            //Create Expressions to get IEntity properties
            //sort to collections
            //save

            throw new NotImplementedException();
        }
    }
}

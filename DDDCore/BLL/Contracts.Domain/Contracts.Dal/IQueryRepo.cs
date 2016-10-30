using System.Collections.Generic;

namespace Contracts.Dal
{
    public interface IQueryRepo
    {
        IEnumerable<T> GetFilteredList<T>(string sql, object parameters = null) where T : class;
        T GetFilteredClass<T>(string sql, object parameters = null) where T : class;
    }
}
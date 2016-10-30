using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Contracts.Dal;
using Contracts.Dal.QueryStack;
using Dapper;

namespace Dal.QueryStack.Dapper
{
    public abstract class QueryRepositoryBase : IQueryRepository
    {
        #region Private Members

        string connectionString;

        #endregion

        #region Protected Methods

        protected string ConnectionString => connectionString ?? (connectionString = ConfigurationManager.ConnectionStrings[DalConsts.ConnectionString.Oltp].ToString());

        protected async Task<IEnumerable<T>> GetFilteredListAsync<T>(string sql, object parameters = null)
        {
            using (var dbCon = GetDbConnection())
            {
                await dbCon.OpenAsync();
                var result = parameters == null ? dbCon.QueryAsync<T>(sql) : dbCon.QueryAsync<T>(sql, parameters);
                return await result;
            }
        }

        protected async Task<T> GetFilteredAsync<T>(string sql, object parameters = null)
        {
            using (var dbCon = GetDbConnection())
            {
                await dbCon.OpenAsync();
                var result = parameters == null ? dbCon.QueryFirstAsync<T>(sql) : dbCon.QueryFirstAsync<T>(sql, parameters);
                return await result;
            }
        }

        protected SqlConnection GetDbConnection()
        {
            var connection = new SqlConnection(ConnectionString);
            return connection;
        }

        protected int GetOffset(int page, int pageSize)
        {
            var offset = (page - 1) * pageSize;
            return offset;
        }

        #endregion
    }
}

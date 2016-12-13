using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using DddCore.Contracts.Dal;
using DddCore.Contracts.Dal.QueryStack;
using Microsoft.Extensions.Options;

namespace DddCore.Dal.QueryStack.Dapper
{
    public abstract class QueryRepositoryBase : IQueryRepository
    {
        #region Private Members

        protected readonly ConnectionStrings ConnectionStrings;

        protected QueryRepositoryBase(IOptions<ConnectionStrings> connectionStrings)
        {
            ConnectionStrings = connectionStrings.Value;
        }

        #endregion

        #region Protected Methods

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

        protected async Task<T1> GetFilteredAsync<T1, T2>(string sql, Func<T1, T2, T1> map,  object parameters = null)
        {
            using (var dbCon = GetDbConnection())
            {
                await dbCon.OpenAsync();

                var result = await (parameters == null ? dbCon.QueryAsync(sql, map) : dbCon.QueryAsync(sql, map, parameters));
                return result.FirstOrDefault();
            }
        }

        protected async Task<Tuple<IEnumerable<T1>, IEnumerable<T2>>> GetMultipleResult<T1, T2>(string sql, object parameters = null)
        {
            using (var dbCon = GetDbConnection())
            {
                await dbCon.OpenAsync();

                var multiResult = await (parameters == null ? dbCon.QueryMultipleAsync(sql) : dbCon.QueryMultipleAsync(sql, parameters));

                var result =
                    Tuple.Create(
                        await multiResult.ReadAsync<T1>(),
                        await multiResult.ReadAsync<T2>());

                return result;
            }
        }

        protected async Task<Tuple<IEnumerable<T1>, IEnumerable<T2>, IEnumerable<T3>>> GetMultipleResult<T1, T2, T3>(string sql, object parameters = null)
        {
            using (var dbCon = GetDbConnection())
            {
                await dbCon.OpenAsync();

                var multiResult = await (parameters == null ? dbCon.QueryMultipleAsync(sql) : dbCon.QueryMultipleAsync(sql, parameters));

                var result =
                    Tuple.Create(
                        await multiResult.ReadAsync<T1>(),
                        await multiResult.ReadAsync<T2>(),
                        await multiResult.ReadAsync<T3>());

                return result;
            }
        }

        protected SqlConnection GetDbConnection()
        {
            var connection = new SqlConnection(ConnectionStrings.ReadOnly);
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

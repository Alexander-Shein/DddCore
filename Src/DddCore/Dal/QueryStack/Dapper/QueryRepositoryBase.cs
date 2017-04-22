using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using DddCore.Contracts.DAL;
using DddCore.Contracts.DAL.QueryStack;
using Microsoft.Extensions.Options;

namespace DddCore.DAL.QueryStack.Dapper
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

        protected async Task<IEnumerable<T>> GetListAsync<T>(string sql, object parameters = null, CommandType commandType = CommandType.Text)
        {
            using (var dbCon = GetDbConnection())
            {
                await dbCon.OpenAsync();

                var result = await dbCon.QueryAsync<T>(sql, parameters, commandType: commandType);
                return result;
            }
        }

        protected async Task<T> GetAsync<T>(string sql, object parameters = null, CommandType commandType = CommandType.Text)
        {
            using (var dbCon = GetDbConnection())
            {
                await dbCon.OpenAsync();

                var result = dbCon.QueryFirstAsync<T>(sql, parameters, commandType: commandType);
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

        protected async Task<(IEnumerable<T1>, IEnumerable<T2>)> GetMultipleResultAsync<T1, T2>(string sql, object parameters = null)
        {
            using (var dbCon = GetDbConnection())
            {
                await dbCon.OpenAsync();

                var multiResult = await (parameters == null ? dbCon.QueryMultipleAsync(sql) : dbCon.QueryMultipleAsync(sql, parameters));

                return (await multiResult.ReadAsync<T1>(), await multiResult.ReadAsync<T2>());
            }
        }

        protected async Task<(IEnumerable<T1>, IEnumerable<T2>, IEnumerable<T3>)> GetMultipleResultAsync<T1, T2, T3>(string sql, object parameters = null)
        {
            using (var dbCon = GetDbConnection())
            {
                await dbCon.OpenAsync();

                var multiResult = await (parameters == null ? dbCon.QueryMultipleAsync(sql) : dbCon.QueryMultipleAsync(sql, parameters));

                var result =
                (
                    await multiResult.ReadAsync<T1>(),
                    await multiResult.ReadAsync<T2>(),
                    await multiResult.ReadAsync<T3>()
                );

                return result;
            }
        }

        /// <summary>
        /// Gets ReadOnly connection string from injected IOptions<ConnectionStrings> option.
        /// </summary>
        /// <returns></returns>
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

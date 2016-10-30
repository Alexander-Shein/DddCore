using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Contracts.Dal;
using Dapper;

namespace Dal.Dapper
{
    public abstract class QueryRepoBase : IQueryRepo
    {
        public string DbConnectionStr { get; set; }

        protected QueryRepoBase(string connectionName)
        {
            DbConnectionStr = ConfigurationManager.ConnectionStrings[connectionName].ToString();
        }

        public IEnumerable<T> GetFilteredList<T>(string sql, object parameters = null) where T : class
        {
            IEnumerable<T> result;
            using (var dbCon = GetDbConnection())
            {
                dbCon.Open();
                result = parameters == null ? dbCon.Query<T>(sql) : dbCon.Query<T>(sql, parameters);
                dbCon.Close();
            }
            return result;

        }

        public T GetFilteredClass<T>(string sql, object parameters = null) where T : class
        {
            T result;
            using (var dbCon = GetDbConnection())
            {
                dbCon.Open();
                result = parameters == null ? dbCon.Query<T>(sql).FirstOrDefault() : dbCon.Query<T>(sql, parameters).FirstOrDefault();
                dbCon.Close();
            }
            return result;
        }

        IDbConnection GetDbConnection()
        {
            var connection = new SqlConnection(DbConnectionStr);
            return connection;
        }
    }
}

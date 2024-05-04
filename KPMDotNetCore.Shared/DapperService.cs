using System.Data;
using System.Data.SqlClient;
using Dapper;
namespace KPMDotNetCore.Shared
{
    public class DapperService
    {
        private readonly string _connectionstring;
        public DapperService(string connectionstring) 
        {
            _connectionstring = connectionstring;
        }

        public List<M> Query <M>(string query ,object? param=null) 
        {
            using IDbConnection db = new SqlConnection(_connectionstring);
            //if (param != null)
            //{
            //    var lst = db.Query<M>(query, param).ToList();
            //}
            //else
            //{
            //    var lst = db.Query<M>(query).ToList();
            //}
            var lst=db.Query<M>(query,param).ToList();

            return lst;
        }
        public M QueryFirstOrDefault <M>(string query, object? param = null)
        {
            using IDbConnection db = new SqlConnection(_connectionstring);
   
            var item = db.Query<M>(query, param).FirstOrDefault();

            return item!;
        }

        public int Execute(string query ,object? param=null)
        {
            using IDbConnection db = new SqlConnection(_connectionstring);
            var result=db.Execute(query,param);
            return result;
        }

    }
}

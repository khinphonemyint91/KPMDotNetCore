using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace KPMDotNetCore.Shared
{
    public class AdoDotNetService
    {

        private readonly string _connectionstring;
        public AdoDotNetService(string connectionstring)
        {
            _connectionstring = connectionstring;
        }
        public List<T> Query<T>(string query, params AdoDotNetParameter[]? parameters)
        {
            SqlConnection connection = new SqlConnection(_connectionstring);
            connection.Open();

            SqlCommand cmd = new SqlCommand(query, connection);
            if(parameters is not null && parameters.Length> 0)
            {  
                //Way 1
                //foreach (var item in parameters)
                //{
                //    cmd.Parameters.AddWithValue(item.Name, item.Value);
                //}
                //Way 2
                //cmd.Parameters.AddRange(parameters.Select(item => new SqlParameter(item.Name,item.Value)).ToArray());
                //Way 3
                var parameterArray = parameters.Select(item => new SqlParameter(item.Name, item.Value)).ToArray();
                cmd.Parameters.AddRange(parameterArray);
            }
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sqlDataAdapter.Fill(dt);

            connection.Close();
            string json=JsonConvert.SerializeObject(dt);  //C# to json
            List<T> lst = JsonConvert.DeserializeObject<List<T>>(json)!; //json to C# object
            return lst;
        }

        public T QueryFirstOrDefault<T>(string query, params AdoDotNetParameter[]? parameters)
        {
            SqlConnection connection = new SqlConnection(_connectionstring);
            connection.Open();

            SqlCommand cmd = new SqlCommand(query, connection);
            if (parameters is not null && parameters.Length > 0)
            {
                cmd.Parameters.AddRange(parameters.Select(item => new SqlParameter(item.Name,item.Value)).ToArray());
              
            }
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sqlDataAdapter.Fill(dt);

            connection.Close();

            if (dt.Rows.Count == 0)
            {
                // No data found, return null
                return default;
            }
            string json = JsonConvert.SerializeObject(dt);  //C# to json
            List<T> lst = JsonConvert.DeserializeObject<List<T>>(json)!; //json to C# object
            return lst[0];
        }
        public int Execute (string query, params AdoDotNetParameter[]? parameters)
        {
            SqlConnection connection = new SqlConnection(_connectionstring);
            connection.Open();

            SqlCommand cmd = new SqlCommand(query, connection);
            if (parameters is not null && parameters.Length > 0)
            {
                var parameterArray = parameters.Select(item => new SqlParameter(item.Name, item.Value)).ToArray();
                cmd.Parameters.AddRange(parameterArray);
            }
            var result=cmd.ExecuteNonQuery();
            
            connection.Close();
            
            return result;
        }
    }
    public class AdoDotNetParameter
    {
        public AdoDotNetParameter() { }
        public AdoDotNetParameter(string name, object value)
        { 
            Name=name; Value=value;
        }
        public string Name { get; set; }
        public object Value { get; set; }
    }
}

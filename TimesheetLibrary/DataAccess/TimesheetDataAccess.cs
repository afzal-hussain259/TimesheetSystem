using Dapper;
using Microsoft.Data.SqlClient;
using System.Data;

namespace TimesheetLibrary.DataAccess
{
    public class TimesheetDataAccess : ITimesheetDataAccess
    {
        public string GetConnectionString()
        {
            return @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=TimesheetDB;Integrated Security=True;Connect Timeout=60;Encrypt=False";
        }

        public List<T> LoadData<T>(string sql)
        {
            using (IDbConnection cnn = new SqlConnection(GetConnectionString()))
            {
                return cnn.Query<T>(sql).ToList();
            }
        }

        public int SavaData<T>(string sql, T data)
        {
            using (IDbConnection cnn = new SqlConnection(GetConnectionString()))
            {
                return cnn.Execute(sql, data);
            }
        }
    }
}

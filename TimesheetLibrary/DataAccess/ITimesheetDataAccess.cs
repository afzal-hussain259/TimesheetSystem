using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimesheetLibrary.DataAccess
{
    public interface ITimesheetDataAccess
    {
        string GetConnectionString();
        List<T> LoadData<T>(string sql);
        int SavaData<T>(string sql, T data);
    }
}

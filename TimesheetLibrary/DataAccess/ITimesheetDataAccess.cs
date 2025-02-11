namespace TimesheetLibrary.DataAccess
{
    public interface ITimesheetDataAccess
    {
        string GetConnectionString();
        List<T> LoadData<T>(string sql);
        int SavaData<T>(string sql, T data);
    }
}

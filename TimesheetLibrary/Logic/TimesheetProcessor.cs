using TimesheetLibrary.DataAccess;
using TimesheetLibrary.Models;

namespace TimesheetLibrary.Logic
{
    public class TimesheetProcessor : ITimesheetProcessor
    {
        private readonly ITimesheetDataAccess _dataAccess;

        public TimesheetProcessor(ITimesheetDataAccess dataAccess)
        {
            _dataAccess = dataAccess;
        }
        public int CreateTimesheetEntry(string userName, string date, string project, string descriptionOfTasks, decimal hoursWorked)
        {
            TimesheetDBModel data = new TimesheetDBModel
            {
                UserName = userName,
                Date = date,
                Project = project,
                DescriptionOfTasks = descriptionOfTasks,
                HoursWorked = hoursWorked
            };

            // Insert new timesheet entry into database.

            string sql = @"insert into dbo.Timesheet (UserName, Date, Project, DescriptionOfTasks, HoursWorked)
                            values (@UserName, @Date, @Project, @DescriptionOfTasks, @HoursWorked);";

            return _dataAccess.SavaData(sql, data);
        }

        public List<TimesheetDBModel> LoadTimesheet()
        {
            // Return all user timesheet data from database.

            string sql = @"select UserName, Date, Project, DescriptionOfTasks, HoursWorked
                            from dbo.Timesheet;";

            var data = _dataAccess.LoadData<TimesheetDBModel>(sql);

            foreach (var item in data)
            {
                item.TotalHoursForTheDay = data.Where(d => d.UserName == item.UserName && d.Date == item.Date).Sum(s => s.HoursWorked);
            }

            return data;
        }
    }
}

using TimesheetLibrary.Models;

namespace TimesheetLibrary.Logic
{
    public interface ITimesheetProcessor
    {
        int CreateTimesheetEntry(string userName, string date, string project, string descriptionOfTasks, decimal hoursWorked);
        List<TimesheetDBModel> LoadTimesheet();
    }
}

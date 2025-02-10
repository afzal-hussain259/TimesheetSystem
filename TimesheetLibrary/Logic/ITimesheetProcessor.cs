using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimesheetLibrary.Models;

namespace TimesheetLibrary.Logic
{
    public interface ITimesheetProcessor
    {
        int CreateTimesheetEntry(string userName, string date, string project, string descriptionOfTasks, decimal hoursWorked);
        List<TimesheetDBModel> LoadTimesheet();
    }
}

using System.Diagnostics;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using TimesheetLibrary.Logic;
using TimesheetSystem.Models;

namespace TimesheetSystem.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ITimesheetProcessor _timesheetProcessor;

        public HomeController(ILogger<HomeController> logger, ITimesheetProcessor timesheetProcessor)
        {
            _logger = logger;
            _timesheetProcessor = timesheetProcessor;
        }

        public IActionResult Index()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public ActionResult Timesheet()
        {
            ViewBag.Message = "Submit Timesheet";

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Timesheet(TimesheetModel timesheet)
        {
            if (ModelState.IsValid)
            {
                int recordsCreated = _timesheetProcessor.CreateTimesheetEntry(timesheet.UserName, timesheet.Date, timesheet.Project, timesheet.DescriptionOfTasks, timesheet.HoursWorked);
                if (recordsCreated != 0)
                {
                    ViewBag.Msg = "The User " + timesheet.UserName + " has successfully added " + timesheet.HoursWorked + " hours to project " + timesheet.Project;
                }
            }

            return View();
        }

        public ActionResult ExportTimesheetToCSV()
        {
            var builder = new StringBuilder();
            builder.AppendLine("UserName,Date,Project,DescriptionOfTasks,HoursWorked,TotalHoursForTheDay");
            var data = _timesheetProcessor.LoadTimesheet();

            foreach (var row in data)
            {
                builder.AppendLine($"{row.UserName},{row.Date},{row.Project},{row.DescriptionOfTasks},{row.HoursWorked},{row.TotalHoursForTheDay}");
            }

            return File(Encoding.UTF8.GetBytes(builder.ToString()), "text/csv", $"TimesheetExport-{DateTime.Now.ToFileTime()}.csv");

        }
    }
}

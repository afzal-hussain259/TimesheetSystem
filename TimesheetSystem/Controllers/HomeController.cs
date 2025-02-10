using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using TimesheetSystem.Models;

namespace TimesheetSystem.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
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
                ViewBag.Msg = "The User " + timesheet.UserName + " has successfully added " + timesheet.HoursWorked + " hours to project " + timesheet.Project;
            }

            return View();
        }
    }
}

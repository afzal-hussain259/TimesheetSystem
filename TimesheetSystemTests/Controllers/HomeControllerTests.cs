using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using TimesheetLibrary.Logic;
using TimesheetLibrary.Models;
using TimesheetSystem.Controllers;
using TimesheetSystem.Models;

namespace TimesheetSystemTests.Controllers
{
    public class HomeControllerTests
    {
        private Mock<ITimesheetProcessor> _timesheetProcessorMock;
        private HomeController _homeController;

        public HomeControllerTests()
        {
            _timesheetProcessorMock = new Mock<ITimesheetProcessor>();
            _homeController = new HomeController(_timesheetProcessorMock.Object);
        }

        [Fact]
        public void CreateTimesheet_Success()
        {
            // Arrange
            var request = GetRequest();

            _timesheetProcessorMock.Setup(x => x.CreateTimesheetEntry(request.UserName, request.Date, request.Project, request.DescriptionOfTasks, request.HoursWorked))
            .Returns(1);

            // Act
            var actual = _homeController.Timesheet(request);

            // Assert
            Assert.NotNull(actual);
            actual.Should().BeOfType(typeof(ViewResult));
        }

        [Fact]
        public void ExportTimesheetToCSV_Success()
        {
            // Arrange
            var request = GetRequest();
            var expectedCsvFormat = "UserName,Date,Project,DescriptionOfTasks,HoursWorked,TotalHoursForTheDay\r\nTest,10/02/2025,Test,Test,5,0\r\nTest,10/02/2025,Test,Test,5,0\r\n";

            _timesheetProcessorMock.Setup(x => x.LoadTimesheet())
            .Returns(SqlGetDbResponse);

            // Act
            var actual = _homeController.ExportTimesheetToCSV();

            var result = actual as FileContentResult;
            byte[] buffer = result.FileContents;
            string csvContent = System.Text.Encoding.UTF8.GetString(buffer, 0, buffer.Length);

            // Assert
            Assert.NotNull(actual);
            actual.Should().BeOfType(typeof(FileContentResult));
            actual.ContentType.Should().Be("text/csv");
            csvContent.Should().Be(expectedCsvFormat);
        }

        private TimesheetModel GetRequest()
        {
            return new TimesheetModel()
            {
                UserName = "Test",
                Date = "10/02/2025",
                Project = "Test",
                DescriptionOfTasks = "Test",
                HoursWorked = 5
            };
        }

        private List<TimesheetDBModel> SqlGetDbResponse()
        {
            return new List<TimesheetDBModel>()
            {
                new TimesheetDBModel
                {
                 Id = 1,
                 UserName = "Test",
                 Date = "10/02/2025",
                 Project = "Test",
                 DescriptionOfTasks = "Test",
                 HoursWorked = 5
                },
                new TimesheetDBModel
                {
                 Id = 2,
                 UserName = "Test",
                 Date = "10/02/2025",
                 Project = "Test",
                 DescriptionOfTasks = "Test",
                 HoursWorked = 5
                },
            };
        }
    }
}

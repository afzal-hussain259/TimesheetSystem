using Moq;
using TimesheetLibrary.DataAccess;
using TimesheetLibrary.Logic;
using TimesheetLibrary.Models;

namespace TimesheetSystemTests.Logic
{
    public class TimesheetProcessorTests
    {
        private Mock<ITimesheetDataAccess> _dataAccessMock;
        private TimesheetProcessor _processor;

        public TimesheetProcessorTests()
        {
            _dataAccessMock = new Mock<ITimesheetDataAccess>();
            _processor = new TimesheetProcessor(_dataAccessMock.Object);
        }

        [Fact]
        public void CreateTimesheetEntry_Success()
        {
            // Arrange
            var dataAccessResponse = SqlGetDbResponse();
            var data = GetRequest();
            var expected = 1;

            _dataAccessMock.Setup(x => x.SavaData(It.IsAny<string>(), It.IsAny<TimesheetDBModel>()))
                .Returns(1);

            // Act
            var actual = _processor.CreateTimesheetEntry(data.UserName, data.Date, data.Project, data.DescriptionOfTasks, data.HoursWorked);

            // Assert
            Assert.IsType<int>(actual);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void LoadTimesheet_Success()
        {
            // Arrange
            var dataAccessResponse = SqlGetDbResponse();
            var data = GetRequest();
            var expected = new List<TimesheetDBModel>()
            {
                new TimesheetDBModel
                {
                 Id = 1,
                 UserName = "Test",
                 Date = "10/02/2025",
                 Project = "Test",
                 DescriptionOfTasks = "Test",
                 HoursWorked = 5,
                 TotalHoursForTheDay = 10
                },
                new TimesheetDBModel
                {
                 Id = 2,
                 UserName = "Test",
                 Date = "10/02/2025",
                 Project = "Test",
                 DescriptionOfTasks = "Test",
                 HoursWorked = 5,
                 TotalHoursForTheDay = 10
                },
            };

            _dataAccessMock.Setup(x => x.LoadData<TimesheetDBModel>(It.IsAny<string>())).Returns(dataAccessResponse);

            // Act
            var actual = _processor.LoadTimesheet();

            // Assert
            Assert.Equivalent(expected, actual);

        }


        private TimesheetDBModel GetRequest()
        {
            return new TimesheetDBModel()
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

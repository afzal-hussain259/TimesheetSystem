namespace TimesheetLibrary.Models
{
    public class TimesheetDBModel
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Date { get; set; }
        public string Project { get; set; }
        public string DescriptionOfTasks { get; set; }
        public decimal HoursWorked { get; set; }
        public decimal TotalHoursForTheDay { get; set; }
    }
}

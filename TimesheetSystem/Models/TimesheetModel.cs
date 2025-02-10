using System.ComponentModel.DataAnnotations;

namespace TimesheetSystem.Models
{
    public class TimesheetModel
    {
        [Required(ErrorMessage = "User name cannot be empty")]
        [Display(Name = "User Name")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Date cannot be empty")]
        [DataType(DataType.Date)]
        public string Date { get; set; }
        [Required(ErrorMessage = "Project cannot be empty")]
        public string Project { get; set; }
        [Required(ErrorMessage = "Description cannot be empty")]
        [Display(Name = "Description of task")]
        public string DescriptionOfTasks { get; set; }
        [Required(ErrorMessage = "Hours worked cannot be empty")]
        [Display(Name = "Hours Worked")]
        [Range(0, 24, ErrorMessage = "Invalid number of hours")]
        public decimal HoursWorked { get; set; }
    }
}

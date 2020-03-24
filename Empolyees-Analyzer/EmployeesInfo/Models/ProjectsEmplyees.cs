using System;

namespace EmployeesInfo.Models
{
    public class ProjectsEmplyees
    {
        public int EmployeeId { get; set; }
        public int ProjectId { get; set; }
        //public DateTime? DateTo { get; set; }
        //public DateTime DateFrom { get; set; }

        public string DateTo { get; set; }
        public string DateFrom { get; set; }
    }
}

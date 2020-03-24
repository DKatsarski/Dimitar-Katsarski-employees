using System.Collections.Generic;

namespace EmployeesInfo.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Project> Projects { get; set; }
    }
}

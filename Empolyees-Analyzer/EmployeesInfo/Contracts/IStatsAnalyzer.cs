using EmployeesInfo.Models;
using System.Collections.Generic;

namespace EmployeesInfo.Contracts
{
    public interface IStatsAnalyzer
    {
        EmployeesStatsDTO GetTheTwoMostWorkedTogether(List<ProjectsEmplyees> projects);
    }
}

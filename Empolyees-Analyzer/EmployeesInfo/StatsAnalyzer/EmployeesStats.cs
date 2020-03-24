using EmployeesInfo.Contracts;
using EmployeesInfo.Models;
using System.Collections.Generic;

namespace EmployeesInfo.StatsAnalyzer
{
    public class EmployeesStats : IStatsAnalyzer
    {
        public EmployeesStatsDTO GetTheTwoMostWorkedTogether(List<ProjectsEmplyees> projects)
        {
            int maxOverlap = 0;
            var firstEmpolyee = 0;
            var secondEmployee = 0;
            var employeeId = 0;
            var projectId = 0;
            Dictionary<int, EmpIdData> empIds = new Dictionary<int, EmpIdData>();

            foreach (var employee in projects)
            {
                employeeId = employee.EmployeeId;
                projectId = employee.ProjectId;

                var timeFrameForId = new TimeFrame(employee.DateFrom, employee.DateTo);

                EmpIdData empIdData = new EmpIdData();
                empIdData.Periods.Add(timeFrameForId);
                empIdData.ProjectId = employee.ProjectId;
                empIds[employeeId] = empIdData;

                foreach (var emp in empIds)
                {
                    if (emp.Key != employeeId && emp.Value.ProjectId == projectId)
                    {
                        int overlapPeriod = emp.Value.Overlaps.GetValueOrDefault(employeeId, 0);

                        foreach (var otherPeriods in emp.Value.Periods)
                            overlapPeriod += timeFrameForId.Overlap(otherPeriods);
                        emp.Value.Overlaps[employeeId] = overlapPeriod;

                        if (overlapPeriod > maxOverlap)
                        {
                            firstEmpolyee = emp.Key;
                            secondEmployee = employeeId;
                            maxOverlap = overlapPeriod;
                        }
                    }
                }
            }

            return new EmployeesStatsDTO
            {
                FirstEmployeeId = firstEmpolyee,
                SecondEmployeeId = secondEmployee,
                ProjectId = projectId,
                ElapsedDays = maxOverlap
            };
        }
    }
}

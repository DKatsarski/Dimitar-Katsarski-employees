using System.Collections.Generic;

namespace EmployeesInfo.StatsAnalyzer
{
    public class EmpIdData
    {
        public EmpIdData()

        {
            this.Periods = new List<TimeFrame>();
            this.Overlaps = new Dictionary<int, int>();
        }

        public int ProjectId { get; set; }
        public List<TimeFrame> Periods { get; }
        public Dictionary<int, int> Overlaps { get; }
    }
}

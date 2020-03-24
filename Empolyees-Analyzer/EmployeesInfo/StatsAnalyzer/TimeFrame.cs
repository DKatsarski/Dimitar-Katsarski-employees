using System;

namespace EmployeesInfo.StatsAnalyzer
{
    public class TimeFrame
    {
        public DateTime Start { get; }
        public DateTime End { get; }

        public TimeFrame(DateTime start, DateTime end)
        {
            this.Start = start;
            this.End = end;
        }

        public int Overlap(TimeFrame other)
        {
            DateTime a = this.Start > other.Start ? this.Start : other.Start;
            DateTime b = this.End < other.End ? this.End : other.End;
            return (a < b) ? b.Subtract(a).Days : 0;
        }
    }
}

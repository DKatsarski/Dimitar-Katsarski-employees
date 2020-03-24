using EmployeesInfo.Models;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace EmployeesInfo
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }


        private void AddTextFile_Clicked(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Title = "Browse Text Files",
                CheckFileExists = true,
                CheckPathExists = true,
                DefaultExt = "txt",
            };

            openFileDialog.ShowDialog();
            var selectedFile = openFileDialog.FileName;
            var db = File.ReadAllLines(selectedFile);
            char[] delimiterChars = { ' ', ',' };

            var projectsEmplyeesDb = ParseDb(delimiterChars, db);

            //Calculates the longest wokring 
            var result = GetTheTwoMostWorkedTogether(projectsEmplyeesDb);

            EmployeesDataGrid.Items.Add(result);
        }

        private List<ProjectsEmplyees> ParseDb(char[] delimiterChars, string[] db)
        {
            var projectsEmplyees = new List<ProjectsEmplyees>();

            foreach (var emplyee in db)
            {
                var dbFragment = emplyee.Split(delimiterChars, StringSplitOptions.RemoveEmptyEntries);

                if (dbFragment[3] == "NULL")
                {
                    dbFragment[3] = DateTime.Now.ToString("yyyy-MM-dd");
                }

                projectsEmplyees.Add(new ProjectsEmplyees
                {
                    EmployeeId = int.Parse(dbFragment[0]),
                    ProjectId = int.Parse(dbFragment[1]),
                    DateFrom = DateTime.ParseExact(dbFragment[2], "yyyy-MM-dd", CultureInfo.InvariantCulture),
                    DateTo = DateTime.ParseExact(dbFragment[3], "yyyy-MM-dd", CultureInfo.InvariantCulture)
                });

            }

            return projectsEmplyees;
        }

        static EmployeesStatsDTO GetTheTwoMostWorkedTogether(List<ProjectsEmplyees> projects)
        {
            int maxOverlap = 0;
            var firstEmpolyee = 0;
            var secondEmployee = 0;
            var employeeId = 0;
            var projectId = 0;
            Dictionary<int, IdData> ids = new Dictionary<int, IdData>();

            foreach (var employee in projects)
            {
                employeeId = employee.EmployeeId;
                projectId = employee.ProjectId;

                Period idPeriod = new Period(employee.DateFrom, employee.DateTo);

                // preserve interval for ID
                IdData idData = new IdData(); /*ids.GetValueOrDefault(id, new IdData());*/
                idData.Periods.Add(idPeriod);
                idData.ProjectId = employee.ProjectId;
                ids[employeeId] = idData;

                foreach (var idObj in ids)
                {
                    if (idObj.Key != employeeId && idObj.Value.ProjectId == projectId)
                    {
                        // here we calculate of new interval with all previously met
                        int overlapPeriod = idObj.Value.Overlaps.GetValueOrDefault(employeeId, 0);

                        foreach (var otherPeriods in idObj.Value.Periods)
                            overlapPeriod += idPeriod.Overlap(otherPeriods);
                        idObj.Value.Overlaps[employeeId] = overlapPeriod;

                        // check whether newly calculate overlapping is the maximal one, preserve Ids if needed too
                        if (overlapPeriod > maxOverlap)
                        {
                            firstEmpolyee = idObj.Key;
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

    class Period
    {
        public DateTime Start { get; }
        public DateTime End { get; }

        public Period(DateTime start, DateTime end)
        {
            this.Start = start;
            this.End = end;
        }

        public int Overlap(Period other)
        {
            DateTime a = this.Start > other.Start ? this.Start : other.Start;
            DateTime b = this.End < other.End ? this.End : other.End;
            return (a < b) ? b.Subtract(a).Days : 0;
        }
    }

    class IdData
    {
        public IdData()

        {
            this.Periods = new List<Period>();
            this.Overlaps = new Dictionary<int, int>();
        }

        public int ProjectId { get; set; }
        public List<Period> Periods { get; }
        public Dictionary<int, int> Overlaps { get; }
    }
}


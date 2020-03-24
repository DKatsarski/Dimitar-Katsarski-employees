using EmployeesInfo.Models;
using EmployeesInfo.StatsAnalyzer;
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

            var empStatsAnalyzer = new EmployeesStats();
            var result = empStatsAnalyzer.GetTheTwoMostWorkedTogether(projectsEmplyeesDb);

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

    }
}


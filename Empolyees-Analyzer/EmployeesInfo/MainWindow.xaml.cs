using EmployeesInfo.Models;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Globalization;
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
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();


        }


        private void AddTextFile_Clicked(object sender, RoutedEventArgs e)
        {


            OpenFileDialog openFileDialog1 = new OpenFileDialog
            {
                Title = "Browse Text Files",
                CheckFileExists = true,
                CheckPathExists = true,
                DefaultExt = "txt",
            };

            openFileDialog1.ShowDialog();

            var selectedFile = openFileDialog1.FileName;
            string[] lines = System.IO.File.ReadAllLines(selectedFile);

            var projectsEmplyees = new List<ProjectsEmplyees>();

            char[] delimiterChars = { ' ', ',' };

            foreach (var emplyee in lines)
            {
                var dbFiller = emplyee.Split(delimiterChars, StringSplitOptions.RemoveEmptyEntries);

                if (dbFiller[3] == "NULL")
                {
                    dbFiller[3] = DateTime.Now.ToString("yyyy-MM-dd");
                }

                projectsEmplyees.Add(new ProjectsEmplyees
                {
                    EmployeeId = int.Parse(dbFiller[0]),
                    ProjectId = int.Parse(dbFiller[1]),
                    DateFrom = DateTime.ParseExact(dbFiller[2], "yyyy-MM-dd", CultureInfo.InvariantCulture),
                    DateTo = DateTime.ParseExact(dbFiller[3], "yyyy-MM-dd", CultureInfo.InvariantCulture)
                });

            }



        }
    }
}

using EmployeesInfo.Models;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
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
            var emp = new Employee()
            {
                Id = 23
            };

            EmployeesDataGrid.Items.Add(emp);

            OpenFileDialog openFileDialog1 = new OpenFileDialog
            {
                Title = "Browse Text Files",
                CheckFileExists = true,
                CheckPathExists = true,
                DefaultExt = "txt",
            };

           openFileDialog1.ShowDialog();

            var a = openFileDialog1.FileName;


            string[] lines = System.IO.File.ReadAllLines(a);
            var projectsEmplyees = new List<ProjectsEmplyees>();
            char[] delimiterChars = { ' ', ',' };

            foreach (var emplyee in lines)
            {
                var dbFiller = emplyee.Split(delimiterChars, StringSplitOptions.RemoveEmptyEntries);
                for (int i = 0; i < 1; i++)
                {
                    projectsEmplyees.Add(new ProjectsEmplyees
                    {
                        EmployeeId = int.Parse(dbFiller[i]),
                        ProjectId = int.Parse(dbFiller[i + 1]),
                        //DateFrom = DateTime.Parse(dbFiller[i + 2]),
                        //DateTo = DateTime.Parse(dbFiller[i + 3])
                        DateFrom = dbFiller[i + 2],
                        DateTo = dbFiller[i + 3],
                    });
                }
 
            }

            foreach (var item in projectsEmplyees)
            {

            }

        }
    }
}

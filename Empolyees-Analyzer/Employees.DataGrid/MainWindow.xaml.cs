using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

namespace Employees.DataGrid
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

        private void CompanyProjectsDb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var firstEmp = new Employee();

            firstEmp.EmpID = "23";
            firstEmp.Name = "df";

            CompanyProjectsDb.Items.Add(firstEmp);
        }

        public class Employee
        {
            public string EmpID { get; set; }
            public string Name { get; set; }

        }
    }
}

using Microsoft.Win32;
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

            var emp = new Employee()
            {
                Id = 23
            };

            EmployeesDataGrid.Items.Add(emp);
        }

        public class Employee
        {
            public int Id { get; set; }
        }

        private void AddTextFile_Clicked(object sender, RoutedEventArgs e)
        {
            var d = new Employee() { Id = 23};
            EmployeesDataGrid.Items.Add(d);

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



        }
    }
}

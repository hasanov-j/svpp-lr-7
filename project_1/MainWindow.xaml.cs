using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace project_1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Employee employee;
        ObservableCollection<Employee> employees=new ObservableCollection<Employee>();
        public MainWindow()
        {
            InitializeComponent();

            employee= new Employee();
            stackpanelEmployee.DataContext = employee;
            listBox.DataContext = employees;
        }

        private void FillData(Employee[]? inputEmployees=null)
        {
            employees.Clear();

            if (inputEmployees == null )
            {
                foreach (Employee employee in Employee.GetEmployees())
                {
                    employees.Add(employee);
                }
            } else {
                foreach (Employee employee in inputEmployees)
                {
                    employees.Add(employee);
                }
            }


        }

        private void buttonView_Click(object sender, RoutedEventArgs e)
        {
            FillData();
        }

        private void buttonInsert_Click(object sender, RoutedEventArgs e)
        {
            employee.Insert();
            FillData();
        }

        private void buttonFind_Click(object sender, RoutedEventArgs e)
        {
            Employee[]? foundEmployees = Employee.FindByFirstName(textBoxFirstName.Text);
            if(foundEmployees == null) {
                MessageBox.Show("запись не найдена");
            } else {
                FillData(foundEmployees);
            }
        }

        private void buttonChange_Click(object sender, RoutedEventArgs e)
        {
            if(listBox.SelectedItem == null) {
                MessageBox.Show("Нет выбранной записи");
                return;
            }

            employee.Id = ((Employee)listBox.SelectedItem).Id;

            if( textBoxFirstName.Text.Length > 0 ) {
                employee.Firstname = textBoxFirstName.Text;
            } else {
                employee.Firstname = ((Employee)(listBox.SelectedItem)).Firstname;
            }

            if( textBoxLastName.Text.Length > 0 ) {
                employee.Lastname = textBoxLastName.Text;
            } else {
                employee.Lastname = ((Employee)(listBox.SelectedItem)).Lastname;
            }

            if(datePickerDob.SelectedDate != DateTime.Today) {
                employee.DateOfBirth = datePickerDob.SelectedDate.Value;
            } else {
                employee.DateOfBirth = ((Employee)(listBox.SelectedItem)).DateOfBirth;
            }

            if( textBoxPosition.Text.Length > 0 ) {
                employee.Position = textBoxPosition.Text;
            } else {
                employee.Position = ((Employee)(listBox.SelectedItem)).Position;
            }

            decimal salary = ConvertTextBoxSalaryToDecimal();
            if (salary != 0 ) {
                employee.Salary = salary;
            } else {
                employee.Salary = ((Employee)(listBox.SelectedItem)).Salary;
            }

            employee.Update();
            FillData();
        }

        private decimal ConvertTextBoxSalaryToDecimal() {

            return Convert.ToDecimal(textBoxSalary.Text);

        }

        private void buttonDelete_Click(object sender, RoutedEventArgs e)
        {
            if(listBox.SelectedItem == null) { return; }

            Employee.Delete(((Employee) listBox.SelectedItem).Id);

            FillData();
        }
    }
}

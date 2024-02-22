using System;
using System.Collections.Generic;
using System.Data;
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

namespace project_2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DataTable ordersTable = new DataTable();
        public MainWindow()
        {
            InitializeComponent();

            //Fill();
        }

        private void Fill()
        {
            ordersTable.Rows.Clear();
            ordersTable = Order.GetAll();
            orderGrid.ItemsSource = ordersTable.DefaultView;
        }

        private void ButtonView_Click(object sender, RoutedEventArgs e)
        {
            Fill();
        }

        private void ButtonUpdate_Click(object sender, RoutedEventArgs e)
        {
            Order.Update();
            Fill();
        }

        private void ButtonFind_Click(object sender, RoutedEventArgs e)
        {
            Order order = new Order();
            Window1 window1 = new Window1(order);

            if (window1.ShowDialog() == false) { return; }

            string? result = order.Find();

            if (result == "") {
                MessageBox.Show("Записей не найдено!");
            } else {
                MessageBox.Show(result);
            }

        }
    }
}

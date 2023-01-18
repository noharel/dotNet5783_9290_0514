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
using System.Windows.Shapes;

namespace PL.Admin
{
    /// <summary>
    /// Interaction logic for OrdersOptions.xaml
    /// </summary>
    public partial class OrdersOptions : Window
    {
        public OrdersOptions()
        {
            InitializeComponent();
        }

        private void orders_Click(object sender, RoutedEventArgs e)
        {

            new PL.Orders.ordersListWindow().ShowDialog();//Open ordersListWindow with list of orders
        }

        private void simulator_Click(object sender, RoutedEventArgs e)
        {
            //new PL.Admin.OrdersSimulator().show();
        }
    }
}

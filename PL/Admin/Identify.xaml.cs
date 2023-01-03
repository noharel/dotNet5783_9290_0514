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
    /// Interaction logic for Identify.xaml
    /// </summary>
    public partial class Identify : Window
    {
        string USER_NAME1 = "talel";
        string USER_NAME2 = "noa";
        string PASSWORD1 = "123456";
        string PASSWORD2 = "111111";
        public Identify()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if((USER_NAME1==user.Text && PASSWORD1==password.Password) || (USER_NAME2 == user.Text && PASSWORD2 == password.Password))
            {
                login.Visibility = Visibility.Collapsed;
                products.Visibility = Visibility.Visible;
                orders.Visibility = Visibility.Visible;
            }
            else
            {
                MessageBox.Show("incorrect information");
            }
        }

        private void products_Click(object sender, RoutedEventArgs e)
        {
            new PL.Products.ProductListWindow().ShowDialog();
        }

        private void orders_Click(object sender, RoutedEventArgs e)
        {
            new PL.Orders.ordersListWindow().ShowDialog();
        }
    }
}

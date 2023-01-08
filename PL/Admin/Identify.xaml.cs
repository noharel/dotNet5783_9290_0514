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

        public Identify()//constructor
        {
            InitializeComponent();
        }

        /// <summary>
        /// Log in button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if((USER_NAME1==user.Text && PASSWORD1==password.Password) || (USER_NAME2 == user.Text && PASSWORD2 == password.Password))//if a correct password and username were inputted is is possible to log in
            {
                login.Visibility = Visibility.Collapsed;
                products.Visibility = Visibility.Visible;
                orders.Visibility = Visibility.Visible;
            }
            else//incorrect information was inputted
            {
                MessageBox.Show("incorrect information");
            }
        }

        /// <summary>
        /// Products button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void products_Click(object sender, RoutedEventArgs e)
        {
            new PL.Products.ProductListWindow().ShowDialog();//Open ProductListWindow with list of products
        }
    
        /// <summary>
        /// orders button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void orders_Click(object sender, RoutedEventArgs e)
        {
            new PL.Orders.ordersListWindow().ShowDialog();//Open ordersListWindow with list of orders
        }
    }
}

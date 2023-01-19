using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Printing;
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
using static System.Net.Mime.MediaTypeNames;

namespace PL
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        BlApi.IBl? bl = BlApi.Factory.Get(); // get bl from factory

        public MainWindow() // constructor
        {
            InitializeComponent();
        }

        private void ProductsMouseEnter(object sender, RoutedEventArgs e) // product buttom
        {
            new PL.Carts.productsListForCart().ShowDialog();//open window of list of products
        }
        /// <summary>
        /// Track Order button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TrackOrder_Click(object sender, RoutedEventArgs e)
        {
            int x;
            bool flag;


            if (orderTrackingNumber.Text.Length != 4)//order id doesn't have 4 charachters
            {
                MessageBox.Show("Order ID has to be 4 numbers");
            }
            else//order id has 4 charachters
            {
                flag = int.TryParse(orderTrackingNumber.Text, out x);//check if order id is a number

                if (flag)//order id is a number
                {
                    try
                    {
                        bl!.Order.TrackingOrder(x);//track order with sent id

                        new PL.Orders.Order(x).ShowDialog();//open Order window with tracked order
                    }
                    catch (BO.DoesntExistExeption ex)//catch for TrackingOrder
                    {
                        string innerEx = "";
                        if (ex.InnerException != null)//print inner exception if it exists
                            innerEx = ": " + ex.InnerException.Message;
                        MessageBox.Show("unsucessfull selection:" + ex.Message + innerEx); // for user print exception

                    }
                }
                else//order id is not a number
                {
                    MessageBox.Show("Order ID has to be a number");
                }
            }
            
        }
        /// <summary>
        /// Admin button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void adminButton_Click(object sender, RoutedEventArgs e)
        {
            new PL.Admin.Identify().ShowDialog();//Open Identify window
        }
    }
}

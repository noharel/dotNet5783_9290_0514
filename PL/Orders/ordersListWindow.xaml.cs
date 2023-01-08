using PL.Products;
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

namespace PL.Orders
{
    /// <summary>
    /// Interaction logic for ordersListWindow.xaml
    /// </summary>
    public partial class ordersListWindow : Window
    {

        BlApi.IBl? bl = BlApi.Factory.Get();  // get bl
        public ordersListWindow() //constructor
        {
            InitializeComponent();
            try
            {
                ordersList.ItemsSource = bl.Order.GetOrders(); // get all the orders
            }
            catch(BO.DoesntExistExeption ex)  // exepction from get orders func
            {
                string innerEx = "";
                if (ex.InnerException != null)
                    innerEx = ": " + ex.InnerException.Message;
                MessageBox.Show("unsucessfull selection:" + ex.Message + innerEx); //print exception for user
            }
        }

        public void ordersList_SelectionChanged(object sender, SelectionChangedEventArgs e) //choose order
        {
            BO.OrderForList? selectedOrder = (BO.OrderForList)ordersList.SelectedItem; // the order was choosed

            if (selectedOrder != null) //if there is an order
            {
                new PL.Orders.Order(selectedOrder.ID, true).ShowDialog(); //open the window with all the information
            }
            try
            {
                ordersList.ItemsSource = bl!.Order.GetOrders(); //return to the window - update the orders
            }
            catch(BO.DoesntExistExeption ex) // catch exception from get orders func
            {
                string innerEx = "";
                if (ex.InnerException != null)
                    innerEx = ": " + ex.InnerException.Message;
                MessageBox.Show("unsucessfull selection:" + ex.Message + innerEx); //print exception for user

            }
           
        }

 
    }
}

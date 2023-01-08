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

        public void ordersList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            BO.OrderForList? selectedOrder = (BO.OrderForList)ordersList.SelectedItem;

            if (selectedOrder != null)
            {
                new PL.Orders.Order(selectedOrder.ID, true).ShowDialog();
            }
            try
            {
                ordersList.ItemsSource = bl!.Order.GetOrders();
            }
            catch(BO.DoesntExistExeption ex)
            {
                string innerEx = "";
                if (ex.InnerException != null)
                    innerEx = ": " + ex.InnerException.Message;
                MessageBox.Show("unsucessfull selection:" + ex.Message + innerEx); //print exception for user

            }
            /*
            try
            {
                ordersList.ItemsSource = bl!.Order.GetOrders(); // update list of product to see changes
            }

            catch (BO.DoesntExistExeption ex) // get list product exception
            {
                string innerEx = "";
                if (ex.InnerException != null)
                    innerEx = ": " + ex.InnerException.Message;
                MessageBox.Show("unsucessfull selection:" + ex.Message + innerEx); //print exception for user

            }*/
        }

 
    }
}

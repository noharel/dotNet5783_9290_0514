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
using System.Windows.Shapes;

namespace PL.Orders
{
    /// <summary>
    /// Interaction logic for OrderInfo.xaml
    /// </summary>
    public partial class OrderInfo : Window
    {
        BlApi.IBl? bl = BlApi.Factory.Get(); // get bl from factory
        int idRec = 0;
        public OrderInfo(int x = 0, bool managerUpdate = false)
        {
            InitializeComponent();
            idRec = x;
            id.Text =x.ToString();
            BO.Order order = bl.Order.OrderInfo(x);
            id.Text = x.ToString();
            name.Text = order.CustomerName;
            address.Text = order.CustomerAddress;
            email.Text = order.CustomerEmail;
            orderDate.Text = order.OrderDate.ToString();
            if (order.Status!=BO.OrderStatus.Ordered)
            {
                shipDate.Text = order.ShipDate.ToString();
                if (order.Status == BO.OrderStatus.Arrived)
                    arrivalDate.Text = order.DeliveryrDate.ToString();
                else
                {
                    arrivalDate.Visibility = Visibility.Collapsed;
                    labelArrivalDate.Visibility = Visibility.Collapsed;
                }
            }
            else
            {
                shipDate.Visibility = Visibility.Collapsed;
                labelShipDate.Visibility = Visibility.Collapsed;
                arrivalDate.Visibility = Visibility.Collapsed;
                labelArrivalDate.Visibility = Visibility.Collapsed;
            }
            status.Text = order.Status.ToString();
            totalPrice.Text = order.TotalPrice.ToString();
            products.ItemsSource = order.Items!.ToList();

            if(managerUpdate)
            {
                
                //addButton.visibility = Visibility.Collapsed;
            }
        }


        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            int id = ((BO.ProductItem?)button.DataContext)!.ID;
            int amount = ((BO.ProductItem?)button.DataContext)!.Amount;
            try
            {
                bl.Order.UpdateByManager(idRec, id, amount + 1);
            }
            catch (BO.DoesntExistExeption ex)
            {
                string innerEx = "";
                if (ex.InnerException != null)
                    innerEx = ": " + ex.InnerException.Message;
                MessageBox.Show("unsucessfull selection:" + ex.Message + innerEx); // for user print exception
            }
            catch (BO.ContradictoryDataExeption ex)
            {
                string innerEx = "";
                if (ex.InnerException != null)
                    innerEx = ": " + ex.InnerException.Message;
                MessageBox.Show("unsucessfull selection:" + ex.Message + innerEx); // for user print exception
            }
        }
        private void removeButton_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            int id = ((BO.ProductItem?)button.DataContext)!.ID;
            int amount = ((BO.ProductItem?)button.DataContext)!.Amount;
            try
            {
                bl.Order.UpdateByManager(idRec, id, amount -1);
            }
            catch(BO.DoesntExistExeption ex)
            {
                string innerEx = "";
                if (ex.InnerException != null)
                    innerEx = ": " + ex.InnerException.Message;
                MessageBox.Show("unsucessfull selection:" + ex.Message + innerEx); // for user print exception
            }
            catch(BO.ContradictoryDataExeption ex)
            {
                string innerEx = "";
                if (ex.InnerException != null)
                    innerEx = ": " + ex.InnerException.Message;
                MessageBox.Show("unsucessfull selection:" + ex.Message + innerEx); // for user print exception
            }
        }
    }
}

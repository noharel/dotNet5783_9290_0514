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
    /// Interaction logic for OrderInfo.xaml
    /// </summary>
    public partial class OrderInfo : Window
    {
        BlApi.IBl? bl = BlApi.Factory.Get(); // get bl from factory

        public OrderInfo(int x = 0)
        {
            InitializeComponent();
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
            }
            status.Text = order.Status.ToString();
            totalPrice.Text = order.TotalPrice.ToString();
            products.ItemsSource = order.Items!.ToList();
        }

    }
}

using MaterialDesignThemes.Wpf;
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

namespace PL.Orders;

/// <summary>
/// Interaction logic for Order.xaml
/// </summary>
public partial class Order : Window
{
    BlApi.IBl? bl = BlApi.Factory.Get(); // get bl from factory
    public Order(int x = 0)
    {
        InitializeComponent();

        status.Text = bl.Order.TrackingOrder(x).Status.ToString();
        orderId.Text = x.ToString();

        
        List<(DateTime?, string?)>? tuplelist = bl.Order.TrackingOrder(x).tuplesList!.ToList();
        int size = tuplelist.Count();
        orderingDate.Text = tuplelist[0].Item1.ToString();
        if (size > 1) 
            shippingDate.Text = tuplelist[1].Item1.ToString();
        if (size > 2 ) arrivalDate.Text = tuplelist[2].Item1.ToString();

        if (size < 3)
        {
            arrivalDate.Visibility = Visibility.Collapsed;
            labelA.Visibility = Visibility.Collapsed;
            if (size < 2)
            {
                shippingDate.Visibility = Visibility.Collapsed;
                labelS.Visibility = Visibility.Collapsed;
            }
        }
       
        
        

    }

    private void orderInfoButton_Click(object sender, RoutedEventArgs e)
    {
        new PL.Orders.OrderInfo().ShowDialog();
    }
}

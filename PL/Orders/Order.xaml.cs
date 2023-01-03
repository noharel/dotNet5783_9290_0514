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
/// 



public partial class Order : Window
{


    BlApi.IBl? bl = BlApi.Factory.Get(); // get bl from factory
    int idRec = 0;
    public Order(int x = 0)
    {

        InitializeComponent();
  
        idRec = x;
        try
        {
            status.Text = bl.Order.TrackingOrder(x).Status.ToString();
        }
        catch(BO.DoesntExistExeption ex)
        {
            string innerEx = "";
            if (ex.InnerException != null)
                innerEx = ": " + ex.InnerException.Message;
            MessageBox.Show("unsucessfull selection:" + ex.Message + innerEx); // for user print exception
        }

        orderId.Text = x.ToString();

        try
        {
            List<(DateTime?, string?)>? tuplelist = bl.Order.TrackingOrder(x).tuplesList!.ToList();
        }
        catch (BO.DoesntExistExeption ex)
        {
            string innerEx = "";
            if (ex.InnerException != null)
                innerEx = ": " + ex.InnerException.Message;
            MessageBox.Show("unsucessfull selection:" + ex.Message + innerEx); // for user print exception
        }
        int size = tuplelist.Count();
        orderingDate.Text = tuplelist[0].Item1.ToString();
        if (size > 1)
            shippingDate.Text = tuplelist[1].Item1.ToString();
        if (size > 2) arrivalDate.Text = tuplelist[2].Item1.ToString();

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
        new PL.Orders.OrderInfo(idRec).ShowDialog();
    }
}

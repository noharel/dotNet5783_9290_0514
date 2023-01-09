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
    bool managerFunc = false;
    BO.OrderTracking? orderT;
    DateTime shipDate;
    DateTime orderDate;
    DateTime deliveryDate;
    public Order(int x = 0, bool manager = false)  //constructor
    {

        InitializeComponent();
        try
        {
            orderT = bl.Order.TrackingOrder(x); //get order information for x
            DataContext = orderT;
            if (manager && orderT.tuplesList!.ToList().Count() == 1)
            // if manager mode and order not shipped yet
            {
                orderInfoButton.Content = "Order information and update"; //can update
            }

            managerFunc = manager; // for all the function will have the mode (manager or not)

            idRec = x; // for all the function will have the id

            List<(DateTime?, string?)>? tuplelist = orderT.tuplesList!.ToList(); 
            int size = tuplelist.Count(); // to know the status

            orderingDate.Text = tuplelist[0].Item1.ToString(); // ordering date
            if (size > 1) // if shipped
                shippingDate.Text = tuplelist[1].Item1.ToString(); //ship date 

            if (size > 2)  // arrived
                arrivalDate.Text = tuplelist[2].Item1.ToString(); // arrival date

            if (size < 3) // not arrived
            {
                // not arrived
                arrivalDate.Visibility = Visibility.Collapsed;

                if (size < 2) // not shipped
                {
                    // not shipped
                    shippingDate.Visibility = Visibility.Collapsed;
                    

                    if (manager) 
                        shipOrderByManager.Visibility = Visibility.Visible; // can ship order
                }
                else // shipped
                {
                    if (manager) 
                        delinerOrderByManager.Visibility = Visibility.Visible; // can deliver
                }
            }
        }
        catch(BO.DoesntExistExeption ex) // tracking order exception 
        {
            string innerEx = "";
            if (ex.InnerException != null)
                innerEx = ": " + ex.InnerException.Message;
            MessageBox.Show("unsucessfull selection:" + ex.Message + innerEx); // for user print exception
        }
    }

    private void orderInfoButton_Click(object sender, RoutedEventArgs e) // order info button
    {
        
        new PL.Orders.OrderInfo(idRec, managerFunc).ShowDialog(); // open the info window

        try
        {
            // try tracking the order after changes in the info window
            BO.OrderTracking orderT = bl!.Order.TrackingOrder(idRec);
        }
        catch(BO.DoesntExistExeption) // if the order is not exist
        {
            // all the buttons and label collapsed
            orderInfoButton.Visibility = Visibility.Collapsed;
            orderDeleted.Visibility = Visibility.Visible;
            labelO.Visibility = Visibility.Collapsed;
            orderingDate.Visibility = Visibility.Collapsed;
            shipOrderByManager.Visibility = Visibility.Collapsed;
            status.Visibility = Visibility.Collapsed;
        }
        catch(BO.InvalidInputExeption ex) // tracking order exception
        {
            string innerEx = "";
            if (ex.InnerException != null)
                innerEx = ": " + ex.InnerException.Message;
            MessageBox.Show("unsucessfull selection:" + ex.Message + innerEx); // for user print exception
        }
    }

    private void shipOrderByManager_Click(object sender, RoutedEventArgs e) //ship order
    {
        try
        {
            bl!.Order.UpdateShip(idRec); // update ship
            shipOrderByManager.Visibility = Visibility.Collapsed; // no need for the button
            try
            {
                // get a new tuple
                List<(DateTime?, string?)>? tuplelist = bl.Order.TrackingOrder(idRec).tuplesList!.ToList(); 

                shippingDate.Text = tuplelist[1].Item1.ToString(); //ship date text
                shippingDate.Visibility = Visibility.Visible;
                labelS.Visibility = Visibility.Visible; // ship date label
                delinerOrderByManager.Visibility = Visibility.Visible; // now the manager can delivery
                orderInfoButton.Content = "Order information"; // can't update anymore
                status.Text = "Shipped"; // change status on the window
            }
            catch (BO.DoesntExistExeption ex) // tracking order func exception
            {

                string innerEx = "";
                if (ex.InnerException != null)
                    innerEx = ": " + ex.InnerException.Message;
                MessageBox.Show("unsucessfull selection:" + ex.Message + innerEx); // for user print exception
            }
        }
        catch(BO.DoesntExistExeption ex) //update ship func exception
        {
            string innerEx = "";
            if (ex.InnerException != null)
                innerEx = ": " + ex.InnerException.Message;
            MessageBox.Show("unsucessfull selection:" + ex.Message + innerEx); // for user print exception
        }
        catch(BO.ContradictoryDataExeption ex) //update ship func exception
        {
            string innerEx = "";
            if (ex.InnerException != null)
                innerEx = ": " + ex.InnerException.Message;
            MessageBox.Show("unsucessfull selection:" + ex.Message + innerEx); // for user print exception
        }
    }

    private void delinerOrderByManager_Click(object sender, RoutedEventArgs e) // deliver order
    {
        try
        {
            bl!.Order.UpdateDelivery(idRec);
            delinerOrderByManager.Visibility = Visibility.Collapsed; // no need for the button

            try
            {
                // get new tupelist
                List<(DateTime?, string?)>? tuplelist = bl.Order.TrackingOrder(idRec).tuplesList!.ToList();

                arrivalDate.Text = tuplelist[2].Item1.ToString(); // arrival date text
                arrivalDate.Visibility = Visibility.Visible; 
                labelA.Visibility = Visibility.Visible; // arrival date label
                status.Text = "Arrived"; // changes status on the window
            }
            catch (BO.DoesntExistExeption ex) // Tracking order exception
            {

                string innerEx = "";
                if (ex.InnerException != null)
                    innerEx = ": " + ex.InnerException.Message;
                MessageBox.Show("unsucessfull selection:" + ex.Message + innerEx); // for user print exception
            }
        }
        catch(BO.DoesntExistExeption ex) //update delivery func exception 
        {
            string innerEx = "";
            if (ex.InnerException != null)
                innerEx = ": " + ex.InnerException.Message;
            MessageBox.Show("unsucessfull selection:" + ex.Message + innerEx); // for user print exception
        }
        catch(BO.ContradictoryDataExeption ex)//update delivery func exception 
        {
            string innerEx = "";
            if (ex.InnerException != null)
                innerEx = ": " + ex.InnerException.Message;
            MessageBox.Show("unsucessfull selection:" + ex.Message + innerEx); // for user print exception
        }
    }
}
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        public BO.Order order { get; set; }
        public bool manager { get; set; }
        public OrderInfo(int x = 0, bool managerUpdate = false) // consturctor
        {
        
            InitializeComponent();
            idRec = x; // for all the functions will have the id
            manager = managerUpdate;
            
            try
            {
                //update the infomation in the window
                order = bl.Order.OrderInfo(x);
                this.DataContext = this;
                if (order.Status != BO.OrderStatus.Ordered) // if shipped or delivered
                {
                    int A;
                    //shipDate.Text = order.ShipDate.ToString(); // ship date
                    if (order.Status == BO.OrderStatus.Arrived)
                        A = 1;// if arrived
                                  //arrivalDate.Text = order.DeliveryrDate.ToString(); // arrival date
                    else // if not arrived
                    {
                        // no need in arrival text and label
                        arrivalDate.Visibility = Visibility.Collapsed;
                        labelArrivalDate.Visibility = Visibility.Collapsed;
                    }
                }
                else  // order just ordered
                {
                    // no need in ship and arrival labels and texts
                    shipDate.Visibility = Visibility.Collapsed;
                    labelShipDate.Visibility = Visibility.Collapsed;
                    arrivalDate.Visibility = Visibility.Collapsed;
                    labelArrivalDate.Visibility = Visibility.Collapsed;
                }

                if (managerUpdate && order!.Status == 0) // if manager mode and not shipped yet
                {
                    // update button hidden for the minus and plus in list view
                    update.Visibility = Visibility.Hidden; 
                }
            }
            catch(BO.InvalidInputExeption ex)// order info func exception
            {

                string innerEx = "";
                if (ex.InnerException != null)
                    innerEx = ": " + ex.InnerException.Message;
                MessageBox.Show("unsucessfull selection:" + ex.Message + innerEx); // for user print exception
            }
            catch(BO.DoesntExistExeption ex)// order info func exception
            {

                string innerEx = "";
                if (ex.InnerException != null)
                    innerEx = ": " + ex.InnerException.Message;
                MessageBox.Show("unsucessfull selection:" + ex.Message + innerEx); // for user print exception
            }
        }


        private void addButton_Click(object sender, RoutedEventArgs e) //add button
        {
            Button button = (Button)sender; // t knoe which product
            int id = ((BO.OrderItem?)button.DataContext)!.ProductID; //product id
            try
            {
                bl!.Order.UpdateByManager(idRec, id, 1); // update the amount of product in order +1
                order = bl.Order.OrderInfo(idRec); // new order info
                totalPrice.Text = order.TotalPrice.ToString(); // change total price
                products.ItemsSource = order.Items!.ToList(); // change the items in list view
            }
            catch (BO.DoesntExistExeption ex)  //update by manager and order info funcs exception
            {
                string innerEx = "";
                if (ex.InnerException != null)
                    innerEx = ": " + ex.InnerException.Message;
                MessageBox.Show("unsucessfull selection:" + ex.Message + innerEx); // for user print exception
            }
            catch (BO.ContradictoryDataExeption ex) // update by manager func exception
            {
                string innerEx = "";
                if (ex.InnerException != null)
                    innerEx = ": " + ex.InnerException.Message;
                MessageBox.Show("unsucessfull selection:" + ex.Message + innerEx); // for user print exception
            }
            catch (BO.InvalidInputExeption ex)// order info func exception
            {

                string innerEx = "";
                if (ex.InnerException != null)
                    innerEx = ": " + ex.InnerException.Message;
                MessageBox.Show("unsucessfull selection:" + ex.Message + innerEx); // for user print exception
            }
        }
        private void removeButton_Click(object sender, RoutedEventArgs e) // remove button
        {
            Button button = (Button)sender; // to knoe which product
            int id = ((BO.OrderItem?)button.DataContext)!.ProductID; // product id
            try
            {
                bl!.Order.UpdateByManager(idRec, id, -1);  // update amount of product -1
                try
                {
                    order = bl.Order.OrderInfo(idRec); // new order info
                }
                catch(BO.DoesntExistExeption ) //order info exception
                {
                    Close(); //delete the last product
                }
                catch(BO.InvalidInputExeption ex) //order info exception
                {
                    string innerEx = "";
                    if (ex.InnerException != null)
                        innerEx = ": " + ex.InnerException.Message;
                    MessageBox.Show("unsucessfull selection:" + ex.Message + innerEx); // for user print exception
                }

                totalPrice.Text = order.TotalPrice.ToString(); // update total price
                products.ItemsSource = order.Items!.ToList(); // update items in list view
            }
            catch(BO.DoesntExistExeption ex) // update by manager func exception
            {
                string innerEx = "";
                if (ex.InnerException != null)
                    innerEx = ": " + ex.InnerException.Message;
                MessageBox.Show("unsucessfull selection:" + ex.Message + innerEx); // for user print exception
            }
            catch(BO.ContradictoryDataExeption ex)// update by manager func exception
            {
                string innerEx = "";
                if (ex.InnerException != null)
                    innerEx = ": " + ex.InnerException.Message;
                MessageBox.Show("unsucessfull selection:" + ex.Message + innerEx); // for user print exception
            }
        }
    }
}

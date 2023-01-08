﻿using Microsoft.VisualBasic;
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
        BO.Order order;
        public OrderInfo(int x = 0, bool managerUpdate = false)
        {
        
            InitializeComponent();
            idRec = x;
            id.Text =x.ToString();
            order = bl.Order.OrderInfo(x);
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

            if (managerUpdate && order!.Status == 0)
            {
                //update.visibility = Visibility.Hidden;
                update.Visibility = Visibility.Hidden;
                MessageBox.Show("yes");
            }
        }


        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            int id = ((BO.OrderItem?)button.DataContext)!.ProductID;
            try
            {
                bl!.Order.UpdateByManager(idRec, id, 1);
                order = bl.Order.OrderInfo(idRec);
                totalPrice.Text = order.TotalPrice.ToString();
                products.ItemsSource = order.Items!.ToList();
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
            int id = ((BO.OrderItem?)button.DataContext)!.ProductID;
            try
            {
                bl!.Order.UpdateByManager(idRec, id, -1);
                try
                {
                    order = bl.Order.OrderInfo(idRec);
                }
                catch(BO.DoesntExistExeption )
                {
                    Close();
                }
                catch(BO.InvalidInputExeption ex)
                {
                    string innerEx = "";
                    if (ex.InnerException != null)
                        innerEx = ": " + ex.InnerException.Message;
                    MessageBox.Show("unsucessfull selection:" + ex.Message + innerEx); // for user print exception
                }

                totalPrice.Text = order.TotalPrice.ToString();
                products.ItemsSource = order.Items!.ToList();
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

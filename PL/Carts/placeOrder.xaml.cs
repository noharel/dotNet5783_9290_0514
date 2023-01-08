using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using BlApi;
using BO;
using MaterialDesignThemes.Wpf;
using Microsoft.VisualBasic;

namespace PL.Carts
{
    /// <summary>
    /// Interaction logic for placeOrder.xaml
    /// </summary>
    public partial class placeOrder : Window
    {
        BlApi.IBl? bl = BlApi.Factory.Get(); // get bl from factory
        BO.Cart cart;

        public placeOrder(BO.Cart c)//constructor
        {
            InitializeComponent();
            cart = c;
            address.ItemsSource = Enum.GetValues(typeof(BO.Cities));//initialize address selectoe
        }
        /// <summary>
        /// Checkout cart button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void checkout_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                cart.CustomerName = nameTextBox.Text;
                cart.CustomerEmail = email.Text;
                cart.CustomerAddress = address.SelectedItem.ToString();
                bl?.Cart.MakingAnOrder(cart); //MAKING AN ORDER - UPDATE THE ORDERS LIST
                MessageBox.Show("Order placed, enjoy your new car!");//let customer know order was places
                List<BO.OrderItem>? list = new List<BO.OrderItem>();
                try//empty cart
                {
                    cart.Items!.ForEach(delegate (BO.OrderItem var)//empty cart
                    {
                        bl!.Cart.UpdateAmountInCart(cart, var.ProductID, 0); //UPDATE AMOUNT OF ITEM IN THE CART to 0
                    });
                }
                catch (BO.DoesntExistExeption)//catch for UpdateAmountInCar
                { }
                catch (BO.ContradictoryDataExeption)//catch for UpdateAmountInCar
                { }
                catch (BO.AlreadyExistExeption)//catch for UpdateAmountInCar
                { }
                catch (BO.InvalidInputExeption)//catch for UpdateAmountInCar
                { }
                this.Close();


            }
            catch (BO.AlreadyExistExeption )//catch for MakingAnOrder
            {
                MessageBox.Show("Couldn't place order");
            }
            catch(BO.ContradictoryDataExeption)//catch for MakingAnOrder
            {
                MessageBox.Show("Couldn't place order");

            }
            catch (BO.DoesntExistExeption)//catch for MakingAnOrder
            {
                MessageBox.Show("Couldn't place order");

            }
            catch (BO.InvalidInputExeption)//catch for MakingAnOrder
            {
                MessageBox.Show("Couldn't place order");

            }
        }
        /// <summary>
        /// Text box for customer name
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void nameTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if(nameTextBox.Text!=null && email.Text!=null && address.SelectedItem!=null)//all information was inputted
            {
                checkout.IsEnabled = true;//it is possible to checkout and press the checkout button
            }
        }
        /// <summary>
        /// Text box for customer email
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void email_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (nameTextBox.Text != null && email.Text != null && address.SelectedItem != null)//all information was inputted
            {
                checkout.IsEnabled = true;//it is possible to checkout and press the checkout button
            }
        }
        /// <summary>
        /// Text box for customer address
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void address_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (nameTextBox.Text != null && email.Text != null && address.SelectedItem != null)//all information was inputted
            {
                checkout.IsEnabled = true;//it is possible to checkout and press the checkout button
            }
        }
    }
}

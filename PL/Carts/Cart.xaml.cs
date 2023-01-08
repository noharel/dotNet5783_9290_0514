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
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PL.Carts
{
    /// <summary>
    /// Interaction logic for Cart.xaml
    /// </summary>
    public partial class Cart : Window
    {
        BlApi.IBl? bl = BlApi.Factory.Get(); // get bl from factory
        BO.Cart cart;
        public Cart(BO.Cart c)//constructor
        {
            InitializeComponent();
            cart = c;
            items.ItemsSource = cart.Items;//initailze items in cart
            totalPrice.Content = cart.TotalPrice;//initialize total proce
            if (cart.Items!.Count()==0)//if the cart is empty you can not place the order, so if the cart is not empty you can press the place order button
            {
                placeOrderButton.IsEnabled = false;//place order button is pressable
            }
                
        }
        /// <summary>
        /// remove all of this a product from cart
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TrashButton_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            int id = ((BO.OrderItem)button.DataContext)!.ProductID;
            try
            {
                bl!.Cart.UpdateAmountInCart(cart, id, 0);////UPDATE AMOUNT OF ITEM IN THE CART to 0
                items.ItemsSource = cart.Items;//refresh items in cart view
                totalPrice.Content = cart.TotalPrice;//refresh total price of cart view
                if (cart.Items!.Count() == 0)//if the cart is empty you can not place the order, so if the cart is not empty you can press the place order button
                {
                    placeOrderButton.IsEnabled = false;//place order button is pressable
                }
            }
            catch (BO.DoesntExistExeption)//catch for UpdateAmountInCart
            { }
            catch (BO.ContradictoryDataExeption)//catch for UpdateAmountInCart
            { }
            catch (BO.AlreadyExistExeption)//catch for UpdateAmountInCart
            { }
            catch (BO.InvalidInputExeption)//catch for UpdateAmountInCart
            { }


        }
        /// <summary>
        /// Place order button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void placeOrder_Click(object sender, RoutedEventArgs e)
        {
            var hg = new Carts.placeOrder(cart); // open place order window
            hg.ShowDialog(); // open 
            List<BO.OrderItem>? list = new List<BO.OrderItem>();
            try//empty cart
            {
                cart.Items!.ForEach(delegate (BO.OrderItem var)//empty cart
                    {
                        bl!.Cart.UpdateAmountInCart(cart, var.ProductID, 0);//update amount of product in cart to 0
                    });
                if (cart.Items!.Count() == 0)//if the cart is empty you can not place the order, so if the cart is not empty you can press the place order button
                {
                    placeOrderButton.IsEnabled = false;//place order button is pressable
                }
            }
            catch (BO.DoesntExistExeption)//catch for UpdateAmountInCart
            { }
            catch (BO.ContradictoryDataExeption)//catch for UpdateAmountInCart
            { }
            catch (BO.AlreadyExistExeption)//catch for UpdateAmountInCart
            { }
            catch (BO.InvalidInputExeption)//catch for UpdateAmountInCart
            { }
            this.Close();
        }
        /// <summary>
        /// Empty cart button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Empty_Click (object sender, RoutedEventArgs e)//for empty
        {
            try
            {
                cart.Items!.ForEach(delegate (BO.OrderItem var)//empty cart
                {
                        bl!.Cart.UpdateAmountInCart(cart, var.ProductID, 0);//update amount of product in cart to 0
                });
                items.ItemsSource = cart.Items;//refresh items in cart view
                totalPrice.Content = cart.TotalPrice;//refresh total price of cart view
                ICollectionView view = CollectionViewSource.GetDefaultView(items.ItemsSource);//refresh view
                view.Refresh();// refresh view
                if (cart.Items!.Count() == 0)//if the cart is empty you can not place the order, so if the cart is not empty you can press the place order button
                {
                    placeOrderButton.IsEnabled = false;//place order button is pressable
                }
            }
            catch (BO.DoesntExistExeption)//catch for UpdateAmountInCart
            { }
            catch (BO.ContradictoryDataExeption)//catch for UpdateAmountInCart
            { }
            catch (BO.AlreadyExistExeption)//catch for UpdateAmountInCart
            { }
            catch (BO.InvalidInputExeption)//catch for UpdateAmountInCart
            { }
        }
        /// <summary>
        /// Add 1 more of a certain product to the cart
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ADD_Click(object sender, RoutedEventArgs e)//for empty
        {
            Button button = (Button)sender;
            int id = ((BO.OrderItem)button.DataContext)!.ProductID;//get id of product in cart
            int amount = ((BO.OrderItem)button.DataContext)!.Amount;//get the current amount of product in cart

            try
            {
                bl!.Cart.UpdateAmountInCart(cart, id, amount+1);//update amount of product in cart to 1 more
                totalPrice.Content = cart.TotalPrice;//refresh items in cart view
                items.ItemsSource = cart.Items;//refresh total price of cart view
                ICollectionView view = CollectionViewSource.GetDefaultView(items.ItemsSource);//refreash view
                view.Refresh();//refresh view
                if (cart.Items!.Count() == 0)//if the cart is empty you can not place the order, so if the cart is not empty you can press the place order button
                {
                    placeOrderButton.IsEnabled = false;//place order button is pressable
                }
            }
            catch (BO.DoesntExistExeption)//catch for UpdateAmountInCart
            { }
            catch (BO.ContradictoryDataExeption ex)//catch for UpdateAmountInCart
            {
                MessageBox.Show(ex.Message); // print exception 
            }
            catch (BO.AlreadyExistExeption)//catch for UpdateAmountInCart
            { }
            catch (BO.InvalidInputExeption)//catch for UpdateAmountInCart
            { }

        }
        /// <summary>
        /// Remove 1 more of a certain product to the cart
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Remove_Click(object sender, RoutedEventArgs e)//for empty
        {
            Button button = (Button)sender;
            int id = ((BO.OrderItem)button.DataContext)!.ProductID;//get id of product in cart
            int amount = ((BO.OrderItem)button.DataContext)!.Amount;//get the current amount of product in cart

            try
            {
                bl!.Cart.UpdateAmountInCart(cart, id, amount - 1);//update amount of product in cart to 1 less
                totalPrice.Content = cart.TotalPrice;//refresh items in cart view
                items.ItemsSource = cart.Items;//refresh total price of cart view
                ICollectionView view = CollectionViewSource.GetDefaultView(items.ItemsSource);//refreash view
                view.Refresh();//refresh view
                if (cart.Items!.Count() == 0)//if the cart is empty you can not place the order, so if the cart is not empty you can press the place order button
                {
                    placeOrderButton.IsEnabled = false;//place order button is pressable
                }
            }
            catch (BO.DoesntExistExeption)//catch for UpdateAmountInCart
            { }
            catch (BO.ContradictoryDataExeption ex)//catch for UpdateAmountInCart
            {
                MessageBox.Show(ex.Message); // print exception 
            }
            catch (BO.AlreadyExistExeption)//catch for UpdateAmountInCart
            { }
            catch (BO.InvalidInputExeption)//catch for UpdateAmountInCart
            { }
        }
        /// <summary>
        /// Go back to catalog button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void back_Click(object sender, RoutedEventArgs e)
        {
            this.Close();//close this window, and end up in previously opened window-catalog
        }
    }
} 


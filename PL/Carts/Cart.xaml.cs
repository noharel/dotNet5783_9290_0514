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
        public Cart(BO.Cart c)
        {
            InitializeComponent();
            cart = c;
            items.ItemsSource = cart.Items;
            totalPrice.Content = cart.TotalPrice;
        }
        private void TrashButton_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            int id = ((BO.OrderItem)button.DataContext)!.ProductID;
            try
            {
                bl!.Cart.UpdateAmountInCart(cart, id, 0);
                items.ItemsSource = cart.Items;
                totalPrice.Content = cart.TotalPrice;

            }
            catch (BO.DoesntExistExeption)
            { }
            catch (BO.ContradictoryDataExeption)
            { }
            catch (BO.AlreadyExistExeption)
            { }
            catch (BO.InvalidInputExeption)
            { }

        }
        private void placeOrder_Click(object sender, RoutedEventArgs e)
        {
            var hg = new Carts.placeOrder(cart); // open place order window
            hg.ShowDialog(); // open 
            List<BO.OrderItem>? list = new List<BO.OrderItem>();
            cart = new BO.Cart { Items = list };
            this.Close();


        }

        private void Empty_Click (object sender, RoutedEventArgs e)//for empty
        {
            try
            {
                cart.Items!.ForEach(delegate (BO.OrderItem var)
                    {
                        bl!.Cart.UpdateAmountInCart(cart, var.ProductID, 0);
                    });
                items.ItemsSource = cart.Items;
                totalPrice.Content = cart.TotalPrice;
                ICollectionView view = CollectionViewSource.GetDefaultView(items.ItemsSource);
                view.Refresh();
            }
            catch (BO.DoesntExistExeption)
            { }
            catch (BO.ContradictoryDataExeption)
            { }
            catch (BO.AlreadyExistExeption)
            { }
            catch (BO.InvalidInputExeption)
            { }
        }
        private void ADD_Click(object sender, RoutedEventArgs e)//for empty
        {
            Button button = (Button)sender;
            int id = ((BO.OrderItem)button.DataContext)!.ProductID;
            int amount = ((BO.OrderItem)button.DataContext)!.Amount;

            try
            {
                bl!.Cart.UpdateAmountInCart(cart, id, amount+1);
                totalPrice.Content = cart.TotalPrice;
                items.ItemsSource = cart.Items;
                ICollectionView view = CollectionViewSource.GetDefaultView(items.ItemsSource);
                view.Refresh();
            }
            catch (BO.DoesntExistExeption)
            { }
            catch (BO.ContradictoryDataExeption ex)
            {
                MessageBox.Show(ex.Message); // print exception 
            }
            catch (BO.AlreadyExistExeption)
            { }
            catch (BO.InvalidInputExeption)
            { }

        }
        private void Remove_Click(object sender, RoutedEventArgs e)//for empty
        {
            Button button = (Button)sender;
            int id = ((BO.OrderItem)button.DataContext)!.ProductID;
            int amount = ((BO.OrderItem)button.DataContext)!.Amount;

            try
            {
                bl!.Cart.UpdateAmountInCart(cart, id, amount - 1);
                items.ItemsSource = cart.Items;
                totalPrice.Content = cart.TotalPrice;
                ICollectionView view = CollectionViewSource.GetDefaultView(items.ItemsSource);
                view.Refresh();
            }
            catch (BO.DoesntExistExeption)
            { }
            catch (BO.ContradictoryDataExeption ex)
            {
                MessageBox.Show(ex.Message); // print exception 
            }
            catch (BO.AlreadyExistExeption)
            { }
            catch (BO.InvalidInputExeption)
            { }
        }
        private void back_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
} 


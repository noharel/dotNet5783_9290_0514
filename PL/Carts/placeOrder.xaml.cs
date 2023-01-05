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
using BO;

namespace PL.Carts
{
    /// <summary>
    /// Interaction logic for placeOrder.xaml
    /// </summary>
    public partial class placeOrder : Window
    {
        BlApi.IBl? bl = BlApi.Factory.Get(); // get bl from factory
        BO.Cart cart;
        public placeOrder(BO.Cart c)
        {
            InitializeComponent();
            cart = c;
            address.ItemsSource = Enum.GetValues(typeof(BO.Cities));
        }

        private void checkout_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                cart.CustomerName = nameTextBox.Text;
                cart.CustomerEmail = email.Text;
                cart.CustomerAddress = address.SelectedItem.ToString();
                bl?.Cart.MakingAnOrder(cart);
                MessageBox.Show("Order placed, enjoy your new car!");

            }
            catch (BO.AlreadyExistExeption )
            {
                MessageBox.Show("Couldn't place order");
            }
            catch(BO.ContradictoryDataExeption)
            {
                MessageBox.Show("Couldn't place order");

            }
            catch (BO.DoesntExistExeption )
            {
                MessageBox.Show("Couldn't place order");

            }
            catch (BO.InvalidInputExeption)
            {
                MessageBox.Show("Couldn't place order");

            }
            this.Close();
        }
    }
}

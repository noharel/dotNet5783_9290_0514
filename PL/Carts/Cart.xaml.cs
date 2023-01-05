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
           
        }
        private void TrashButton_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            int id = ((BO.OrderItem)button.DataContext)!.ProductID;
            try
            {
                bl!.Cart.UpdateAmountInCart(cart, id, 0);
                items.ItemsSource = cart.Items;
            }
            catch (BO.DoesntExistExeption)
            {
                MessageBox.Show("Can't remove"); // print exception 

            }
            catch (BO.ContradictoryDataExeption)
            {
                MessageBox.Show("Can't remove"); // print exception 
            }

        }
        private void AmountSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //int amount = (int)AmountSelector.SelectedItem;
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
} 


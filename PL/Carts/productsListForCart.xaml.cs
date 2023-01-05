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
using PL.Products;
using System.Runtime.CompilerServices;
using MaterialDesignThemes.Wpf;

namespace PL.Carts
{
    /// <summary>
    /// Interaction logic for productsListForCart.xaml
    /// </summary>
    public partial class productsListForCart : Window
    {
        BlApi.IBl? bl = BlApi.Factory.Get(); // get bl from factory
        private static BO.Cart? cart;
        List<BO.ProductForList?> listProd;

        public productsListForCart()
        {
            InitializeComponent();
            listProd = bl.Product.GetListProduct().ToList();
            producs.ItemsSource = listProd;
            List<BO.OrderItem>? list = new List<BO.OrderItem>();
            cart = new BO.Cart { Items = list };
            amountInCart.Content = cart!.Items!.ToList().Count.ToString();
            CategorySelector.ItemsSource = Enum.GetValues(typeof(BO.Category)); // put them in the category selector

        }
        /*
        private void producs_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            BO.ProductForList prodForList = (BO.ProductForList)producs.SelectedItem;
            var hg = new Carts.productItem(prodForList.ID, cart!); // open ProductWindow on update and action mode
            hg.ShowDialog(); // open 
            amountInCart.Content = cart!.Items!.ToList().Count.ToString();
        }*/
        private void AddButton_click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            int id = ((BO.ProductForList?)button.DataContext)!.ID;
            try
            {
                int count = 0;
                int num = 0;
                cart!.Items!.ToList().ForEach(delegate (BO.OrderItem var)
                {
                    if (var.ProductID == id) num = var.Amount;
                    count += var.Amount;

                });
                if (num != 0)
                {
                    bl!.Cart.UpdateAmountInCart(cart!, id, num +1);
                }
                else {
                    bl!.Cart.AddProductToCart(cart!, id); }
                
                amountInCart.Content = count+1;
                
            }
            catch (BO.DoesntExistExeption)
            {
                MessageBox.Show("Can't add to cart, product is out of stock"); // print exception 

            }
            catch (BO.ContradictoryDataExeption ex)
            {
                MessageBox.Show(ex.Message); // print exception 
            }


        }
        private void RemoveButton_click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            int id = ((BO.ProductForList?)button.DataContext)!.ID;
            try
            {
                int count = 0;
                int num = 0;
                cart!.Items!.ToList().ForEach(delegate (BO.OrderItem var)
                {
                    if (var.ProductID == id) num = var.Amount;
                    count += var.Amount;

                });
                if (num != 0)
                {
                    bl!.Cart.UpdateAmountInCart(cart!, id, num - 1);
                    amountInCart.Content = count - 1;
                }
                else
                {
                    MessageBox.Show("Can't remove, product is not in the cart"); // print exception 
                }


            }
            catch (BO.DoesntExistExeption)
            {
                MessageBox.Show("Can't remove, product is not in the cart"); // print exception 

            }
            catch(BO.ContradictoryDataExeption ex)
            {
                MessageBox.Show(ex.Message); // print exception 
            }



        }
        private void InfoButton_click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            int id = ((BO.ProductForList?)button.DataContext)!.ID;
            var hg = new Carts.productItem(id, cart!); // open ProductWindow on update and action mode
            hg.ShowDialog(); // open 
        }
        private void CategorySelector_SelectionChanged(object sender, SelectionChangedEventArgs e) //get selection for category
        {
            BO.Category? category = (BO.Category?)CategorySelector.SelectedItem; // get the asked category
            try
            {
                //return all the products in the asked category
                producs.ItemsSource = bl!.Product.GetListProduct(x => x == null ? throw new Exception() : x.Category == category);
            }
            catch (BO.DoesntExistExeption ex) // get list product exception
            {
                string innerEx = "";
                if (ex.InnerException != null)
                    innerEx = ": " + ex.InnerException.Message;
                MessageBox.Show("unsucessfull selection:" + ex.Message + innerEx); // for user print exception

            }

        }
        private void ClearChoice_Click_1(object sender, RoutedEventArgs e) //clear choice button
        {
            try
            {
                producs.ItemsSource = bl!.Product.GetListProduct(); //get all the products
                CategorySelector.SelectedIndex = -1;
                producs.ItemsSource = listProd;
            }
            catch (BO.DoesntExistExeption ex) //get list product exception
            {
                string innerEx = "";
                if (ex.InnerException != null)
                    innerEx = ": " + ex.InnerException.Message;
                MessageBox.Show("unsucessfull click:" + ex.Message + innerEx); //print exception for user

            }
        }
        private void CartButton_Click(object sender, RoutedEventArgs e) // cart button
        {
            var hg = new Carts.Cart(cart!); // open ProductWindow on update and action mode
            hg.ShowDialog(); // open 
            int count = 0;
            cart!.Items!.ToList().ForEach(delegate (BO.OrderItem var)
            {
                count += var.Amount;
            });
            amountInCart.Content = count;
        }
    }
}

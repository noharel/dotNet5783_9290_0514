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
        List<BO.ProductForList?>? listProd;

        public productsListForCart()//constructor
        {
            try
            {
                InitializeComponent();
                listProd = bl.Product.GetListProduct().ToList();//GET LIST OF PRODUCT
                producs.ItemsSource = listProd;//for listview of products
                List<BO.OrderItem>? list = new List<BO.OrderItem>();
                cart = new BO.Cart { Items = list };//initailize cart
                amountInCart.Content = cart!.Items!.ToList().Count.ToString();//for amount in cart
                CategorySelector.ItemsSource = Enum.GetValues(typeof(BO.Category)); // put them in the category selector
            }
            catch(BO.DoesntExistExeption ex)
            {
                MessageBox.Show( ex.Message+", "+ex.InnerException!.Message); // print exception 

            }
        }
        /// <summary>
        /// Add product to cart button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddButton_click(object sender, RoutedEventArgs e)//for ADD product to cart button 
        {
            Button button = (Button)sender;
            int id = ((BO.ProductForList?)button.DataContext)!.ID;
            try
            {
                int count = 0;
                int num = 0;
                cart!.Items!.ToList().ForEach(delegate (BO.OrderItem var)//count total amount of products in cart and amount of product with this id 
                {
                    if (var.ProductID == id) num = var.Amount;
                    count += var.Amount;

                });
                if (num != 0)//if this product is already in cart
                {
                    bl!.Cart.UpdateAmountInCart(cart!, id, num +1);
                }
                else {//product is not already in cart
                    bl!.Cart.AddProductToCart(cart!, id); }
                
                amountInCart.Content = count+1;
                
            }
            catch (BO.DoesntExistExeption)//catch for UpdateAmountInCart, AddProductToCart
            {
                MessageBox.Show("Can't add to cart, product is out of stock"); // print exception 

            }
            catch (BO.ContradictoryDataExeption ex)//catch for UpdateAmountInCart, AddProductToCart
            {
                MessageBox.Show(ex.Message); // print exception 
            }


        }
        /// <summary>
        /// Remove product from cart button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RemoveButton_click(object sender, RoutedEventArgs e)//for REMOVE product to cart button 
        {
            Button button = (Button)sender;
            int id = ((BO.ProductForList?)button.DataContext)!.ID;
            try
            {
                int count = 0;
                int num = 0;
                cart!.Items!.ToList().ForEach(delegate (BO.OrderItem var)//count total amount of products in cart and amount of product with this id 
                {
                    if (var.ProductID == id) num = var.Amount;
                    count += var.Amount;

                });
                if (num != 0)//if this product is already in cart
                {
                    bl!.Cart.UpdateAmountInCart(cart!, id, num - 1);
                    amountInCart.Content = count - 1;
                }
                else//product is  not already in cart
                {
                    MessageBox.Show("Can't remove, product is not in the cart"); // print exception 
                }

            }
            catch (BO.DoesntExistExeption)//catch for UpdateAmountInCart, AddProductToCart
            {
                MessageBox.Show("Can't remove, product is not in the cart"); // print exception 

            }
            catch(BO.ContradictoryDataExeption ex)//catch for UpdateAmountInCart, AddProductToCart
            {
                MessageBox.Show(ex.Message); // print exception 
            }

        }
        /// <summary>
        /// Open info window for product button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void InfoButton_click(object sender, RoutedEventArgs e)//for information button
        {
            Button button = (Button)sender;
            int id = ((BO.ProductForList?)button.DataContext)!.ID;
            var hg = new Carts.productItem(id, cart!); // open ProductItem window to view information of product
            hg.ShowDialog(); // open 
        }
        /// <summary>
        /// Selection chenged on categry selector
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <exception cref="Exception"></exception>
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
        /// <summary>
        /// Clear Category choice selection button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ClearChoice_Click_1(object sender, RoutedEventArgs e) //clear choice button
        {
            try
            {
                producs.ItemsSource = bl!.Product.GetListProduct(); //get all the products
                CategorySelector.SelectedIndex = -1;//initialize the selected category on screen
                producs.ItemsSource = listProd;//refresh products 
            }
            catch (BO.DoesntExistExeption ex) //get list product exception
            {
                string innerEx = "";
                if (ex.InnerException != null)
                    innerEx = ": " + ex.InnerException.Message;
                MessageBox.Show("unsucessfull click:" + ex.Message + innerEx); //print exception for user

            }
        }
        /// <summary>
        /// Open cart window button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CartButton_Click(object sender, RoutedEventArgs e) // cart button
        {
            var hg = new Carts.Cart(cart!); // open ProductWindow on update and action mode
            hg.ShowDialog(); // open 
            int count = 0;
            cart!.Items!.ToList().ForEach(delegate (BO.OrderItem var)//count total amount of products in cart
            {
                count += var.Amount;
            });
            amountInCart.Content = count;//for amount in cart
        }
    }
}

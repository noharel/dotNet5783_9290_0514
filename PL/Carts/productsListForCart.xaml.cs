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
using PL.Products;

namespace PL.Carts
{
    /// <summary>
    /// Interaction logic for productsListForCart.xaml
    /// </summary>
    public partial class productsListForCart : Window
    {
        BlApi.IBl? bl = BlApi.Factory.Get(); // get bl from factory
        BO.Cart cart;
        public productsListForCart()
        {
            InitializeComponent();
            producs.ItemsSource = bl.Product.GetListProduct().ToList();
            List<BO.OrderItem>? list = new List<BO.OrderItem>();
            cart = new BO.Cart { Items = list };
        }

        private void producs_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            BO.ProductForList prodForList = (BO.ProductForList)producs.SelectedItem;
            var hg = new Carts.productItem(prodForList.ID, cart); // open ProductWindow on update and action mode
            hg.ShowDialog(); // open 


            /*try
            {
                producs.ItemsSource = bl!.Product.GetListProduct(); // update list of product to see changes
            }
            catch (BO.DoesntExistExeption ex) // get list product exception
            {
                string innerEx = "";
                if (ex.InnerException != null)
                    innerEx = ": " + ex.InnerException.Message;
                MessageBox.Show("unsucessfull selection:" + ex.Message + innerEx); //print exception for user

            }*/
        }
    }
}

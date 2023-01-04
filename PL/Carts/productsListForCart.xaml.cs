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
namespace PL.Carts
{
    /// <summary>
    /// Interaction logic for productsListForCart.xaml
    /// </summary>
    public partial class productsListForCart : Window
    {
        BlApi.IBl? bl = BlApi.Factory.Get(); // get bl from factory
        private static BO.Cart? cart;
        public productsListForCart()
        {
            InitializeComponent();

            List<BO.ProductForList?> listProd = bl.Product.GetListProduct().ToList();
            producs.ItemsSource = listProd;
            List<BO.OrderItem>? list = new List<BO.OrderItem>();
            cart = new BO.Cart { Items = list };
            amountInCart.Content = cart!.Items!.ToList().Count.ToString();
        }        
        
        private void producs_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            BO.ProductForList prodForList = (BO.ProductForList)producs.SelectedItem;
            var hg = new Carts.productItem(prodForList.ID, cart!); // open ProductWindow on update and action mode
            hg.ShowDialog(); // open 
            amountInCart.Content = cart!.Items!.ToList().Count.ToString();
        }
    }
}

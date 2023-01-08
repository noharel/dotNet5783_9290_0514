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
    /// Interaction logic for productItem.xaml
    /// </summary>
    public partial class productItem : Window
    {
        BlApi.IBl? bl = BlApi.Factory.Get();  // get bl
        public productItem(int x, BO.Cart cart)//constructor
        {
            try
            {
                InitializeComponent();
                id.Text = x.ToString();//for id
                BO.ProductItem prodItem = bl.Product.GetProductInfo_client(x, cart); //Get Product in cart Info for client
                name.Text = prodItem.Name;//for name
                category.Text = prodItem.Category.ToString();//for category
                amount.Text = prodItem.Amount.ToString();//for amount
                if (prodItem.InStock)//for in stock
                    inStock.Text = "Product is currently in stock";
            }
            catch(BO.DoesntExistExeption ex)//catch for GetProductInfo_client
            {
                MessageBox.Show("Can't  get product info: " + ex.Message); // print exception 
            }
            catch (BO.InvalidInputExeption ex)//catch for GetProductInfo_client
            {
                MessageBox.Show("Can't  get product info: " + ex.Message); // print exception 
            }
        }
    }
}
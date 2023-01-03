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
        public productItem(int x, BO.Cart cart)
        {
            InitializeComponent();
            id.Text= x.ToString();
            BO.ProductItem prodItem = bl.Product.GetProductInfo_client(x,cart );
            name.Text = prodItem.Name;
            category.Text = prodItem.Category.ToString();
            amount.Text = prodItem.Amount.ToString();
            if (prodItem.InStock) 
                inStock.Text = "Product is currently in stock";
        }
    }
}

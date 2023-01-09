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
        public productItem(BO.ProductItem prodItem)//constructor
        {
            try
            {
                InitializeComponent();
                this.DataContext = prodItem;
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
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PL
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        BlApi.IBl? bl = BlApi.Factory.Get(); // get bl from factory

        public MainWindow() // constructor
        {
            InitializeComponent(); 
        }

        private void ProductsMouseEnter(object sender, MouseEventArgs e) // product buttom
        {
            // open the ProductListWindow
            var win = new PL.Products.ProductListWindow();
            win.Owner = this; // for the new window to be first
            win.Show();
        }
    }
}

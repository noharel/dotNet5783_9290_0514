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

namespace PL.Products;

/// <summary>
/// Interaction logic for ProductForList.xaml
/// </summary>
public partial class ProductListWindow : Window
{
    BlApi.IBl? bl = BlApi.Factory.Get();
    public ProductListWindow()
    {
        InitializeComponent();
        ProductListView.ItemsSource = bl.Product.GetListProduct();
        CategorySelector.ItemsSource = Enum.GetValues(typeof(BO.Category));
        this.ClearChoice.MouseDoubleClick += ClearChoice_Click_1;
        this.AddButton.MouseDoubleClick += AddButton_Click;



    }




    private void CategorySelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        BO.Category? category = (BO.Category?)CategorySelector.SelectedItem;
        try
        {
            ProductListView.ItemsSource = bl!.Product.GetListProduct(x => x == null ? throw new Exception() : x.Category == category);
        }
        catch(BO.DoesntExistExeption ex)
        {
            string innerEx = "";
            if (ex.InnerException != null)
                innerEx = ": " + ex.InnerException.Message;
            MessageBox.Show("unsucessfull selection:" + ex.Message + innerEx);

        }

    }

    private void ClearChoice_Click_1(object sender, RoutedEventArgs e)
    {
        try
        {
            ProductListView.ItemsSource = bl!.Product.GetListProduct();
        }
        catch (BO.DoesntExistExeption ex)
        {
            string innerEx = "";
            if (ex.InnerException != null)
                innerEx = ": " + ex.InnerException.Message;
            MessageBox.Show("unsucessfull click:" + ex.Message + innerEx);

        }
    }



    private void AddButton_Click(object sender, RoutedEventArgs e)
    {
        new ProductWindow().ShowDialog();
        try
        {
            ProductListView.ItemsSource = bl!.Product.GetListProduct();
        }
        catch (BO.DoesntExistExeption ex)
        {
            string innerEx = "";
            if (ex.InnerException != null)
                innerEx = ": " + ex.InnerException.Message;
            MessageBox.Show("unsucessfull click:" + ex.Message + innerEx);

        }

    }

    private void ProductListView_SelectionChanged(object sender, MouseButtonEventArgs e)
    {
        var hg = new ProductWindow((BO.ProductForList)ProductListView.SelectedItem);
        hg.ShowDialog();
        try
        {
            ProductListView.ItemsSource = bl!.Product.GetListProduct();
        }
        catch (BO.DoesntExistExeption ex)
        {
            string innerEx = "";
            if (ex.InnerException != null)
                innerEx = ": " + ex.InnerException.Message;
            MessageBox.Show("unsucessfull selection:" + ex.Message + innerEx);

        }
    }
};

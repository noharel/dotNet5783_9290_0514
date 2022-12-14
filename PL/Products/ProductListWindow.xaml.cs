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
    BlApi.IBl? bl = BlApi.Factory.Get();  // get bl
    public ProductListWindow() // constructor
    {
        InitializeComponent();
        ProductListView.ItemsSource = bl.Product.GetListProduct(); // get all products
        CategorySelector.ItemsSource = Enum.GetValues(typeof(BO.Category)); // put them in the category selector
        this.ClearChoice.MouseDoubleClick += ClearChoice_Click_1; // clear choice button
        this.AddButton.MouseDoubleClick += AddButton_Click; // add button
    }




    private void CategorySelector_SelectionChanged(object sender, SelectionChangedEventArgs e) //get selection for category
    {
        BO.Category? category = (BO.Category?)CategorySelector.SelectedItem; // get the asked category
        try
        {
            //return all the products in the asked category
            ProductListView.ItemsSource = bl!.Product.GetListProduct(x => x == null ? throw new Exception() : x.Category == category);
        }
        catch(BO.DoesntExistExeption ex) // get list product exception
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
            ProductListView.ItemsSource = bl!.Product.GetListProduct(); //get all the products
        }
        catch (BO.DoesntExistExeption ex) //get list product exception
        {
            string innerEx = "";
            if (ex.InnerException != null)
                innerEx = ": " + ex.InnerException.Message;
            MessageBox.Show("unsucessfull click:" + ex.Message + innerEx); //print exception for user

        }
    }



    private void AddButton_Click(object sender, RoutedEventArgs e) //add button 
    {
        new ProductWindow().ShowDialog(); // open ProductWindow

        try
        {
            ProductListView.ItemsSource = bl!.Product.GetListProduct(); // update the list product to see the new one
        }
        catch (BO.DoesntExistExeption ex) // gat list product exception
        {
            string innerEx = "";
            if (ex.InnerException != null)
                innerEx = ": " + ex.InnerException.Message;
            MessageBox.Show("unsucessfull click:" + ex.Message + innerEx); // print exceprion for user

        }

    }

    private void ProductListView_SelectionChanged(object sender, MouseButtonEventArgs e) // for updates and other actions- click on certain product
    {
        var hg = new ProductWindow((BO.ProductForList)ProductListView.SelectedItem); // open ProductWindow on udate and action mode
        hg.ShowDialog(); // open 
        

        try
        {
            ProductListView.ItemsSource = bl!.Product.GetListProduct(); // update list of product to see changes
        }
        catch (BO.DoesntExistExeption ex) // get list product exception
        {
            string innerEx = "";
            if (ex.InnerException != null)
                innerEx = ": " + ex.InnerException.Message;
            MessageBox.Show("unsucessfull selection:" + ex.Message + innerEx); //print exception for user

        }
    }
};

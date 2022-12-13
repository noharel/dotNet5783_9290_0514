using BlApi;
using BlImplementation;
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
/// Interaction logic for Product.xaml
/// </summary>
public partial class ProductWindow : Window
{
    IBl bl = new Bl();
    BO.Category category;
    string name;
    int inStock, id, price;
    public ProductWindow()
    {
        InitializeComponent();
        inputCategory.ItemsSource = Enum.GetValues(typeof(BO.Category));
        this.completeAdd.MouseDoubleClick += completeAdd_Click;
    }

    private void CompleteAdd_MouseDoubleClick(object sender, MouseButtonEventArgs e)
    {
        BO.Product product = new BO.Product() { ID = id, Category = category, InStock = inStock, Name = name, Price = price };
        try
        {
            bl.Product.AddProdut(product);
            added.Visibility = Visibility.Visible; //איך מעדכנים את הXML
            //added.
        }
        // מה עושיפעם החריגות פה והאם צירך לבדוק קלט לפני שליחה לADD
        catch (BO.DoesntExistExeption ex)
        {
            //unimlemented
        }
        catch (BO.InvalidInputExeption ex)
        {
            //unimlemented
        }
        catch (BO.ContradictoryDataExeption ex)
        {
            //unimlemented
        }


    }

    private void TextBoxID(object sender, TextChangedEventArgs e)
    {
        int.TryParse(inputID.Text,out id);
    }

    private void inputCategoryFunc(object sender, SelectionChangedEventArgs e)
    {
       category = (BO.Category)inputCategory.SelectedItem;

    }
    
    private void completeAdd_Click(object sender, RoutedEventArgs e)
    {
        BO.Product product = new BO.Product() { ID = id, Category = category, InStock = inStock, Name = name, Price = price };
        bl.Product.AddProdut(product);
    }

    private void inputNameFunc(object sender, TextChangedEventArgs e)
    {
        name=inputName.Text;
    }

    private void inputInStockFunc(object sender, TextChangedEventArgs e)
    {
        int.TryParse(inputInStock.Text, out inStock);

    }

    private void inputPriceFunc(object sender, TextChangedEventArgs e)
    {
        int.TryParse(inputPrice.Text,out price);
    }
}

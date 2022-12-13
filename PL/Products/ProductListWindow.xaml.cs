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
/// Interaction logic for ProductForList.xaml
/// </summary>
public partial class ProductListWindow : Window
{
    IBl bl = new Bl();
    public ProductListWindow()
    {
        InitializeComponent();
        // d:ItemsSource="{d:SampleData ItemCount=5}"
        ProductListView.ItemsSource = bl.Product.GetListProduct();
        CategorySelector.ItemsSource = Enum.GetValues(typeof(BO.Category));
        this.ClearChoice.MouseDoubleClick += ClearChoice_Click_1;
        this.AddButton.MouseDoubleClick += AddButton_Click;



    }



    /*private void ProductListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        Category? category = (Category?)CategorySelector.SelectedItem;
        ProductListView.ItemsSource = bl.Product.GetListProduct(x => x == null ? throw new Exception(): x.Category == category) ;

    }*/

    private void CategorySelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        BO.Category? category = (BO.Category?)CategorySelector.SelectedItem;
        ProductListView.ItemsSource = bl.Product.GetListProduct(x => x == null ? throw new Exception() : x.Category == category);

    }

    private void ClearChoice_Click_1(object sender, RoutedEventArgs e)
    {
        ProductListView.ItemsSource = bl.Product.GetListProduct();

    }

    // private void addButton(object sender, RoutedEventArgs e) => new ;

    private void AddButton_Click(object sender, RoutedEventArgs e) => new ProductWindow().Show();

    private void ProductListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {

    }
};

 //<ListView Name = "ProductListView" Grid.Row="1" d:ItemsSource="{d:SampleData ItemCount=5}" SelectionChanged="CategorySelector_SelectionChanged" />
 //           <ListView.View>
 //             <GridView/>
 //           </ListView.View>

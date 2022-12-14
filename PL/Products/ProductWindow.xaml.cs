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
    BlApi.IBl? bl = BlApi.Factory.Get();
    BO.Category? category;
    string ?nameOfProd;
    int inStock = int.MinValue;
    int id = int.MinValue;
    double price= int.MinValue;
    BO.ProductForList? prod;
    public ProductWindow(BO.ProductForList? product = null)
    {
        prod = product;
        InitializeComponent();
        inputCategory.ItemsSource = Enum.GetValues(typeof(BO.Category));
        if(product != null)//for update
        {
            completeAdd.Visibility = Visibility.Collapsed;
            completeUpdate.Visibility = Visibility.Visible;
            int id = product.ID;
            
            inputID.Text=(id.ToString());
            inputName.Text=(product.Name);
            inputPrice.Text = (product.Price.ToString());
            inputInStock.Text=(bl.Product.GetProductInfo_manager(product.ID)).InStock.ToString();
            inputCategory.Text=product.Category.ToString();
            inputID.IsEnabled = false;
        }
        else//for add
        {
            completeAdd.Visibility = Visibility.Visible;
            completeUpdate.Visibility = Visibility.Collapsed;
            completeAdd.IsEnabled = false;
            //update button should be visible
        }

    }

    private void CompleteAdd_MouseDoubleClick(object sender, RoutedEventArgs e)
    {
        BO.Product product = new BO.Product() { ID = id, Category = category, InStock = inStock, Name = nameOfProd, Price = price };
        try
        {
            bl!.Product.AddProdut(product);
            MessageBox.Show("Product added succefully");
            this.Close();
        }
        catch (BO.DoesntExistExeption ex)
        {
            string innerEx = "";
            if(ex.InnerException!=null)
                innerEx = ": "+ex.InnerException.Message;
            MessageBox.Show("unsucessfull addition:"+ex.Message+innerEx);    
            
        }
        catch (BO.InvalidInputExeption ex)
        {
            string innerEx = "";
            if (ex.InnerException != null)
                innerEx = ": " + ex.InnerException.Message;
            MessageBox.Show("unsucessfull addition:" + ex.Message + innerEx);


        }
        catch (BO.ContradictoryDataExeption ex)
        {
            string innerEx = "";
            if (ex.InnerException != null)
                innerEx = ": " + ex.InnerException.Message;
            MessageBox.Show("unsucessfull addition:" + ex.Message + innerEx);

        }
    }

    private void TextBoxID(object sender, TextChangedEventArgs e)
    {
        if((int.TryParse(inputID.Text, out id))&&(completeUpdate.Visibility==Visibility.Collapsed))//for add and input was a number
        {
            if((id>0)&& ((id.ToString()).Length == 6))//valid id
            {
                if ((id != int.MinValue) && (price != int.MinValue) && (inStock != int.MinValue) && (nameOfProd != null) && (category != null))
                {
                    completeAdd.IsEnabled = true;
                }
            }
            else
            {
                id = -1;//as if no id was inputted
            }
        }
    }

    private void inputCategoryFunc(object sender, SelectionChangedEventArgs e)
    {
        category = (BO.Category)inputCategory.SelectedItem;
        if (completeUpdate.Visibility == Visibility.Collapsed)//for add
        {
            if ((id != int.MinValue) && (price != int.MinValue) && (inStock != int.MinValue) && (nameOfProd != null) && (category != null))
            {
                completeAdd.IsEnabled = true;
            }
        }
    }


    private void inputNameFunc(object sender, TextChangedEventArgs e)
    {
        nameOfProd =inputName.Text;
        if (completeUpdate.Visibility == Visibility.Collapsed)//for add 
        {
            if ((id != int.MinValue) && (price != int.MinValue) && (inStock != int.MinValue) && (nameOfProd != null) && (category != null))
            {
                completeAdd.IsEnabled = true;
            }
        }
    }

    private void completeUpdate_Click(object sender, RoutedEventArgs e)
    {
        if (price == int.MinValue) price = prod!.Price;
        if (nameOfProd == null) nameOfProd = prod!.Name;
        if(inStock== int.MinValue) inStock= (bl!.Product.GetProductInfo_manager(prod!.ID)).InStock;
        if (category == null) category = prod!.Category;
        BO.Product product = new BO.Product() { ID = id, Category = category, InStock = inStock, Name = nameOfProd, Price = price };

        try
        {
            bl!.Product.UpdateProduct(product);
            MessageBox.Show("Product updated succefully");
            this.Close();
        }
        catch (BO.DoesntExistExeption ex)
        {
            string innerEx = "";
            if (ex.InnerException != null)
                innerEx = ": " + ex.InnerException.Message;
            MessageBox.Show("unsucessfull update:" + ex.Message + innerEx);

        }
        catch (BO.InvalidInputExeption ex)
        {
            string innerEx = "";
            if (ex.InnerException != null)
                innerEx = ": " + ex.InnerException.Message;
            MessageBox.Show("unsucessfull update:" + ex.Message + innerEx);

        }
        catch (BO.ContradictoryDataExeption ex)
        {
            string innerEx = "";
            if (ex.InnerException != null)
                innerEx = ": " + ex.InnerException.Message;
            MessageBox.Show("unsucessfull update:" + ex.Message + innerEx);

        }



    }

    private void inputInStockFunc(object sender, TextChangedEventArgs e)
    {
       
        if ((int.TryParse(inputInStock.Text, out inStock))&&(completeUpdate.Visibility == Visibility.Collapsed))//for add and input was a number
        {
            if ((id != int.MinValue) && (price != int.MinValue) && (inStock != int.MinValue) && (nameOfProd != null) && (category != null))
            {
                completeAdd.IsEnabled = true;
            }
        }
    }

    private void inputPriceFunc(object sender, TextChangedEventArgs e)
    {
        
        if ((double.TryParse(inputPrice.Text, out price))&&(completeUpdate.Visibility == Visibility.Collapsed))//for add and input was a number
        {
            if ((id != int.MinValue) && (price != int.MinValue) && (inStock != int.MinValue) && (nameOfProd != null) && (category != null))
            {
                completeAdd.IsEnabled = true;
            }
        }
    }
}

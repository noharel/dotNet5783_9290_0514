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
    BlApi.IBl? bl = BlApi.Factory.Get(); // get bl

    BO.Category? category;
    string? nameOfProd;
    int inStock = int.MinValue;
    int id = int.MinValue;
    double price= int.MinValue;
    BO.ProductForList? prod;

    public ProductWindow(BO.ProductForList? product = null) // cunstructor
    {
        prod = product;
        InitializeComponent();
        inputCategory.ItemsSource = Enum.GetValues(typeof(BO.Category)); // to choose category for the product

        if(product != null) // for update
        {
            completeAdd.Visibility = Visibility.Collapsed; // add button not available
            completeUpdate.Visibility = Visibility.Visible; 
            int id = product.ID;
            DataContext = product;
            // put all the values of the product
            inputID.Text=id.ToString();
            inputName.Text=product.Name;
            inputPrice.Text = product.Price.ToString();
            inputInStock.Text=(bl.Product.GetProductInfo_manager(product.ID)).InStock.ToString();
            inputCategory.Text=product.Category.ToString();
            // inputID.IsEnabled = false; // can't change id
        }

        else //for add
        {
            completeAdd.Visibility = Visibility.Visible;
            completeUpdate.Visibility = Visibility.Collapsed; // updae button not available
            completeAdd.IsEnabled = false; // can't click ADD
           
        }

    }

    private void CompleteAdd_MouseDoubleClick(object sender, RoutedEventArgs e) // COMPLETE ADD BUTTON
    {
        // makes product
        BO.Product product = new BO.Product() { ID = id, Category = category, InStock = inStock, Name = nameOfProd, Price = price };
        try
        {
            bl!.Product.AddProdut(product); // add the product to the list
            MessageBox.Show("Product added succefully"); // Notice to the user
            this.Close();
        }

        // catch all exception from add product function
        catch (BO.DoesntExistExeption ex)
        {
            string innerEx = "";
            if(ex.InnerException!=null)
                innerEx = ": "+ex.InnerException.Message;
            MessageBox.Show("unsucessfull addition:"+ex.Message+innerEx); // print exception 
            
        }
        catch (BO.InvalidInputExeption ex)
        {
            string innerEx = "";
            if (ex.InnerException != null)
                innerEx = ": " + ex.InnerException.Message;
            MessageBox.Show("unsucessfull addition:" + ex.Message + innerEx); // print exception


        }
        catch (BO.ContradictoryDataExeption ex)
        {
            string innerEx = "";
            if (ex.InnerException != null)
                innerEx = ": " + ex.InnerException.Message;
            MessageBox.Show("unsucessfull addition:" + ex.Message + innerEx); // print exception
        }
        catch (BO.AlreadyExistExeption ex)
        {
            string innerEx = "";
            if (ex.InnerException != null)
                innerEx = ": " + ex.InnerException.Message;
            MessageBox.Show("unsucessfull addition:" + ex.Message + innerEx); // print exception
        }
    }

    private void TextBoxID(object sender, TextChangedEventArgs e) // text box for id input
    {
        if(int.TryParse(inputID.Text, out id)&&(completeUpdate.Visibility==Visibility.Collapsed)) // if it is on an 'ADD' mode and if id is digits
        {
            if((id>0) && (id.ToString().Length == 6)) // valid id
            {
                if ((id != int.MinValue) && (price != int.MinValue) && (inStock != int.MinValue) && (nameOfProd != null) && (category != null)) // if user put input in all of this
                {
                    completeAdd.IsEnabled = true; // can click ADD
                }
            }
            else
            {
                //as if no id was inputted
                //id = -1;
                id = int.MinValue;
            }
        }
    }

    private void inputCategoryFunc(object sender, SelectionChangedEventArgs e) // combo box to choose category
    {
        category = (BO.Category)inputCategory.SelectedItem; // get the input
        if (completeUpdate.Visibility == Visibility.Collapsed) // if it is on an ADD mode
        {
            if ((id != int.MinValue) && (price != int.MinValue) && (inStock != int.MinValue) && (nameOfProd != null) && (category != null)) // if the user put input in those text box
            {
                completeAdd.IsEnabled = true; //can click ADD
            }
        }
    }


    private void inputNameFunc(object sender, TextChangedEventArgs e) // text box for name product
    {
        nameOfProd =inputName.Text; // get the name
        if (completeUpdate.Visibility == Visibility.Collapsed) // if it is on an ADD mode
        {
            if ((id != int.MinValue) && (price != int.MinValue) && (inStock != int.MinValue) && (nameOfProd != null) && (category != null))  // if the user put values
            {
                completeAdd.IsEnabled = true; // can click ADD
            }
        }
    }

    private void completeUpdate_Click(object sender, RoutedEventArgs e) // COMPLETE UPDATE BUTTON
    {
        if (price == int.MinValue) price = prod!.Price; // the id with no change
        if (nameOfProd == null) nameOfProd = prod!.Name; // te name with no change
        if(inStock== int.MinValue) inStock= bl!.Product.GetProductInfo_manager(prod!.ID).InStock; // in stock with no change
        if (category == null) category = prod!.Category; // category with no change

        // makes the product
        BO.Product product = new BO.Product() { ID = id, Category = category, InStock = inStock, Name = nameOfProd, Price = price };

        try
        {
            bl!.Product.UpdateProduct(product); // upadte
            MessageBox.Show("Product updated succefully"); // notice the user
            this.Close();
        }

        // catch update products exception
        catch (BO.DoesntExistExeption ex) 
        {
            string innerEx = "";
            if (ex.InnerException != null)
                innerEx = ": " + ex.InnerException.Message;
            MessageBox.Show("unsucessfull update:" + ex.Message + innerEx); // print the exception

        }
        catch (BO.InvalidInputExeption ex)
        {
            string innerEx = "";
            if (ex.InnerException != null)
                innerEx = ": " + ex.InnerException.Message;
            MessageBox.Show("unsucessfull update:" + ex.Message + innerEx); // print the exception

        }
        catch (BO.ContradictoryDataExeption ex)
        {
            string innerEx = "";
            if (ex.InnerException != null)
                innerEx = ": " + ex.InnerException.Message;
            MessageBox.Show("unsucessfull update:" + ex.Message + innerEx); // print the exception

        }



    }

    private void inputInStockFunc(object sender, TextChangedEventArgs e) // text box in stock input
    {
       
        if (int.TryParse(inputInStock.Text, out inStock)&&(completeUpdate.Visibility == Visibility.Collapsed)) // if it is on an ADD mode and in stock input is digits
        {
            if ((id != int.MinValue) && (price != int.MinValue) && (inStock != int.MinValue) && (nameOfProd != null) && (category != null)) // if the user filled all of those text boxes
            {
                completeAdd.IsEnabled = true; // can click ADD
            }
        }
    }

    private void inputPriceFunc(object sender, TextChangedEventArgs e) // text box for price input
    {
        
        if (double.TryParse(inputPrice.Text, out price)&&(completeUpdate.Visibility == Visibility.Collapsed)) // 'ADD' mode and price input is digits
        {
            if ((id != int.MinValue) && (price != int.MinValue) && (inStock != int.MinValue) && (nameOfProd != null) && (category != null)) // if the user filled all of those text boxes
            {
                completeAdd.IsEnabled = true; // can click ADD
            }
        }
    }
}

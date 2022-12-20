using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using BlApi;
using BO;
using DO;

namespace BlImplementation;

/// <summary>
/// A PRODUCT CLASS THAT IMPLEMENTS THE INTERFACE
/// </summary>
internal class Product : BlApi.IProduct
{
    DalApi.IDal Dal = DalApi.Factory.Get(); //DalList object Type

    /// <summary>
    /// GET LIST OF PRODUCT
    /// </summary>
    /// <returns>list of products</returns>
    /// <exception cref="BO.DoesntExistExeption"></exception>
    public IEnumerable<BO.ProductForList> GetListProduct()//בקשת רשימת מוצרים
    {
        List<BO.ProductForList> productForLists = new List<BO.ProductForList>();
        try
        {
            //gets a list of DO.products from dl and turns it into list of BO.products
            IEnumerable<DO.Product?> productList = Dal.Product.GetAll();
            foreach (DO.Product? var in productList)//for every item in productList add its content to ProductForList
            {
                BO.ProductForList productForListsTemp = new BO.ProductForList { ID = (int)var?.ID!, Name = var?.Name, Category = (BO.Category)var?.Category!, Price = (double)var?.Price! };
                productForLists.Add(productForListsTemp);
            }
            return productForLists;
        }
        catch (DO.DoesntExistExeption e)//catch exception for getall
        {
            throw new BO.DoesntExistExeption("Couldn't get list of products", e);
        }
    }
    /// <summary>
    /// GET LIST PRODUCT WITH FILTER
    /// </summary>
    /// <param name="filter"></param>
    /// <returns>list of filtered products</returns>
    /// <exception cref="BO.DoesntExistExeption"></exception>
    public IEnumerable<BO.ProductForList> GetListProduct(Func<ProductForList?, bool>? filter)//בקשת רשימת מוצרים
    {
        List<BO.ProductForList> productForLists = new List<BO.ProductForList>();
        try
        {
            //gets a list of DO.products from dl and turns it into list of BO.products
            List<DO.Product?> productList = (List<DO.Product?>)Dal.Product.GetAll();

            foreach (DO.Product? var in productList)//for every item in productList add its content to ProductForList
            {
                BO.ProductForList productForListsTemp = new BO.ProductForList { ID = (int)var?.ID!, Name = var?.Name, Category = (BO.Category)var?.Category!, Price = (double)var?.Price! };
                productForLists.Add(productForListsTemp);
            }

            IEnumerable<BO.ProductForList> productForListsFiltered = (from x in productForLists//filters list of all products
                                                                      where filter!(x)
                                                                      select x).ToList();

            return productForListsFiltered;
        }
        catch (DO.DoesntExistExeption e)//catch exception for getall
        {
            throw new BO.DoesntExistExeption("Couldn't get list of products", e);
        }
    }
    /// <summary>
    ///  Get Product Info for manager
    /// </summary>
    /// <param name="id"></param>
    /// <returns>Product with the sent id</returns>
    /// <exception cref="BO.DoesntExistExeption"></exception>
    public BO.Product GetProductInfo_manager(int id)//בקשת פרטי מוצר עבור מסך מנהל
    {
        try
        {
            DO.Product newProduct = Dal.Product.GetById(id);//get product from dal with the id
            return new BO.Product { ID = newProduct.ID, Name = newProduct.Name, Price = newProduct.Price, Category = (BO.Category)newProduct.Category!, InStock = newProduct.InStock };
        }
        catch (DO.DoesntExistExeption e)//catch exception for GetByID
        {
            throw new BO.DoesntExistExeption("Couldn't get product", e);
        }
    }
    /// <summary>
    /// Get Product in cart Info for client
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cart"></param>
    /// <returns>return product</returns>
    /// <exception cref="BO.DoesntExistExeption"></exception>
    /// <exception cref="BO.InvalidInputExeption"></exception>
    public BO.ProductItem GetProductInfo_client(int id, BO.Cart cart)//בקשת פרטי מוצר עבור מסך לקוח
    {
        if ((id >= 0) && (id.ToString().Length == 6))//valid id was sent
        {
            try
            {
                DO.Product product = Dal.Product.GetById(id);//get DO.Product with the sent id
                //transfers from DO.Product to BO.Product
                bool ifInStock = false;
                if (product.InStock > 0)
                    ifInStock = true;
                BO.ProductItem item = new BO.ProductItem { ID = product.ID, Name = product.Name, Price = product.Price, Category = (BO.Category)product.Category!, Amount = product.InStock, InStock = ifInStock };
                return item;
            }
            catch (DO.DoesntExistExeption e)//catch for GetByID
            {
                throw new BO.DoesntExistExeption("Couldn't get product", e);
            }
        }
        else//invalid id was sent
        {
            throw new BO.InvalidInputExeption("Invalid ID");
        }
    }
    /// <summary>
    /// Add the Product- FOR MANAGER
    /// </summary>
    /// <param name="product"></param>
    /// <exception cref="BO.InvalidInputExeption"></exception>
    /// <exception cref="BO.DoesntExistExeption"></exception>
    public void AddProdut(BO.Product product)
    {
        if ((product.ID < 0) || (product.ID.ToString().Length != 6) || (product.Name == "") || (product.Price < 0) || (product.InStock < 0))//if invalid information for a product was sent
            throw new BO.InvalidInputExeption("incorrect product information");
        try
        {
            Dal.Product.Add(new DO.Product { ID = product.ID, Name = product.Name, Category = (DO.Category)product.Category!, InStock = product.InStock, Price = product.Price });//adds the product to list of all products
        }

        // catch for Add
        catch (DO.DoesntExistExeption e) 
        {
            throw new BO.DoesntExistExeption("couldn't add product", e);
        }
        catch(DO.AlreadyExistExeption e)
        {
            throw new BO.AlreadyExistExeption("can't add,", e);
        }
    }
    /// <summary>
    /// Deletes the Product - FOR MANAGER
    /// </summary>
    /// <param name="id"></param>
    /// <exception cref="BO.DoesntExistExeption"></exception>
    /// <exception cref="BO.ContradictoryDataExeption"></exception>
    public void DeleteProduct(int id)
    {
        bool flag = true;
        try
        {
            IEnumerable<DO.Order?> orderList = Dal.Order.GetAll();
            foreach (DO.Order? var in orderList)
            {
                try
                {
                    IEnumerable<DO.OrderItem?> listOfOrder = Dal.OrderItem.GetListOrder((int)var?.ID!);
                    flag = flag & listOfOrder.Where(item => item?.PrudoctID == id).FirstOrDefault() == null; //found product in list of orders
                                                                                                            
                    //foreach (DO.OrderItem? item in listOfOrder)
                    //{
                    //    if (item?.PrudoctID == id) flag = false;
                    //}
                }
                catch (DO.DoesntExistExeption e)//catch for GetLiistOrder
                {
                    throw new BO.DoesntExistExeption("Couldn't get list of products", e);
                }
            }
            if (flag)//product isn't in orders
            {
                try
                {
                    Dal.Product.Delete(id);//deletes the product from list of products
                }
                catch (DO.DoesntExistExeption e)//catch for delele
                {
                    throw new BO.DoesntExistExeption("couldn't get product", e);
                }
            }
            else//product is in orders
            {
                throw new BO.ContradictoryDataExeption("Product is in orders");
            }
        }
        catch (DO.DoesntExistExeption e)//catch for GetAll
        {
            throw new BO.DoesntExistExeption("Couldn't get list of products", e);
        }
    }
    /// <summary>
    /// Updates the Product - FOR MANAGER
    /// </summary>
    /// <param name="product"></param>
    /// <exception cref="BO.InvalidInputExeption"></exception>
    /// <exception cref="BO.ContradictoryDataExeption"></exception>
    public void UpdateProduct(BO.Product product)
    {
        if ((product.ID < 0) || (((product.ID).ToString()).Length != 6) || (product.Name == "") || (product.Price < 0) || (product.InStock < 0))//if invalid information for a product was sent
            throw new BO.InvalidInputExeption("incorrect product information");
        try
        {
            Dal.Product.Update(new DO.Product { ID = product.ID, Name = product.Name, Category = (DO.Category)product.Category!, InStock = product.InStock, Price = product.Price });//updates information of product in the list of products
        }
        catch (DO.DoesntExistExeption e)//catch for Update
        {
            throw new BO.ContradictoryDataExeption("Couldn't update", e);
        }
    }
}

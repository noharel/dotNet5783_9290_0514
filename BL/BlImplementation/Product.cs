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

internal class Product: BlApi.IProduct
{
    DalApi.IDal Dal= DalApi.Factory.Get();
    public IEnumerable<BO.ProductForList> GetListProduct()//בקשת רשימת מוצרים
    {
        List <BO.ProductForList> productForLists = new List<BO.ProductForList>();
        try
        {
            IEnumerable<DO.Product?> productList = Dal.Product.GetAll();
            foreach (DO.Product? var in productList)
            {
                BO.ProductForList productForListsTemp = new BO.ProductForList { ID = (int)var?.ID!, Name = var?.Name, Category = (BO.Category)var?.Category!, Price = (double)var?.Price! };
                productForLists.Add(productForListsTemp);
            }
            return productForLists;
        }
        catch (DO.DoesntExistExeption e)
        {
            throw new BO.DoesntExistExeption("Couldn't get list of products", e);
        }
    }
    public IEnumerable<BO.ProductForList> GetListProduct(Func<ProductForList?, bool>? filter)//בקשת רשימת מוצרים
    {
        List<BO.ProductForList> productForLists = new List<BO.ProductForList>();
        try
        {
            List<DO.Product?> productList = (List<DO.Product?>)Dal.Product.GetAll();

            foreach (DO.Product? var in productList)
            {
                BO.ProductForList productForListsTemp = new BO.ProductForList { ID = (int)var?.ID!, Name = var?.Name, Category = (BO.Category)var?.Category!, Price = (double)var?.Price! };
                productForLists.Add(productForListsTemp);
            }

            IEnumerable<BO.ProductForList> productForListsFiltered = (from x in productForLists
                                                                      where filter!(x)
                                                                      select x).ToList();

            return productForListsFiltered;
        }
        catch (DO.DoesntExistExeption e)
        {
            throw new BO.DoesntExistExeption("Couldn't get list of products", e);
        }



    }
    public BO.Product GetProductInfo_manager(int id)//בקשת פרטי מוצר עבור מסך מנהל
    {
        try
        {
            DO.Product newProduct = Dal.Product.GetById(id);
            return new BO.Product { ID = newProduct.ID, Name = newProduct.Name, Price = newProduct.Price, Category = (BO.Category)newProduct.Category!, InStock = newProduct.InStock };
        }
        catch (DO.DoesntExistExeption e)
        {
            throw new BO.DoesntExistExeption("Couldn't get product", e);
        }
    }
    public BO.ProductItem GetProductInfo_client(int id, BO.Cart cart)//בקשת פרטי מוצר עבור מסך לקוח
    {
       if((id>=0)&& (((id).ToString()).Length == 6))
        {
            try
            {
                DO.Product product=Dal.Product.GetById(id);
                bool ifInStock = false;
                if (product.InStock > 0)
                    ifInStock = true;
                BO.ProductItem item=new BO.ProductItem{ ID=product.ID,Name=product.Name,Price=product.Price,Category=(BO.Category)product.Category!,Amount=product.InStock,InStock=ifInStock};
                return item;
            }
            catch(DO.DoesntExistExeption e)
            {
                throw new BO.DoesntExistExeption("Couldn't get product",e);
            }
        }
       else
        {
            throw new BO.InvalidInputExeption("Invalid ID");
        }
    }
    public void AddProdut(BO.Product product)
    {
        if ((product.ID < 0)||(((product.ID).ToString()).Length != 6) || (product.Name == "") || (product.Price < 0) || (product.InStock < 0))
            throw new BO.InvalidInputExeption("incorrect product information");
        try
        {
            Dal.Product.Add(new DO.Product { ID = product.ID, Name = product.Name, Category = (DO.Category)product.Category!, InStock = product.InStock, Price=product.Price  });
        }
        catch(DO.DoesntExistExeption e)
        {
            throw new BO.DoesntExistExeption("couldn't add product",e);
        }
    }
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
                    foreach (DO.OrderItem? item in listOfOrder)
                    {
                        if (item?.PrudoctID == id) flag = false;
                    }
                }
                catch (DO.DoesntExistExeption e)
                {
                    throw new BO.DoesntExistExeption("Couldn't get list of products", e);
                }
            }
            if (flag)
            {
                try
                {
                    Dal.Product.Delete(id);
                }
                catch (DO.DoesntExistExeption e)
                {
                    throw new BO.DoesntExistExeption("couldn't get product", e);
                }
            }
            else
            {
                throw new BO.ContradictoryDataExeption("Product is in orders");
            }
        }
        catch (DO.DoesntExistExeption e)
        {
            throw new BO.DoesntExistExeption("Couldn't get list of products", e);
        }
    }
    public void UpdateProduct(BO.Product product)
    {
        if ((product.ID < 0)||(((product.ID).ToString()).Length != 6)|| (product.Name == "") || (product.Price < 0) || (product.InStock < 0))
            throw new BO.InvalidInputExeption("incorrect product information");
        try
        {
            Dal.Product.Update(new DO.Product { ID = product.ID, Name = product.Name, Category = (DO.Category)product.Category!, InStock = product.InStock, Price = product.Price });
        }
        catch(DO.DoesntExistExeption e)
        {
            throw new BO.ContradictoryDataExeption("Couldn't update",e);
        }
    }
}

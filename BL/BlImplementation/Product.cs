using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using BlApi;
namespace BlImplementation;

internal class Product: BlApi.IProduct
{
    DalApi.IDal Dal= new DalList();
    public IEnumerable<BO.ProductForList> GetListProduct()//בקשת רשימת מוצרים
    {
        IEnumerable<BO.ProductForList> productForLists = new List<BO.ProductForList>();
        IEnumerable < DO.Product > productList = Dal.Product.GetAll();
        foreach (DO.Product var in productList)
        {
            BO.ProductForList productForListsTemp = new BO.ProductForList{ID = var.ID,Name=var.Name,Category= (Category)var.Category,Price=var.Price};
            productForLists.Append(productForListsTemp);
        }
        return productForLists;
    }
    public BO.Product GetProductInfo_manager(int id)//בקשת פרטי מוצר עבור מסך מנהל
    {
        DO.Product newProduct = Dal.Product.GetById(id);
        return new BO.Product { ID = newProduct.ID, Name = newProduct.Name, Price = newProduct.Price, Category = (Category)newProduct.Category, InStock = newProduct.InStock};
    }
    public BO.ProductItem GetProductInfo_client(int id, BO.Cart cart)//בקשת פרטי מוצר עבור מסך לקוח
    {
       if(id>0)
        {
            try
            {
                DO.Product product=Dal.Product.GetById(id);
                bool ifInStock = false;
                if (product.InStock > 0)
                    ifInStock = true;
                BO.ProductItem item=new BO.ProductItem{ ID=product.ID,Name=product.Name,Price=product.Price,Category=(BO.Category)product.Category,Amount=product.InStock,InStock=ifInStock};
                return item;
            }
            catch(Exception e)
            {
                throw e;
            }
        }
       else
        {
            throw new Exception("Invalid ID");
        }
    }
    public void AddProdut(BO.Product product)
    {
        if ((product.ID < 0) || (product.Name == "") || (product.Price < 0) || (product.InStock < 0))
            throw new Exception("incorrect product information");
        try
        {
            Dal.Product.Add(new DO.Product { ID = product.ID, Name = product.Name, Category = (DO.Category)product.Category, InStock = product.InStock });
        }
        catch(Exception e)
        {
            throw e;
        }
    }
    public void DeleteProduct(int id)
    {
        IEnumerable<DO.Order> orderList = Dal.Order.GetAll();
        foreach (DO.Order var in orderList)
        {
           
            if(Dal.OrderItem.GetListOrder(id).Count()==0)
            {
                try
                {
                    Dal.Product.Delete(id);
                }
                catch(Exception e)
                {
                    throw e;
                }
            }
            else
            {
                throw new Exception("Product is indexer orders");
            }
        }
    }
    public void UpdateProduct(BO.Product product)
    {
        if ((product.ID < 0) || (product.Name == "") || (product.Price < 0) || (product.InStock < 0))
            throw new Exception("incorrect product information");
        try
        {
            Dal.Product.Update(new DO.Product { ID = product.ID, Name = product.Name, Category = (DO.Category)product.Category, InStock = product.InStock });
        }
        catch(Exception e)
        {
            throw e;
        }
    }
}

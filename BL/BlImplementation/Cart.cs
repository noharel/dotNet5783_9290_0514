using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlApi;
using DalApi;
using DO;

namespace BlImplementation;

internal class Cart :BlApi.ICart
{
    IDal Dal = DalList();
    public BO.Cart AddProductToCart(BO.Cart cart, int id)
    {
        bool flag=false;
        BO.OrderItem orderItem=new BO.OrderItem();
        foreach(BO.OrderItem var in cart.Items )
        {
            if (var.ID == id)
            {
                flag = true;  //FOUND THE PRODUCT IN THE CART
                orderItem = var;
            }
        }
        if (flag)//THR PPRODUCT IS IN THE CART
        {
            if (Dal.Product.GetById(id).InStock > 0)
            {
                orderItem.Amount++;
                orderItem.Price += Dal.Product.GetById(id).Price;
                cart.TotalPrice += Dal.Product.GetById(id).Price;

            }
            else
            {
                throw new Exception("Not in stock");
            }
        }
        else //NOT FOUND
        {
            try
            {
                DO.Product product = Dal.Product.GetById(id);
                if (product.InStock > 0)
                {
                    Random rand = new Random();
                    BO.OrderItem newOrderItem=new BO.OrderItem() { ID =  rand.Next(1000,9999), Name = product.Name, ProductID = product.ID, Price = product.Price, Amount = 1, TotalPrice = product.Price };
                    cart.Items.Append(newOrderItem);
                    cart.TotalPrice+=product.Price;
                }
                else
                {
                    throw new Exception("product does not exsist");
                }

            }
            catch (Exception e)
            {
                throw e;
            }
        }
        return cart;

    }

    public BO.Cart UpdateAmountInCart(BO.Cart cart, int id, int amount)
    {
        IEnumerable<BO.OrderItem> ourItem = from var in cart.Items 
                               where var.ID == id 
                               select var;
        BO.OrderItem ourOrderItem = ourItem.FirstOrDefault();
        int amountItem = ourOrderItem.Amount;
        if (amountItem < amount)
        {
            AddProductToCart(cart, amountItem);
        }
        if(amountItem > amount)
        {
            cart.TotalPrice -= ourOrderItem.Price*(amountItem-amount);
            ourOrderItem.TotalPrice -= ourOrderItem.Price * (amountItem - amount);
            ourOrderItem.Amount=amount;
            
        }
        if(amountItem == 0)
        {
            cart.Items= from var in cart.Items
                        where var.ID != id
                        select var;
            cart.TotalPrice -= ourOrderItem.TotalPrice;
        }
        return cart;
    }

    public void MakingAnOrder(BO.Cart cart)
    {
        IEnumerable<DO.Product> productsLit = Dal.Product.GetAll();
        bool flag = true;

        foreach (BO.OrderItem var in cart.Items)
        {
            bool internalFlag = false;
            foreach (DO.Product prod in productsLit)
            {
                if (var.ProductID == prod.ID)
                {
                    internalFlag = true;
                    flag = flag && (prod.InStock > 0);
                }
            }
            flag = flag && internalFlag && var.Amount > 0;
        }
        flag = flag && cart.CustomerName!=null && cart.CustomerEmail!=null && cart.CustomerAddress!=null;
        if (flag)
        {
            Random rand = new Random();
            DO.Order newOrder = new DO.Order() { ID = rand.Next(1000, 9999), CustomerAddress = cart.CustomerAddress, CustomerEmail = cart.CustomerEmail, CustomerName = cart.CustomerName, OrderDate = DateTime.Now, IsDeleted = false };
            try
            {
                int newOrderID = Dal.Order.Add(newOrder);
                foreach (BO.OrderItem var in cart.Items)
                {
                    DO.OrderItem newOrderItem = new DO.OrderItem() { ID = var.ID, PrudoctID = var.ProductID, OrderID = newOrderID, Price = var.Price, Amount = var.Amount, IsDeleted = false};
                    try
                    {
                        Dal.OrderItem.Add(newOrderItem);
                        DO.Product newProduct = Dal.Product.GetById(var.ProductID);
                        DO.Product updateProduct = new DO.Product() { ID = newProduct.ID, Name = newProduct.Name, Price = newProduct.Price, Category = newProduct.Category, InStock = newProduct.InStock - newOrderItem.Amount, IsDeleted = false };
                        Dal.Product.Update(updateProduct);
                    }
                    catch(Exception e)
                    {
                        throw e;
                    }
                }
            }
            catch(Exception e)
            {
                throw e;
            }
           
        }
        else throw new Exception("invalid information");
    }
}

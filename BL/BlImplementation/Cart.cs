using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlApi;
using DalApi;


namespace BlImplementation;

internal class Cart : BlApi.ICart
{

    
    DalApi.IDal Dal = DalApi.Factory.Get();


    public BO.Cart AddProductToCart(BO.Cart cart, int id)
    {
        bool flag=false;
        BO.OrderItem orderItem=new BO.OrderItem();
        foreach(BO.OrderItem? var in cart.Items! )
        {
            if (var.ProductID == id)
            {
                flag = true;  //FOUND THE PRODUCT IN THE CART
                orderItem = var;
            }
        }
        if (flag)//THR PPRODUCT IS IN THE CART
        {
            try
            {
                if (Dal.Product.GetById(id).InStock > 0)
                {
                    orderItem.Amount++;
                    orderItem.Price = Dal.Product.GetById(id).Price;
                    orderItem.TotalPrice = orderItem.Price * orderItem.Amount;
                    cart.TotalPrice += Dal.Product.GetById(id).Price;

                }
                else
                {
                    throw new BO.ContradictoryDataExeption("Not in stock");
                }
            }
            catch(DO.DoesntExistExeption e)
            {
                throw new BO.DoesntExistExeption("couldn't find", e);
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
                    cart.Items.Add(newOrderItem);
                    cart.TotalPrice+=product.Price;
                }
                else
                {
                    throw new BO.DoesntExistExeption("product does not exsist");
                }

            }
            catch (DO.DoesntExistExeption e)
            {
                throw new BO.DoesntExistExeption("couldn't find " , e);
            }
        }
        return cart;

    }

    public BO.Cart UpdateAmountInCart(BO.Cart cart, int id, int amount)
    {
        List<BO.OrderItem> ourItem = new List<BO.OrderItem>(from var in cart.Items
                                                            where var.ProductID == id
                                                            select var);
        BO.OrderItem ourOrderItem = ourItem!.FirstOrDefault()!;
        int amountItem = ourOrderItem!.Amount ;
        if (amountItem < amount)
        {
            for (int i = amountItem; i < amount; i++)
            {
                try
                {
                    AddProductToCart(cart, id);
                }
                catch (BO.DoesntExistExeption e)
                {
                    throw new BO.DoesntExistExeption("couldn't update", e);
                }
                catch (BO.ContradictoryDataExeption e)
                {
                    throw new BO.ContradictoryDataExeption("couldn't update", e);
                }
            }
        }
        if(amountItem > amount)
        {
            cart.TotalPrice -= ourOrderItem.Price*(amountItem-amount);
            ourOrderItem.TotalPrice -= ourOrderItem.Price * (amountItem - amount);
            ourOrderItem.Amount=amount;
            
        }
        if(amountItem == 0)
        {
            cart.Items= new List<BO.OrderItem>(from var in cart.Items
                        where var.ID != id
                        select var);
            cart.TotalPrice -= ourOrderItem.TotalPrice;
        }
        return cart;
    }

    public void MakingAnOrder(BO.Cart cart)
    {
        try
        {
            IEnumerable<DO.Product?> productsLit = Dal.Product.GetAll();
            bool flag = true;

            foreach (BO.OrderItem var in cart.Items!)
            {
                bool internalFlag = false;
                foreach (DO.Product? prod in productsLit)
                {
                    if (var.ProductID == prod?.ID)
                    {
                        internalFlag = true;
                        flag = flag && (prod?.InStock > 0);
                    }
                }
                flag = flag && internalFlag && var.Amount > 0;
            }
            flag = flag && cart.CustomerName != null && cart.CustomerEmail != null && cart.CustomerAddress != null;
            if (flag)
            {
                Random rand = new Random();
                try
                {
                    //figure out how to make sure the random id is not the same as configp
                    int temp;
                    List<DO.Order> listOrder;
                    do//makes sure that the id that we crate for order is not already an order id
                    {
                        temp = rand.Next(1000, 9999);
                        listOrder = new List<DO.Order>(from DO.Order var in Dal.Order.GetAll()
                                                       where var.ID == temp
                                                       select var);

                    } while (listOrder.Count > 0);
                    DO.Order newOrder = new DO.Order() { ID = temp, CustomerAddress = cart.CustomerAddress, CustomerEmail = cart.CustomerEmail, CustomerName = cart.CustomerName, OrderDate = DateTime.Now, IsDeleted = false };
                    try
                    {
                        int newOrderID = Dal.Order.Add(newOrder);
                        foreach (BO.OrderItem var in cart.Items)
                        {
                            DO.OrderItem newOrderItem = new DO.OrderItem() { ID = var.ID, PrudoctID = var.ProductID, OrderID = newOrderID, Price = var.Price, Amount = var.Amount, IsDeleted = false };
                            try
                            {
                                Dal.OrderItem.Add(newOrderItem);
                                DO.Product newProduct = Dal.Product.GetById(var.ProductID);
                                DO.Product updateProduct = new DO.Product() { ID = newProduct.ID, Name = newProduct.Name, Price = newProduct.Price, Category = newProduct.Category, InStock = newProduct.InStock - newOrderItem.Amount, IsDeleted = false };
                                Dal.Product.Update(updateProduct);
                            }
                            catch (DO.DoesntExistExeption e)
                            {
                                throw new BO.DoesntExistExeption("couldn't find", e);
                            }
                        }
                    }
                    catch (DO.DoesntExistExeption e)//umsuccesful add 
                    {
                        throw new BO.DoesntExistExeption("couldn't find", e);
                    }
                }
                catch (BO.DoesntExistExeption e)
                {
                    throw new BO.DoesntExistExeption("couldn't find", e);
                }

            }
            else throw new BO.InvalidInputExeption("invalid information");
        }
        catch(DO.DoesntExistExeption e)
        {
            throw new BO.DoesntExistExeption("couldn't get prducucts in order list", e);
        }
    }
}

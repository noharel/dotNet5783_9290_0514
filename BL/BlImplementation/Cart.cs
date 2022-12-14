using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlApi;
using DalApi;


namespace BlImplementation;

/// <summary>
/// 
/// A CART CLASS THAT IMPLEMENTS THE INTERFACE
/// </summary>
internal class Cart : BlApi.ICart
{

    DalApi.IDal Dal = DalApi.Factory.Get(); //DalList object Type

    /// <summary>
    /// ADD A PRODUCT TO CART
    /// </summary>
    /// <param name="cart"></param>
    /// <param name="id"></param>
    /// <returns></returns>
    /// <exception cref="BO.ContradictoryDataExeption"></exception>
    /// <exception cref="BO.DoesntExistExeption"></exception>
    public BO.Cart AddProductToCart(BO.Cart cart, int id)
    {
        bool flag=false; // Flag for finding the id
        BO.OrderItem orderItem=new BO.OrderItem();
        foreach(BO.OrderItem? var in cart.Items!)  // Go through the items in the cart
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
                if (Dal.Product.GetById(id).InStock > 0) // The product is in stock
                {
                    // Update amount, price, total price of item and total price of cart
                    orderItem.Amount++; 
                    orderItem.Price = Dal.Product.GetById(id).Price;
                    orderItem.TotalPrice = orderItem.Price * orderItem.Amount;
                    cart.TotalPrice += Dal.Product.GetById(id).Price;
                }
                else // The product is not in stock
                {
                    throw new BO.ContradictoryDataExeption("Not in stock");
                }
            }
            catch(DO.DoesntExistExeption e) // Catch exception from GetBYId function
            {
                throw new BO.DoesntExistExeption("couldn't find", e);
            }
        }
        else //NOT FOUND
        {
            try
            {
                DO.Product product = Dal.Product.GetById(id);  // Get the product
                if (product.InStock > 0) // The product is in stock
                {
                    ///makes sure that the id that we crate for product is not already a product id
                    int temp;
                    List<DO.Product> listProduct;
                    Random rand = new Random(); // for product id
                    do
                    {
                        temp = rand.Next(1000, 9999); // raffels 
                        listProduct = new List<DO.Product>(from DO.Product var in Dal.Product.GetAll()
                                                           where var.ID == temp
                                                           select var); // get all the products with the same id

                    } while (listProduct.Count > 0); //stops when it finds an id which is not already used

                    // makes the product
                    BO.OrderItem newOrderItem=new BO.OrderItem() { ID =  rand.Next(1000,9999), Name = product.Name, ProductID = product.ID, Price = product.Price, Amount = 1, TotalPrice = product.Price };
                    cart.Items.Add(newOrderItem); // ADD
                    cart.TotalPrice+=product.Price;  //Update the total price of cart
                }
                else  // The product is not in stock
                {
                    throw new BO.DoesntExistExeption("product does not exsist");
                }

            }
            catch (DO.DoesntExistExeption e)  // catch GetById function exeption 
            {
                throw new BO.DoesntExistExeption("couldn't find " , e);
            }
        }
        return cart;  // Returns the updated cart
    }

    /// <summary>
    /// UPDATE AMOUNT OF A CERTAIN ITEM IN THE CART
    /// </summary>
    /// <param name="cart"></param>
    /// <param name="id"></param>
    /// <param name="amount"></param>
    /// <returns></returns>
    /// <exception cref="BO.DoesntExistExeption"></exception>
    /// <exception cref="BO.ContradictoryDataExeption"></exception>
    public BO.Cart UpdateAmountInCart(BO.Cart cart, int id, int amount)
    {
        List<BO.OrderItem> ourItem = new List<BO.OrderItem>(from var in cart.Items
                                                            where var.ProductID == id
                                                            select var); // get the product 
        BO.OrderItem ourOrderItem = ourItem!.FirstOrDefault()!;
        int amountItem = ourOrderItem!.Amount ;
        if (amountItem < amount) // if the new  expected amount is MORE than now
        {
            for (int i = amountItem; i < amount; i++) // Fills the gap
            {
                try
                {
                    AddProductToCart(cart, id);  // ADD
                }

                // Catch AddProductToCart function exception
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
        if(amountItem > amount) // if the new  expected amount is LESS than now
        {
            // Update the total price of cart and item, and the amount
            cart.TotalPrice -= ourOrderItem.Price*(amountItem-amount);
            ourOrderItem.TotalPrice -= ourOrderItem.Price * (amountItem - amount);
            ourOrderItem.Amount=amount;
            
        }
        if(amountItem == 0) // If mount of the product in the cart is 0
        {
            cart.Items= new List<BO.OrderItem>(from var in cart.Items
                        where var.ID != id
                        select var); // Get all the product which are not the given product
            cart.TotalPrice -= ourOrderItem.TotalPrice;  // Update the total price of cart
        }
        return cart; // Returns the updated cart
    }

    /// <summary>
    /// MAKING AN ORDER - UPDATE THE ORDERS LIST
    /// </summary>
    /// <param name="cart"></param>
    /// <exception cref="BO.DoesntExistExeption"></exception>
    /// <exception cref="BO.InvalidInputExeption"></exception>
    public void MakingAnOrder(BO.Cart cart)
    {
        try
        {
            // check valid cart values
            IEnumerable<DO.Product?> productsList = Dal.Product.GetAll();  // get all products
            bool flag = true;

            foreach (BO.OrderItem var in cart.Items!)  // go throwgh items in the cart
            {
                bool internalFlag = false;
                foreach (DO.Product? prod in productsList) //go throwgh product all te products
                {
                    if (var.ProductID == prod?.ID) // fonid the product
                    {
                        // update flag
                        internalFlag = true; 
                        flag = flag && (prod?.InStock > 0);  // product is in stock
                    }
                }
                flag = flag && internalFlag && var.Amount > 0; // product is in the cart and amount>0
            }
            flag = flag && cart.CustomerName != null && cart.CustomerEmail != null && cart.CustomerAddress != null; //strings are not null

            if (flag) //valid values
            {
                Random rand = new Random();
                try
                {
                    //makes sure that the id that we crate for order is not already an order id
                    int temp;
                    List<DO.Order> listOrder;
                    do
                    {
                        temp = rand.Next(1000, 9999); // raffels
                        listOrder = new List<DO.Order>(from DO.Order var in Dal.Order.GetAll()
                                                       where var.ID == temp
                                                       select var); // get all the orders with the same id

                    } while (listOrder.Count > 0); //stops when it finds an id which is not already used

                    // make the order with dates null besides order date
                    DO.Order newOrder = new DO.Order() { ID = temp, CustomerAddress = cart.CustomerAddress, CustomerEmail = cart.CustomerEmail, CustomerName = cart.CustomerName, OrderDate = DateTime.Now, IsDeleted = false };
                    try
                    {
                        int newOrderID = Dal.Order.Add(newOrder);  //ADD order
                        foreach (BO.OrderItem var in cart.Items)  // go throwgh all the items in the cart
                        {
                            //copy item
                            DO.OrderItem newOrderItem = new DO.OrderItem() { ID = var.ID, PrudoctID = var.ProductID, OrderID = newOrderID, Price = var.Price, Amount = var.Amount, IsDeleted = false };
                            try
                            {
                                Dal.OrderItem.Add(newOrderItem); //ADD item
                                DO.Product newProduct = Dal.Product.GetById(var.ProductID); // get the product id for update his amount
                                DO.Product updateProduct = new DO.Product() { ID = newProduct.ID, Name = newProduct.Name, Price = newProduct.Price, Category = newProduct.Category, InStock = newProduct.InStock - newOrderItem.Amount, IsDeleted = false };
                                Dal.Product.Update(updateProduct); //update amount
                            }
                            catch (DO.DoesntExistExeption e) // catch exception from GetById/ADD/Update
                            {
                                throw new BO.DoesntExistExeption("couldn't find", e);
                            }
                        }
                    }
                    catch (DO.DoesntExistExeption e) // catch ADD function exception - unsuccessful addition
                    {
                        throw new BO.DoesntExistExeption("couldn't find", e);
                    }
                }
                catch (BO.DoesntExistExeption e) // catch GetAll function exception
                {
                    throw new BO.DoesntExistExeption("couldn't find", e);
                }

            }
            else throw new BO.InvalidInputExeption("invalid information"); // invalid cart values
        }
        catch(DO.DoesntExistExeption e)  // Catch GetAll function exception
        {
            throw new BO.DoesntExistExeption("couldn't get prducucts in order list", e);
        }
    }
}

using BlApi;

using Microsoft.VisualBasic;

namespace BLTest;

internal class BlTest
{
    static void funcProduct(IBl bl) // check products functions
    {

        string? ch;
        do
        {
            Console.WriteLine($@"
        choose one of the following commands:
        1-List of products
        2-Get product info for manager
        3-Add product
        4-Delete product
        5-Update product
        e-home");
            ch = Console.ReadLine(); // get choice
            int id,inStock,price;
            string? s;
            string? name;
            BO.Category c;
            bool flag = true;

            switch (ch)
            {
                case "1":  // get list of products
                    Console.WriteLine("The prouducts are:");
                    try
                    {
                        // go through all the products and print each one of them
                        bl.Product.GetListProduct().ToList().ForEach(delegate (BO.ProductForList? var) { Console.WriteLine(var); });

                        //    CHANGE TO LINQ
                        //foreach (BO.ProductForList? var in bl.Product.GetListProduct()) 
                        //{
                        //    Console.WriteLine(var); 
                        //}
                    }
                    catch(BO.DoesntExistExeption ex) // catch get list product exception
                    {
                        // print exceptions
                        Console.WriteLine(ex.Message);
                        if(ex.InnerException!=null)
                            Console.WriteLine(ex.Message);
                    }
                    break;

                case "2":  // Get product info for manager
                    Console.WriteLine("Enter product id:");
                    s = Console.ReadLine(); // get product id
                    try
                    {

                        if (int.TryParse(s, out id)) // if the input is digits
                        {
                            Console.WriteLine(bl.Product.GetProductInfo_manager(id)); // print information
                        }
                        else // the input is not a number
                        {
                            Console.WriteLine("Not a number");
                        }
                    }
                    
                    // catch get product information function exception
                    catch (BO.DoesntExistExeption e)
                    {
                        Console.WriteLine(e.Message);
                        if (e.InnerException != null)
                            Console.WriteLine(e.InnerException.Message);
                    }
                    catch (BO.InvalidInputExeption e)
                    {
                        Console.WriteLine(e.Message);
                        if (e.InnerException != null)
                            Console.WriteLine(e.InnerException.Message);
                    }
                    catch (BO.ContradictoryDataExeption e)
                    {
                        Console.WriteLine(e.Message);
                        if (e.InnerException != null)
                            Console.WriteLine(e.InnerException.Message);
                    }
                    break;

                case "3":  // add product

                    // get values from the user
                    Console.WriteLine("Enter product Id:");
                    s = Console.ReadLine();
                    flag = int.TryParse(s, out id)&&flag;
                    Console.WriteLine("Enter product name:");
                    name= Console.ReadLine();
                    Console.WriteLine("Enter product category:");
                    s = Console.ReadLine();
                    BO.Category.TryParse(s, out c);
                    Console.WriteLine("Enter amount in stock:");
                    s = Console.ReadLine();
                    flag = int.TryParse(s, out inStock)&&flag;
                    Console.WriteLine("Enter price:");
                    s = Console.ReadLine();
                    flag = int.TryParse(s, out price)&&flag;

                    if (flag) // valid values
                    {
                        // make the product
                        BO.Product prod = new BO.Product() { Category = c, ID = id, Name = name, InStock = inStock, Price = price };
                        try
                        {
                            bl.Product.AddProdut(prod); // add the product
                        }

                        // catch AddProduct function exception
                        catch (BO.DoesntExistExeption e)
                        {
                            Console.WriteLine(e.Message);
                            if (e.InnerException != null)
                                Console.WriteLine(e.InnerException.Message);
                        }
                        catch (BO.InvalidInputExeption e)
                        {
                            Console.WriteLine(e.Message);
                            if (e.InnerException != null)
                                Console.WriteLine(e.InnerException.Message);
                        }
                        catch (BO.ContradictoryDataExeption e)
                        {
                            Console.WriteLine(e.Message);
                            if (e.InnerException != null)
                                Console.WriteLine(e.InnerException.Message);
                        }
                        catch (BO.AlreadyExistExeption e)
                        {
                            Console.WriteLine(e.Message);
                            if (e.InnerException != null)
                                Console.WriteLine(e.InnerException.Message);
                        }


                    }
                    else // invalid values
                    {
                        Console.WriteLine("Not a valid input for product");
                    }
                    break;

                case "4": // Delete product

                    Console.WriteLine("Enter product Id to delete:");
                    s = Console.ReadLine(); // get id product

                    if (int.TryParse(s, out id)) // valid input
                    {
                        try
                        {
                            bl.Product.DeleteProduct(id); // delete the product
                        }

                        // catch DeleteProduct function exception and print them
                        catch (BO.DoesntExistExeption e)
                        {
                            Console.WriteLine(e.Message);
                            if (e.InnerException != null)
                                Console.WriteLine(e.InnerException.Message);
                        }
                        catch (BO.InvalidInputExeption e)
                        {
                            Console.WriteLine(e.Message);
                            if (e.InnerException != null)
                                Console.WriteLine(e.InnerException.Message);
                        }
                        catch (BO.ContradictoryDataExeption e)
                        {
                            Console.WriteLine(e.Message);
                            if (e.InnerException != null)
                                Console.WriteLine(e.InnerException.Message);
                        }
                    }
                    else // invalid input
                    {
                        Console.WriteLine("Not a number");
                    }
                    break;

                case "5": // Update product

                    // get input
                    flag = true;
                    Console.WriteLine("Enter product Id:");
                    s = Console.ReadLine();
                    flag=int.TryParse(s, out id)&&flag;
                    Console.WriteLine("Enter product name:");
                    name = Console.ReadLine();
                    Console.WriteLine("Enter product category:");
                    s = Console.ReadLine();
                    BO.Category.TryParse(s, out c);
                    Console.WriteLine("Enter amount in stock:");
                    s = Console.ReadLine();
                    flag=int.TryParse(s, out inStock)&&flag;
                    Console.WriteLine("Enter price:");
                    s = Console.ReadLine();
                    flag=int.TryParse(s, out price)&&flag;

                    if (flag) // valid input
                    {
                        // makes the product
                        BO.Product prodforUp = new BO.Product() { Category = c, ID = id, Name = name, InStock = inStock, Price = price };
                        try
                        { 
                            bl.Product.UpdateProduct(prodforUp);  // update
                        }

                        // catch Updateproduct function exception and print it
                        catch (BO.DoesntExistExeption e)
                        {
                            Console.WriteLine(e.Message);
                            if (e.InnerException != null)
                                Console.WriteLine(e.InnerException.Message);
                        }
                        catch (BO.InvalidInputExeption e)
                        {
                            Console.WriteLine(e.Message);
                            if (e.InnerException != null)
                                Console.WriteLine(e.InnerException.Message);
                        }
                        catch (BO.ContradictoryDataExeption e)
                        {
                            Console.WriteLine(e.Message);
                            if (e.InnerException != null)
                                Console.WriteLine(e.InnerException.Message);
                        }

                    }
                    else // invalid input
                    {
                        Console.WriteLine("Not a number");
                    }
                    break;

                case "e":  //to exit
                    break;

                default: 
                    Console.WriteLine("ERROR");
                    break;
            }
        } while (ch != "e");
    }
    static void funcOrder(IBl bl) // check orders functions
    {
        string? ch;
        bool flag;
        do
        {
            Console.WriteLine($@"
        choose one of the following commands:
        1-List of orders
        2-Get order info 
        3-Update ship date
        4-Update delivery date
        5-Tracking order
        6-Update by manager
        e-home");
            ch = Console.ReadLine(); // get choice

            int id;
            string? s;
            switch (ch)

            {
                case "1":  // get list of orders
                    Console.WriteLine("The orders are:");
                    try
                    {
                        // go through all the orders and print each one, there is no linq for this
                        bl.Order.GetOrders().ToList().ForEach(delegate (BO.OrderForList? var) { Console.WriteLine(var); });
                    }
                    
                    // catch GetOrders function exception and print it
                    catch (BO.DoesntExistExeption e)
                    {
                        Console.WriteLine(e.Message);
                        if (e.InnerException != null)
                            Console.WriteLine(e.InnerException.Message);
                    }
                    catch (BO.InvalidInputExeption e)
                    {
                        Console.WriteLine(e.Message);
                        if (e.InnerException != null)
                            Console.WriteLine(e.InnerException.Message);
                    }
                    catch (BO.ContradictoryDataExeption e)
                    {
                        Console.WriteLine(e.Message);
                        if (e.InnerException != null)
                            Console.WriteLine(e.InnerException.Message);
                    }
                    break;

                case "2":  // Get order information

                    Console.WriteLine("Enter order id:");
                    s = Console.ReadLine(); // get prodcut id

                    if (int.TryParse(s, out id)) // input is intiger - valid
                    {
                        try
                        {
                            Console.WriteLine(bl.Order.OrderInfo(id)); // print order info
                        } 

                        // catch OrderInfo function exception and print it
                        catch (BO.DoesntExistExeption e)
                        {
                            Console.WriteLine(e.Message);
                            if (e.InnerException != null)
                                Console.WriteLine(e.InnerException.Message);
                        }
                        catch (BO.InvalidInputExeption e)
                        {
                            Console.WriteLine(e.Message);
                            if (e.InnerException != null)
                                Console.WriteLine(e.InnerException.Message);
                        }
                        catch (BO.ContradictoryDataExeption e)
                        {
                            Console.WriteLine(e.Message);
                            if (e.InnerException != null)
                                Console.WriteLine(e.InnerException.Message);
                        }
                    }
                    else // invalid input
                    {
                        Console.WriteLine("Invalid id input,not a number");
                    }
                    break;

                case "3":  // Update ship date

                    Console.WriteLine("Enter order Id:");
                    s = Console.ReadLine(); // get order id

                    if (int.TryParse(s, out id)) // valid input
                    {
                        try
                        {
                            Console.WriteLine(bl.Order.UpdateShip(id)); // update ship date
                        }

                        // catch UpdateShip function exception and print it
                        catch (BO.DoesntExistExeption e)
                        {
                            Console.WriteLine(e.Message);
                            if (e.InnerException != null)
                                Console.WriteLine(e.InnerException.Message);
                        }
                        catch (BO.InvalidInputExeption e)
                        {
                            Console.WriteLine(e.Message);
                            if (e.InnerException != null)
                                Console.WriteLine(e.InnerException.Message);
                        }
                        catch (BO.ContradictoryDataExeption e)
                        {
                            Console.WriteLine(e.Message);
                            if (e.InnerException != null)
                                Console.WriteLine(e.InnerException.Message);
                        }
                    }
                    else // invalid input
                    {
                        Console.WriteLine("Invalid id input,not a number");

                    }
                    break;

                case "4": // Update delivery date

                    Console.WriteLine("Enter order Id:");
                    s = Console.ReadLine(); // get order id

                    if(int.TryParse(s, out id)) // valid input id
                    {
                        try
                        { 
                            Console.WriteLine(bl.Order.UpdateDelivery(id)); // update delivery
                        }

                        // catch UpdateDelivery function exception and print it
                        catch (BO.DoesntExistExeption e)
                        {
                            Console.WriteLine(e.Message);
                            if (e.InnerException != null)
                                Console.WriteLine(e.InnerException.Message);
                        }
                        catch (BO.ContradictoryDataExeption e)
                        {
                            Console.WriteLine(e.Message);
                            if (e.InnerException != null)
                                Console.WriteLine(e.InnerException.Message);
                        }
                    }
                    else //invalid input id
                    {
                        Console.WriteLine("Invalid id input,not a number");

                    }
                    break;

                case "5": // Tracking order

                    Console.WriteLine("Enter order Id:");
                    s = Console.ReadLine(); // get order id input

                    if (int.TryParse(s, out id)) // valid input id
                    {
                        try
                        {
                            Console.WriteLine(bl.Order.TrackingOrder(id)); // print the tracking order
                        }

                        // catch TrackingorderException and print it
                        catch (BO.DoesntExistExeption e)
                        {
                            Console.WriteLine(e.Message);
                            if (e.InnerException != null)
                                Console.WriteLine(e.InnerException.Message);
                        }
                        catch (BO.InvalidInputExeption e)
                        {
                            Console.WriteLine(e.Message);
                            if (e.InnerException != null)
                                Console.WriteLine(e.InnerException.Message);
                        }
                        catch (BO.ContradictoryDataExeption e)
                        {
                            Console.WriteLine(e.Message);
                            if (e.InnerException != null)
                                Console.WriteLine(e.InnerException.Message);
                        }
                    }
                    else // ivalid input id
                    {
                        Console.WriteLine("Invalid id input,not a number");
                    }

                    break;

                case "6": // Update by manager

                    // get input
                    int orderID, orderItemId, amount;
                    flag = true;
                    Console.WriteLine("Enter order Id:");
                    s = Console.ReadLine();
                    flag=int.TryParse(s, out orderID)&&flag;
                    Console.WriteLine("Enter order item Id:");
                    s = Console.ReadLine();
                    flag=int.TryParse(s, out orderItemId)&&flag;
                    Console.WriteLine("Enter amount:");
                    s = Console.ReadLine();
                    flag=int.TryParse(s, out amount)&&flag;

                    if (flag) // vaid input
                    {
                        try
                        {
                            bl.Order.UpdateByManager(orderID, orderItemId, amount); // update
                        }

                        // catch UpdateByManager function exception and print it
                        catch (BO.DoesntExistExeption e)
                        {
                            Console.WriteLine(e.Message);
                            if (e.InnerException != null)
                                Console.WriteLine(e.InnerException.Message);
                        }
                        catch (BO.InvalidInputExeption e)
                        {
                            Console.WriteLine(e.Message);
                            if (e.InnerException != null)
                                Console.WriteLine(e.InnerException.Message);
                        }
                        catch (BO.ContradictoryDataExeption e)
                        {
                            Console.WriteLine(e.Message);
                            if (e.InnerException != null)
                                Console.WriteLine(e.InnerException.Message);
                        }
                    }
                    else // invalid input
                    {
                        Console.WriteLine("inalid order input");
                    }
                    break;

                case "e":  //to exit
                    break;

                default:
                    Console.WriteLine("ERROR"); 
                    break;

            }
        } while (ch != "e");
    }
    static void funcCart(IBl bl) // check cart functions
    { 
        // get all the values for cart to make on it action
        int id,amount;
        bool flag = true;
        string? s, name, customerAddress, customerEmail;
        Console.WriteLine("Enter information for cart:");
        Console.WriteLine("Enter customer address:");
        customerAddress = Console.ReadLine();
        Console.WriteLine("Enter customer email:"); //לבדוק אם צריך לבדוק סיומת @JCT
        customerEmail = Console.ReadLine();
        Console.WriteLine("Enter customer name:");
        name = Console.ReadLine();

        // makes the cart
        BO.Cart cart = new BO.Cart() { CustomerAddress = customerAddress, CustomerEmail = customerEmail, CustomerName = name, Items = new List<BO.OrderItem>(), TotalPrice = 0 };

 

        string? ch;
        do
        {
            Console.WriteLine($@"
        choose one of the following commands:
        1-Add product to cart
        2-Update amount in cart
        3-Make an order
        e-home");
            ch = Console.ReadLine(); // get choice

             switch (ch)
            {
                case "1": // Add product to cart

                    Console.WriteLine("Enter product Id:");
                    s = Console.ReadLine(); // get product id

                    if (int.TryParse(s, out id)) // valid input id
                    {
                        try
                        {
                            // get the new cart and print it
                            cart = bl.Cart.AddProductToCart(cart, id);
                            Console.WriteLine(cart);

                        }

                        // catch AddProductToCart function excwption and print it
                        catch (BO.DoesntExistExeption e)
                        {
                            Console.WriteLine(e.Message);
                            if (e.InnerException != null)
                                Console.WriteLine(e.InnerException.Message);
                        }
                        catch (BO.InvalidInputExeption e)
                        {
                            Console.WriteLine(e.Message);
                            if (e.InnerException != null)
                                Console.WriteLine(e.InnerException.Message);
                        }
                        catch (BO.ContradictoryDataExeption e)
                        {
                            Console.WriteLine(e.Message);
                            if (e.InnerException != null)
                                Console.WriteLine(e.InnerException.Message);
                        }
                    }
                    else // indalid input id
                    {
                        Console.WriteLine("invalid order id, not a number");
                    }
                    break;

                case "2": // Update amount in cart  
                    
                    // get input
                    Console.WriteLine("Enter product Id:");
                    s = Console.ReadLine();
                    flag=int.TryParse(s, out id)&&flag;
                    Console.WriteLine("Enter  amount:");
                    s = Console.ReadLine();
                    flag=int.TryParse(s, out amount)&&flag;

                    if (flag) // valid input
                    {
                        try
                        {
                            // get the new cart and print it
                            cart = bl.Cart.UpdateAmountInCart(cart, id, amount);
                            Console.WriteLine(cart);

                        }

                        // catch UpdateAmountInCart function exception and print it
                        catch (BO.DoesntExistExeption e)
                        {
                            Console.WriteLine(e.Message);
                            if (e.InnerException != null)
                                Console.WriteLine(e.InnerException.Message);
                        }
                        catch (BO.InvalidInputExeption e)
                        {
                            Console.WriteLine(e.Message);
                            if (e.InnerException != null)
                                Console.WriteLine(e.InnerException.Message);
                        }
                        catch (BO.ContradictoryDataExeption e)
                        {
                            Console.WriteLine(e.Message);
                            if (e.InnerException != null)
                                Console.WriteLine(e.InnerException.Message);
                        }
                    }
                    else // invalid input
                    {
                        Console.WriteLine("invalid input");
                    }
                    break;

                case "3": // Make an order  

                    try
                    {
                        // make an order and add it to list of orders
                        bl.Cart.MakingAnOrder(cart);
                    }

                    // catch UpdateAmountInCart function exception and print it
                    catch (BO.DoesntExistExeption e)
                    {
                        Console.WriteLine(e.Message);
                        if (e.InnerException != null)
                            Console.WriteLine(e.InnerException.Message);
                    }
                    catch (BO.InvalidInputExeption e)
                    {
                        Console.WriteLine(e.Message);
                        if (e.InnerException != null)
                            Console.WriteLine(e.InnerException.Message);
                    }
                    catch (BO.ContradictoryDataExeption e)
                    {
                        Console.WriteLine(e.Message);
                        if (e.InnerException != null)
                            Console.WriteLine(e.InnerException.Message);
                    }
                    catch(BO.AlreadyExistExeption e)
                    {
                        Console.WriteLine(e.Message);
                        if (e.InnerException != null)
                            Console.WriteLine(e.InnerException.Message);
                    }
                    break;
               
                case "e":  //to exit
                    break;

                default: 
                    Console.WriteLine("ERROR");
                    break;
            }
        } while (ch != "e");
    }

    static void Main(string[] args)
    {
        BlApi.IBl? bl = BlApi.Factory.Get(); // DalList object Type


        string? ch;
        do
        {
            Console.WriteLine($@"
        choose one of the following commands:
        1-for a product
        2-for an order
        3-for a cart
        e-exit"); 
            ch = Console.ReadLine(); // get choice
            switch (ch)
            {

                case "1":  //for product
                    funcProduct(bl);
                    break;
                case "2":  //for order
                    funcOrder(bl);
                    break;
                case "3":  //for cart
                    funcCart(bl);
                    break;
                case "e":  //to exit
                    Console.WriteLine("Have an amazing Day! Remember to always drive safe and that someone loves you :)");
                    break;

                default: Console.WriteLine("ERROR"); break;

            }
        } while (ch != "e"); // ch == e - exit



    }
}
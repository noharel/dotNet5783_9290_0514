using DalApi;
using System;
using DO;
using Dal;
using System.Runtime.CompilerServices;
using System.Diagnostics;
using System.Xml.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Linq.Expressions;

//linq done

namespace DalTest;
partial class DalTest
{
    static void product_func(DalProduct product) // operations on the product
    {
        string? ch;

        //print the options
        Console.WriteLine($@"     
        choose one of the following commands:
        a-Add
        b-get by ID
        c-get all
        d-update
        e-delete");
        ch = Console.ReadLine(); //get the choice from the user

        if (ch == "a")  // Add new product to the list
        {
            int ID, price, iS;
            string? s;
            string? name;
            Category category;
            Product p = new Product();  // new product
            Console.WriteLine("enter information for a product to add: ");
            Console.WriteLine("enter product ID: ");
            s=Console.ReadLine();
            int.TryParse(s, out ID);
            p.ID = ID;  // new ID
            Console.WriteLine("enter name: ");
            name = Console.ReadLine();
            p.Name = name;  // new name
            Console.WriteLine("enter price: ");
            s = Console.ReadLine();
            int.TryParse(s, out price);
            p.Price = price;  // new price
            Console.WriteLine("enter category: ");
            s = Console.ReadLine();
            Category.TryParse(s, out category);
            p.Category = (Category)category;  // new category
            Console.WriteLine("enter in stock: ");
            s = Console.ReadLine();
            int.TryParse(s, out iS);
            p.InStock = iS;  // new amount
            p.IsDeleted = false;
            try
            {
                product.Add(p);  // add the new product to the list
            }
            catch(NotImplementedException e)  // catch exeption
            {
                Console.WriteLine(e.Message);
            }
        }

        if (ch == "b") // GET BY ID
        {
            string? s;
            int ID;
            Console.WriteLine("enter product ID: ");
            s = Console.ReadLine();  // get the ID
            int.TryParse(s, out ID);

            try
            {
                Console.WriteLine(product.GetById(ID)); // print the product
            }
            catch (Exception e) // catch exeption
            {
                Console.WriteLine(e.Message);
            }
        }

        if (ch == "c") // print all the products
        {
            Console.WriteLine("The products are:");

            // goes through all the products and prints them
            product.GetAll().ToList().ForEach(delegate (Product? product_from_list) { Console.WriteLine(product_from_list); });

           
        }

        if (ch == "d") // update
        {

            string? s, name;
            int ID, iS, price;
            Product p = new Product();
            Category category;
            Console.WriteLine("enter product ID: ");//prints product before update
            s = Console.ReadLine();//prints product before update
            int.TryParse(s, out ID);//prints product before update

            try
            {
                Console.WriteLine(product.GetById(ID));//prints product before update
            }
            catch (Exception e) //catch exeption
            {
                Console.WriteLine(e);
            }

            p.ID=ID;     //gets the ID of the product we want to update
            Console.WriteLine("enter new name: ");
            name = Console.ReadLine();  //get a new name
            if (name == "")  //if there is no need to change 
            {
                try
                {
                    p.Name = product.GetById(ID).Name; //get the name it allready has
                }
                catch (Exception e) //catch exception
                {
                    Console.WriteLine(e);
                }
            }
            else
                p.Name = name; //update name

            Console.WriteLine("enter new price: ");
            s = Console.ReadLine(); //get the new price
            if (s == "") //if there is no need to change
            {
                try
                {
                    p.Price = product.GetById(ID).Price; //get the price it allready has
                }
                catch (Exception e) //catch exception
                {
                    Console.WriteLine(e);
                }
            }
            else
            {
                int.TryParse(s, out price);
                p.Price = price;  //update the price
            }

            Console.WriteLine("enter new category: ");
            s = Console.ReadLine();
            if (s == "") //there is no need to change
            {
                try
                {
                    p.Category = product.GetById(ID).Category; //get the category with no change
                }
                catch (Exception e)  //catch exception
                {
                    Console.WriteLine(e);
                }
            }
            else
            {
                Category.TryParse(s, out category);
                p.Category = (Category)category;  //update category

            }

            Console.WriteLine("enter new in stock: ");
            s = Console.ReadLine();  //get the new amount
            if (s == "")
            {
                try
                {
                    p.InStock = product.GetById(ID).InStock;  //no change
                }
                catch (Exception e) //catch exception
                {
                    Console.WriteLine(e);
                }
            }
            else
            {
                int.TryParse(s, out iS);
                p.InStock = iS; //update the amount
            }
            try
            {
                product.Update(p); //update the product
            }
            catch(NotImplementedException e)  //catch exception
            {
                Console.WriteLine(e.Message);
            }
        }

        if (ch == "e") // delete product
        {
            int ID;
            string? s;
            Console.WriteLine("enter id of product to delete");
            s = Console.ReadLine(); //get the ID of the product to delete
            int.TryParse(s, out ID);
            try
            {
                product.Delete(ID); //delete the product
            }
            catch (DoesntExistExeption e)  //catch eception
            {
                Console.WriteLine(e.Message); //print the error
            }
        }
    }
    static void order_func(DalOrder order)  // operations on orders
    {
        string? ch;

        //print the options
        Console.WriteLine($@"
        choose one of the following commands:
        a-Add
        b-get by ID
        c-get all
        d-update
        e-delete");
        ch = Console.ReadLine(); //get the choice
        Random s_rand = new Random();

        if (ch == "a")  //Add a new order
        {
            int ID;
            string? s, name;
            DateTime dt;
            Order o = new Order();  //build a new order
            Console.WriteLine("enter information for an order to add: ");
            Console.WriteLine("enter order ID: ");
            s = Console.ReadLine(); //get the ID
            int.TryParse(s, out ID);
            o.ID = ID;
            Console.WriteLine("enter customer name: ");
            name = Console.ReadLine();  //get the name
            o.CustomerName = name; 
            Console.WriteLine("enter customer email: ");
            s = Console.ReadLine(); //get the customer email
            o.CustomerEmail = s;
            Console.WriteLine("enter customer address: ");
            s = Console.ReadLine(); //get the customer addredd
            o.CustomerAddress = s;
            Console.WriteLine("enter order date: ");
            s = Console.ReadLine(); //get the order date
            DateTime.TryParse(s, out dt);
            o.OrderDate = dt;
            Console.WriteLine("enter ship date: ");
            s = Console.ReadLine(); //get the ship date
            DateTime.TryParse(s, out dt);
            if (dt == DateTime.MinValue)
            {
                o.ShipDate = null;
            }
            else
            {
                o.ShipDate = dt;
            }
            Console.WriteLine("enter delivery date: ");
            s = Console.ReadLine(); //get the delivery date
            DateTime.TryParse(s, out dt);
            if (dt == DateTime.MinValue)
            {
                o.DeliveryrDate = null;
            }
            else
            {
                o.DeliveryrDate = dt;
            }
            o.IsDeleted = false;

            try
            {
                order.Add(o); //add the new order
            }
            catch (DoesntExistExeption e)  //catch eception
            {
                Console.WriteLine(e.Message); //print the error
            }
        }

        if (ch == "b") //GET BY ID
        {
            string? s;
            int ID;
            Console.WriteLine("enter ID: ");
            s = Console.ReadLine();  //get the ID
            int.TryParse(s, out ID);
            try
            {
                Console.WriteLine(order.GetById(ID));  //print the order
            }
            catch (DoesntExistExeption e)  //catch eception
            {
                Console.WriteLine(e.Message); //print the error
            }
        }

        if (ch == "c") //print all orders
        {
            Console.WriteLine("The orders are:");

            //goes through all the orders and print them
            order.GetAll().ToList().ForEach(delegate (Order? order_from_list) { Console.WriteLine(order_from_list); });

                //    CHANGE TO LINQ
            //foreach (Order? order_from_list in order.GetAll()) //goes through all the orders,there is no linq for this
            //{
            //    Console.WriteLine(order_from_list); //print it
            //}
        }

        if (ch == "d") //update
        {
            string ?s, name;
            int ID;
            Order o = new Order();
            DateTime dt;
            Console.WriteLine("enter ID: ");//prints product before update
            s = Console.ReadLine();//prints product before update
            int.TryParse(s, out ID);//prints product before update
            try
            {
                Console.WriteLine(order.GetById(ID));//prints product before update
            }
            catch (Exception e) //catch exeption
            {
                Console.WriteLine(e);
            }
            o.ID = ID;
            Console.WriteLine("enter a new customer name: ");
            name = Console.ReadLine();
            if (name == "") //if there is no need to change
            {
                try
                {
                    o.CustomerName = order.GetById(ID).CustomerName; //no change
                }
                catch (Exception e) //catch exception
                {
                    Console.WriteLine(e);
                }
            }
            else
                o.CustomerName = name; //update name

            Console.WriteLine("enter a new customer email: ");
            s = Console.ReadLine();
            if (s == "") //no need to change
            {
                try
                {
                    o.CustomerEmail = order.GetById(ID).CustomerEmail; //no change
                }
                catch (DoesntExistExeption e)  //catch eception
                {
                    Console.WriteLine(e.Message); //print the error
                }
            }
            else
                o.CustomerEmail = s; //update 

            Console.WriteLine("enter a new customer address: ");
            s = Console.ReadLine();
            if (s == "") //no need to change
            {
                try
                {
                    o.CustomerAddress = order.GetById(ID).CustomerAddress; //no change
                }
                catch (DoesntExistExeption e)  //catch eception
                {
                    Console.WriteLine(e.Message); //print the error
                }
            }
            else
                o.CustomerAddress = s; //update

            Console.WriteLine("enter a new order date: ");
            s = Console.ReadLine();
            if (s == "") //no need to change
            {
                try
                {
                    o.OrderDate = order.GetById(ID).OrderDate; //no change
                }
                catch (DoesntExistExeption e)  //catch eception
                {
                    Console.WriteLine(e.Message); //print the error
                }
            }
            else
            {
                DateTime.TryParse(s, out dt);
                o.OrderDate = dt;  //update
            }

            Console.WriteLine("enter a new ship date: ");
            s = Console.ReadLine(); //get the new ship date
            if (s == "") //no need to change
            {
                try
                {
                    o.ShipDate = order.GetById(ID).ShipDate; //no change
                }
                catch (DoesntExistExeption e)  //catch eception
                {
                    Console.WriteLine(e.Message); //print the error
                }
            }
            else
            {
                DateTime.TryParse(s, out dt);
                o.ShipDate = dt; //update
            }

            Console.WriteLine("enter a new delivery date: ");
            s = Console.ReadLine();
            if (s == "") //no need to change
            {
                try
                {
                    o.DeliveryrDate = order.GetById(ID).DeliveryrDate; //no change
                }
                catch (DoesntExistExeption e)  //catch eception
                {
                    Console.WriteLine(e.Message); //print the error
                }
            }
            else
            {
                DateTime.TryParse(s, out dt); 
                o.DeliveryrDate = dt; //update
            }
            try
            {
                o.IsDeleted = order.GetById(ID).IsDeleted; //IsDeleted with no change
            }
            catch (DoesntExistExeption e)  //catch eception
            {
                Console.WriteLine(e.Message); //print the error
            }
            try
            {
                order.Update(o); //update the order
            }
            catch (DoesntExistExeption e)  //catch eception
            {
                Console.WriteLine(e.Message); //print the error
            }
        }

        if (ch == "e") //delete order
        {
            int ID;
            string ?s;
            Console.WriteLine("enter id of order to delete");
            s = Console.ReadLine();  //get the ID
            int.TryParse(s, out ID);
            try
            {
                order.Delete(ID); //delete the order
            }
            catch (DoesntExistExeption e)  //catch eception
            {
                Console.WriteLine(e.Message); //print the error
            }
        }
    }

    static void orderItem_func(DalOrderItem orderItem)  //poerations on order item
    {
        string ?ch;

        //print the prtions
        Console.WriteLine($@"
        choose one of the following commands:
        a-Add
        b-get by ID
        c-get all
        d-update
        e-delete
        f-get list order
        g - get product by order id and item id");
        ch = Console.ReadLine(); //get the choice
        Random s_rand = new Random();

        if (ch == "a")  // Add
        {
            int ID, price,  amount;
            string? s;
            OrderItem oi = new OrderItem();  // build a new order item
            Console.WriteLine("enter information for an order Item to add: ");
            Console.WriteLine("enter ID: ");
            s = Console.ReadLine(); // get ID
            int.TryParse(s, out ID);
            oi.ID = ID;
            Console.WriteLine("enter product ID: ");
            s = Console.ReadLine(); //get product id
            int.TryParse(s, out ID);
            oi.PrudoctID = ID;
            Console.WriteLine("enter order ID: ");
            s = Console.ReadLine(); //get order id
            int.TryParse(s, out ID);
            oi.OrderID = ID;
            Console.WriteLine("enter price: ");
            s = Console.ReadLine(); //get price
            int.TryParse(s, out price);
            oi.Price = price;
            Console.WriteLine("enter amount: ");
            s = Console.ReadLine(); //get amount
            int.TryParse(s, out amount);
            oi.Amount = amount;
            oi.IsDeleted = false;

            try
            {
                orderItem.Add(oi); //add the new order item to the list
            }
            catch (DoesntExistExeption e)  //catch eception
            {
                Console.WriteLine(e.Message); //print the error
            }
        }

        if (ch == "b") //GET BY ID
        {
            string? s;
            int ID;
            Console.WriteLine("enter ID: ");
            s = Console.ReadLine(); //get ID
            int.TryParse(s, out ID);
            try
            {
                Console.WriteLine(orderItem.GetById(ID)); //print the order item
            }
            catch (DoesntExistExeption e)  //catch eception
            {
                Console.WriteLine(e.Message); //print the error
            }
        }

        if (ch == "c") //print all
        {
            Console.WriteLine("The orders items are:");

            //goes through all order items and prints them
            orderItem.GetAll().ToList().ForEach(delegate (OrderItem? orderitem_from_list) { Console.WriteLine(orderitem_from_list); });


            //   CHANGE TO LINQ
            //foreach (OrderItem? orderitem_from_list in orderItem.GetAll()) //goes through all order items,there is no linq for this
            //{
            //    Console.WriteLine(orderitem_from_list); //print it
            //}
        }

        if (ch == "d") //update
        {
            string? s;
            int ID, price, amount;
            OrderItem oi = new OrderItem();
            Console.WriteLine("enter ID: ");//prints product before update
            s = Console.ReadLine();//prints product before update
            int.TryParse(s, out ID);//prints product before update
            try
            {
                Console.WriteLine(orderItem.GetById(ID));//prints product before update
            }
            catch (Exception e) //catch exception
            {
                Console.WriteLine(e);
            }
            oi.ID = ID;
            Console.WriteLine("enter a new product ID: ");
            s = Console.ReadLine(); //get a new product id
            if (s == "") //nno need to change
            {
                try
                {
                    oi.PrudoctID = orderItem.GetById(ID).PrudoctID; //no change
                }
                catch (DoesntExistExeption e)  //catch eception
                {
                    Console.WriteLine(e.Message); //print the error
                }
            }
            else
            {
                int.TryParse(s, out ID);
                oi.PrudoctID = ID; //update
            }
            Console.WriteLine("enter a new order ID: ");
            s = Console.ReadLine();
            if (s == "") //no need to change
            {
                try
                {
                    oi.OrderID = orderItem.GetById(ID).OrderID; //no change
                }
                catch (DoesntExistExeption e)  //catch eception
                {
                    Console.WriteLine(e.Message); //print the error
                }
            }
            else
            {
                int.TryParse(s, out ID);
                oi.OrderID = ID; //update
            }
            Console.WriteLine("enter a new price: ");
            s = Console.ReadLine();
            if (s == "") //no need to change
            {
                try
                {
                    oi.Price = orderItem.GetById(ID).Price; //no change
                }
                catch (DoesntExistExeption e)  //catch eception
                {
                    Console.WriteLine(e.Message); //print the error
                }
            }
            else
            {
                int.TryParse(s, out price);
                oi.Price = price; //update
            }

            Console.WriteLine("enter a new amount: ");
            s = Console.ReadLine();
            if (s == "") //no need to change
            {
                try
                {
                    oi.Amount = orderItem.GetById(ID).Amount; //no change
                }
                catch (DoesntExistExeption e)  //catch eception
                {
                    Console.WriteLine(e.Message); //print the error
                }
            }
            else
            {
                int.TryParse(s, out amount);
                oi.Amount = amount; //update
            }
            try
            {
                oi.IsDeleted = orderItem.GetById(ID).IsDeleted; //no change
            }
            catch (DoesntExistExeption e)  //catch eception
            {
                Console.WriteLine(e.Message); //print the error
            }
            try
            { 
                orderItem.Update(oi);  //update 
            }
            catch (DoesntExistExeption e)  //catch eception
            {
                Console.WriteLine(e.Message); //print the error
            }
        }

        if (ch == "e") //delete 
        {
            int ID;
            string? s;
            Console.WriteLine("enter id of Order Item to delete");
            s = Console.ReadLine(); //get ID
            int.TryParse(s, out ID);
            try
            {
                orderItem.Delete(ID); //delete
            }
            catch (DoesntExistExeption e)  //catch eception
            {
                Console.WriteLine(e.Message); //print the error
            }
        }

        if (ch == "f") //get all the order items in the same order
        {
            int ID;
            string? s;
            Console.WriteLine("enter ID");
            s = Console.ReadLine(); //get order ID
            int.TryParse(s, out ID);
            Console.WriteLine("The List of the orders with this Id are:");

            //goes through all the order items in the same order and print them
            orderItem.GetListOrder(ID).ToList().ForEach(delegate (OrderItem? orderitem_from_list) { Console.WriteLine(orderitem_from_list); });
           
            //            CHANGE TO LINQ
            //foreach (OrderItem? orderitem_from_list in orderItem.GetListOrder(ID)) //goes through all the order items in the same order,there is no linq for this
            //{
            //    Console.WriteLine(orderitem_from_list);
            //}
        }

        if (ch == "g") //get product by order id and item id
        {
            int ID, prodID;
            string ?s;
            Console.WriteLine("enter ID");
            s = Console.ReadLine();
            int.TryParse(s, out ID);
            Console.WriteLine("enter product ID");
            s = Console.ReadLine();
            int.TryParse(s, out prodID);
            try
            {
                Console.WriteLine("The product is:" + orderItem.GetProduct(ID, prodID)); //print the product
            }
            catch (DoesntExistExeption e)  //catch eception
            {
                Console.WriteLine(e.Message); //print the error
            }
        }
    }
    static void Main(string[] args)
    {
        DalOrderItem orderItem = new DalOrderItem();
        DalOrder order = new DalOrder();
        DalProduct product = new DalProduct();

        string? ch;
        do
        {
            Console.WriteLine($@"
        choose one of the following commands:
        p-for a product
        o-for an order
        oI-for an orderItem
        e-exit");  //print the options
            ch = Console.ReadLine(); // get the option

            switch (ch)
            {
                case "p":  //for product
                    product_func(product);
                    break;
                case "o":  //for orders
                    order_func(order);
                    break;
                case "oI":  //for order items
                    orderItem_func(orderItem);
                    break;
                case "e":  //to exit
                    Console.WriteLine("Have an amazing Day! Remember to always drive safe and that someone loves you :)");
                    break;
            }
        } while (ch != "e");
    }
}
using DalApi;
using System;
using DO;
using Dal;
using System.Runtime.CompilerServices;
using System.Diagnostics;
using System.Xml.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Linq.Expressions;

namespace DalTest;
partial class DalTest
{

    static void product_func(DalProduct product)
    {
        string ch;
        Console.WriteLine($@"
        choose one of the following commands:
        a-Add
        b-get by ID
        c-get all
        d-update
        e-delete");
        ch = Console.ReadLine();
        //Random s_rand = new Random();

        if (ch == "a")  //Add
        {
            int ID, price, iS;
            string s, name;
            Category category;
            Product p = new Product();
            Console.WriteLine("enter information for a product to add: ");
            Console.WriteLine("enter ID: ");
            s = Console.ReadLine();
            int.TryParse(s, out ID);
            p.ID = ID;
            Console.WriteLine("enter name: ");
            name = Console.ReadLine();
            p.Name = name;
            Console.WriteLine("enter price: ");
            s = Console.ReadLine();
            int.TryParse(s, out price);
            p.Price = price;
            Console.WriteLine("enter category: ");
            s = Console.ReadLine();
            Category.TryParse(s, out category);
            p.Category = (Category)category;
            Console.WriteLine("enter in stock: ");
            s = Console.ReadLine();
            int.TryParse(s, out iS);
            p.InStock = iS;
            p.IsDeleted = false;
            try
            {
                product.Add(p);
            }
            catch(NotImplementedException e)
            {
                Console.WriteLine("ERROR");
            }
        }
        if (ch == "b") //GET BY ID
        {
            string s;
            int ID;
            Console.WriteLine("enter ID: ");
            s = Console.ReadLine();
            int.TryParse(s, out ID);
            try
            {
                Console.WriteLine(product.GetById(ID));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

        }
        if (ch == "c")
        {
            Console.WriteLine("The products are:");

            foreach (Product product_from_list in product.GetAll())
            {
                Console.WriteLine(product_from_list);
            }
        }
        if (ch == "d")
        {

            string s, name;
            int ID, iS, price;
            Product p = new Product();
            Category category;
            Console.WriteLine("enter ID: ");//prints product before update
            s = Console.ReadLine();//prints product before update
            int.TryParse(s, out ID);//prints product before update
            try
            {
                Console.WriteLine(product.GetById(ID));//prints product before update
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            p.ID=ID;    
            Console.WriteLine("enter new name: ");
            name = Console.ReadLine();
            if (name == "")
            {
                try
                {
                    p.Name = product.GetById(ID).Name;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }
            else
                p.Name = name;

            Console.WriteLine("enter new price: ");
            s = Console.ReadLine();

            if (s == "")
            {
                try
                {
                    p.Price = product.GetById(ID).Price;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }
            else
            {
                int.TryParse(s, out price);
                p.Price = price;
            }
            Console.WriteLine("enter new category: ");
            s = Console.ReadLine();

            if (s == "")
            {
                try
                {
                    p.Category = product.GetById(ID).Category;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }
            else
            {
                Category.TryParse(s, out category);
                p.Category = (Category)category;

            }
            Console.WriteLine("enter new in stock: ");
            s = Console.ReadLine();
            if (s == "")
            {
                try
                {
                    p.InStock = product.GetById(ID).InStock;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }
            else
            {
                int.TryParse(s, out iS);
                p.InStock = iS;
            }
            try
            {
                product.Update(p);
            }
            catch(NotImplementedException e)
            {
                Console.WriteLine("ERROR");
            }
        }
        if (ch == "e")
        {
            int ID;
            string s;
            Console.WriteLine("enter id of product to delete");
            s = Console.ReadLine();
            int.TryParse(s, out ID);
            try
            {
                product.Delete(ID);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
    static void order_func(DalOrder order)  //order operations
    {
        string ch;
        Console.WriteLine($@"
        choose one of the following commands:
        a-Add
        b-get by ID
        c-get all
        d-update
        e-delete");
        ch = Console.ReadLine();
        Random s_rand = new Random();
        if (ch == "a")  //Add
        {
            int ID, price, iS;
            string s, name;
            DateTime dt;
            Order o = new Order();
            Console.WriteLine("enter information for an order to add: ");
            Console.WriteLine("enter ID: ");
            s = Console.ReadLine();
            int.TryParse(s, out ID);
            o.ID = ID;
            Console.WriteLine("enter customer name: ");
            name = Console.ReadLine();
            o.CustomerName = name;
            Console.WriteLine("enter customer email: ");
            s = Console.ReadLine();
            o.CustomerEmail = s;
            Console.WriteLine("enter customer address: ");
            s = Console.ReadLine();
            o.CustomerAddress = s;
            Console.WriteLine("enter order date: ");
            s = Console.ReadLine();
            DateTime.TryParse(s, out dt);
            o.OrderDate = dt;
            Console.WriteLine("enter ship date: ");
            s = Console.ReadLine();
            DateTime.TryParse(s, out dt);
            o.ShipDate = dt;
            Console.WriteLine("enter delivery date: ");
            s = Console.ReadLine();
            DateTime.TryParse(s, out dt);
            o.DeliveryrDate = dt;
            o.IsDeleted = false;
            try
            {
                order.Add(o);
            }
            catch(NotImplementedException e)
            {
                Console.WriteLine("ERROR");
            }
        }
        if (ch == "b") //GET BY ID
        {
            string s;
            int ID;
            Console.WriteLine("enter ID: ");
            s = Console.ReadLine();
            int.TryParse(s, out ID);
            try
            {
                Console.WriteLine(order.GetById(ID));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
        if (ch == "c")
        {
            Console.WriteLine("The orders are:");

            foreach (Order order_from_list in order.GetAll())
            {
                Console.WriteLine(order_from_list);
            }
        }
        if (ch == "d") //update
        {
            string s, name;
            int ID, iS, price;
            Order o = new Order();
            DateTime dt;

            Console.WriteLine("enter ID: ");//prints product before update
            s = Console.ReadLine();//prints product before update
            int.TryParse(s, out ID);//prints product before update
            try
            {
                Console.WriteLine(order.GetById(ID));//prints product before update
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            o.ID = ID;
            Console.WriteLine("enter a new customer name: ");
            name = Console.ReadLine();
            if (name == "")
            {
                try
                {
                    o.CustomerName = order.GetById(ID).CustomerName;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }
            else
                o.CustomerName = name;
            Console.WriteLine("enter a new customer email: ");
            s = Console.ReadLine();
            if (s == "")
            {
                try
                {
                    o.CustomerEmail = order.GetById(ID).CustomerEmail;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }
            else
                o.CustomerEmail = s;
            Console.WriteLine("enter a new customer address: ");
            s = Console.ReadLine();
            if (s == "")
            {
                try
                {
                    o.CustomerAddress = order.GetById(ID).CustomerAddress;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }
            else
                o.CustomerAddress = s;
            Console.WriteLine("enter a new order date: ");
            s = Console.ReadLine();
            if (s == "")
            {
                try
                {
                    o.OrderDate = order.GetById(ID).OrderDate;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }
            else
            {
                DateTime.TryParse(s, out dt);
                o.OrderDate = dt;
            }
            Console.WriteLine("enter a new ship date: ");
            s = Console.ReadLine();
            if (s == "")
            {
                try
                {
                    o.ShipDate = order.GetById(ID).ShipDate;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }
            else
            {
                DateTime.TryParse(s, out dt);
                o.ShipDate = dt;
            }
            Console.WriteLine("enter a new delivery date: ");
            s = Console.ReadLine();
            if (s == "")
            {
                try
                {
                    o.DeliveryrDate = order.GetById(ID).DeliveryrDate;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }
            else
            {
                DateTime.TryParse(s, out dt);
                o.DeliveryrDate = dt;
            }
            try
            {
                o.IsDeleted = order.GetById(ID).IsDeleted;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            try
            {
                order.Update(o);
            }
            catch (NotImplementedException e)
            {
                Console.WriteLine("ERROR");
            }
        }
        if (ch == "e")
        {
            int ID;
            string s;
            Console.WriteLine("enter id of order to delete");
            s = Console.ReadLine();
            int.TryParse(s, out ID);
            try
            {
                order.Delete(ID);
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }

    static void orderItem_func(DalOrderItem orderItem)  //order item poerations
    {
        string ch;
        Console.WriteLine($@"
        choose one of the following commands:
        a-Add
        b-get by ID
        c-get all
        d-update
        e-delete
        f-get list order
        g - get product by order id and item id");
        ch = Console.ReadLine();
        Random s_rand = new Random();
        if (ch == "a")  //Add
        {
            int ID, price, iS, amount;
            string s, name;
            DateTime dt;
            OrderItem oi = new OrderItem();
            Console.WriteLine("enter information for an order Item to add: ");
            Console.WriteLine("enter ID: ");
            s = Console.ReadLine();
            int.TryParse(s, out ID);
            oi.ID = ID;
            Console.WriteLine("enter product ID: ");
            s = Console.ReadLine();
            int.TryParse(s, out ID);
            oi.PrudoctID = ID;
            Console.WriteLine("enter order ID: ");
            s = Console.ReadLine();
            int.TryParse(s, out ID);
            oi.OrderID = ID;
            Console.WriteLine("enter price: ");
            s = Console.ReadLine();
            int.TryParse(s, out price);
            oi.Price = price;
            Console.WriteLine("enter amount: ");
            s = Console.ReadLine();
            int.TryParse(s, out amount);
            oi.Amount = amount;
            oi.IsDeleted = false;
            try
            {
                orderItem.Add(oi);
            }
            catch(NotImplementedException e)
            {
                Console.WriteLine("ERROR");
            }
        }
        if (ch == "b") //GET BY ID
        {
            string s;
            int ID;
            Console.WriteLine("enter ID: ");
            s = Console.ReadLine();
            int.TryParse(s, out ID);
            try
            {
                Console.WriteLine(orderItem.GetById(ID));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
        if (ch == "c") //print all
        {
            Console.WriteLine("The orders items are:");

            foreach (OrderItem orderitem_from_list in orderItem.GetAll())
            {
                Console.WriteLine(orderitem_from_list);
            }
        }
        if (ch == "d") //update
        {
            string s, name;
            int ID, iS, price, amount;
            OrderItem oi = new OrderItem();
            DateTime dt;
            Console.WriteLine("enter ID: ");//prints product before update
            s = Console.ReadLine();//prints product before update
            int.TryParse(s, out ID);//prints product before update
            try
            {
                Console.WriteLine(orderItem.GetById(ID));//prints product before update
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            oi.ID = ID;
            Console.WriteLine("enter a new product ID: ");
            s = Console.ReadLine();
            if (s == "")
            {
                try
                {
                    oi.PrudoctID = orderItem.GetById(ID).PrudoctID;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }
            else
            {
                int.TryParse(s, out ID);
                oi.PrudoctID = ID;
            }
            Console.WriteLine("enter a new order ID: ");
            s = Console.ReadLine();
            if (s == "")
            {
                try
                {
                    oi.OrderID = orderItem.GetById(ID).OrderID;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }
            else
            {
                int.TryParse(s, out ID);
                oi.OrderID = ID;
            }
            Console.WriteLine("enter a new price: ");
            s = Console.ReadLine();
            if (s == "")
            {
                try
                {
                    oi.Price = orderItem.GetById(ID).Price;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }
            else
            {
                int.TryParse(s, out price);
                oi.Price = price;
            }
            Console.WriteLine("enter a new amount: ");
            s = Console.ReadLine();
            if (s == "")
            {
                try
                {
                    oi.Amount = orderItem.GetById(ID).Amount;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }
            else
            {
                int.TryParse(s, out amount);
                oi.Amount = amount;
            }
            try
            {
                oi.IsDeleted = orderItem.GetById(ID).IsDeleted;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            try
            {
                orderItem.Update(oi);
            }
            catch (NotImplementedException e)
            {
                Console.WriteLine("ERROR");
            }
        }
        if (ch == "e")
        {
            int ID;
            string s;
            Console.WriteLine("enter id of Order Item to delete");
            s = Console.ReadLine();
            int.TryParse(s, out ID);
            try
            {
                orderItem.Delete(ID);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
        if (ch == "f")
        {
            int ID;
            string s;
            Console.WriteLine("enter ID");
            s = Console.ReadLine();
            int.TryParse(s, out ID);
            Console.WriteLine("The List of the orders with this Id are:");
            foreach (OrderItem orderitem_from_list in orderItem.GetListOrder(ID))
            {
                Console.WriteLine(orderitem_from_list);
            }
        }
        if (ch == "g")
        {
            int ID, prodID;
            string s;
            Console.WriteLine("enter ID");
            s = Console.ReadLine();
            int.TryParse(s, out ID);
            Console.WriteLine("enter product ID");
            s = Console.ReadLine();
            int.TryParse(s, out prodID);
            Console.WriteLine("The product is:" + orderItem.GetProduct(ID, prodID));
        }
    }
    static void Main(string[] args)
    {
        DalOrderItem orderItem = new DalOrderItem();
        DalOrder order = new DalOrder();
        DalProduct product = new DalProduct();

        string ch, ch2;
        do
        {
            Console.WriteLine($@"
        choose one of the following commands:
        p-for a product
        o-for an order
        oI-for an orderItem
        e-exit");
            ch = Console.ReadLine();
            switch (ch)
            {
                case "p":
                    product_func(product);
                    break;
                case "o":
                    order_func(order);
                    break;
                case "oI":
                    orderItem_func(orderItem);
                    break;
                case "e":
                    Console.WriteLine("Have an amazing Day! Remember to always drive safe and that someone loves you :)");
                    break;
            }
        } while (ch != "e");
    }
}
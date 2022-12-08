using BlApi;
using BlImplementation;
using BO;
using Microsoft.VisualBasic;

namespace BLTest;
internal class BlTest
{

    static void funcProduct(IBl bl)
    {

        string ch;
        do
        {
            /*
             public IEnumerable<ProductForList> GetListProduct();//בקשת רשימת מוצרים
    public Product GetProductInfo_manager(int id);//בקשת פרטי מוצר עבור מסך מנהל
    public ProductItem GetProductInfo_client(int id,Cart cart);//בקשת פרטי מוצר עבור מסך לקוח

    public void AddProdut(Product product);
    public void DeleteProduct(int id);
    public void UpdateProduct(Product product);*/


            Console.WriteLine($@"
        choose one of the following commands:
        1-List of products
        2-Get product info for manager
        3-Add product
        4-Delete product
        5-Update product
        e-home");
            ch = Console.ReadLine();
            int id,inStock,price;
            string s,name;
            Category c;
            switch (ch)
            {
                case "1":  //List of products
                    Console.WriteLine("The prouducts are:");
                    foreach (BO.ProductForList var in bl.Product.GetListProduct())
                    {
                        //Console.WriteLine(var.ID);
                        //Console.WriteLine("case 1 p");
                        Console.WriteLine(var);
                    }
                    break;

                case "2":  //Get product info for manager
                    Console.WriteLine("Enter product id:");
                    s= Console.ReadLine();
                    int.TryParse(s,out id);
                    Console.WriteLine(bl.Product.GetProductInfo_manager(id));
                    break;

                case "3":  //Add product
                    Console.WriteLine("Enter product Id:");
                    s = Console.ReadLine();
                    int.TryParse(s, out id);
                    Console.WriteLine("Enter product name:");
                    name = Console.ReadLine();
                    Console.WriteLine("Enter product category:");
                    s = Console.ReadLine();
                    Category.TryParse(s, out c);
                    Console.WriteLine("Enter amount in stock:");
                    s = Console.ReadLine();
                    int.TryParse(s, out inStock);
                    Console.WriteLine("Enter price:");
                    s = Console.ReadLine();
                    int.TryParse(s, out price);


                    BO.Product prod = new BO.Product() { Category=c, ID=id, Name=name, InStock=inStock, Price=price};
                    try
                    {
                        bl.Product.AddProdut(prod);
                    }
                    catch(BO.DoesntExistExeption e)
                    {
                        Console.WriteLine(e.Message);
                        Console.WriteLine(e.InnerException.Message);
                    }

                    break;
                case "4"://Delete product
                    Console.WriteLine("Enter product Id to delete:");
                    s = Console.ReadLine();
                    int.TryParse(s, out id);
                    try
                    {
                        bl.Product.DeleteProduct(id);
                    }
                    catch (BO.DoesntExistExeption e)
                    {
                        Console.WriteLine(e.Message);
                        Console.WriteLine(e.InnerException.Message);
                    }
                    break;
                case "5"://Update product
                    Console.WriteLine("Enter product Id:");
                    s = Console.ReadLine();
                    int.TryParse(s, out id);
                    Console.WriteLine("Enter product name:");
                    name = Console.ReadLine();
                    Console.WriteLine("Enter product category:");
                    s = Console.ReadLine();
                    Category.TryParse(s, out c);
                    Console.WriteLine("Enter amount in stock:");
                    s = Console.ReadLine();
                    int.TryParse(s, out inStock);
                    Console.WriteLine("Enter price:");
                    s = Console.ReadLine();
                    int.TryParse(s, out price);
                    BO.Product prodforUp = new BO.Product() { Category = c, ID = id, Name = name, InStock = inStock, Price = price };
                    bl.Product.UpdateProduct(prodforUp);
                    break;

                case "e":  //to exit
                    break;
            }
        } while (ch != "e");
    }
    static void funcOrder(IBl bl)
    {
        string ch;
        do
        {
            /*public IEnumerable<OrderForList> GetOrders();
    public BO.Order OrderInfo(int orderId);
    public BO.Order UpdateShip(int orderId);
    public BO.Order UpdateDelivery(int orderId);
    public BO.OrderTracking TrackingOrder(int orderId);

    public void UpdateByManager(int orderID,int orderItemId, int amount);//bonus
*/


            Console.WriteLine($@"
        choose one of the following commands:
        1-List of orders
        2-Get order info 
        3-Update ship date
        4-Update delivery date
        5-Tracking order
        6-Update by manager
        e-home");
            ch = Console.ReadLine();
            int id, inStock, price;
            string s, name;
            Category c;
            switch (ch)
            {
                case "1":  //List of orders
                    Console.WriteLine("The orders are:");
                    foreach (BO.OrderForList var in bl.Order.GetOrders())
                    {
                        Console.WriteLine(var);
                    }
                    break;

                case "2":  //Get order info 
                    Console.WriteLine("Enter order id:");
                    s = Console.ReadLine();
                    int.TryParse(s, out id);
                    try
                    {
                        Console.WriteLine(bl.Order.OrderInfo(id));
                    }
                    catch (BO.DoesntExistExeption e)
                    {
                        Console.WriteLine(e.Message);
                        Console.WriteLine(e.InnerException.Message);
                    }
                    break;

                case "3":  //Update ship date
                    Console.WriteLine("Enter  Id:");
                    s = Console.ReadLine();
                    int.TryParse(s, out id);
                    try
                    {
                        Console.WriteLine(bl.Order.UpdateShip(id));
                    }
                    catch (BO.DoesntExistExeption e)
                    {
                        Console.WriteLine(e.Message);
                        Console.WriteLine(e.InnerException.Message);
                    }
                    break;
                case "4"://Update delivery date
                    Console.WriteLine("Enter  Id:");
                    s = Console.ReadLine();
                    int.TryParse(s, out id);
                    try
                    {
                        Console.WriteLine(bl.Order.UpdateDelivery(id));
                    }
                    catch (BO.DoesntExistExeption e)
                    {
                        Console.WriteLine(e.Message);
                        Console.WriteLine(e.InnerException.Message);
                    }
                    break;
                case "5"://Tracking order
                    Console.WriteLine("Enter order Id:");
                    s = Console.ReadLine();
                    int.TryParse(s, out id);
                    try
                    {
                        Console.WriteLine( bl.Order.TrackingOrder(id));
                    }
                    catch (BO.DoesntExistExeption e)
                    {
                        Console.WriteLine(e.Message);
                        Console.WriteLine(e.InnerException.Message);
                    }
                    break;
                case "6"://Update by manager
                    int orderID, orderItemId, amount;
                    Console.WriteLine("Enter order Id:");
                    s = Console.ReadLine();
                    int.TryParse(s, out orderID);
                    Console.WriteLine("Enter order item Id:");
                    s = Console.ReadLine();
                    int.TryParse(s, out orderItemId);
                    Console.WriteLine("Enter amount:");
                    s = Console.ReadLine();
                    int.TryParse(s, out amount);
                    try
                    {
                       bl.Order.UpdateByManager(orderID,orderItemId,amount);
                    }
                    catch (BO.DoesntExistExeption e)
                    {
                        Console.WriteLine(e.Message);
                        Console.WriteLine(e.InnerException.Message);
                    }
                    break;
                case "e":  //to exit
                    break;
            }
        } while (ch != "e");
    }
    static void funcCart(IBl bl)
    {
        int id, price,amount;
        string s, name, customerAddress, customerEmail;
        Console.WriteLine("Enter information for cart:");
        Console.WriteLine("Enter customer address:");
        customerAddress = Console.ReadLine();
        Console.WriteLine("Enter customer email:");
        customerEmail = Console.ReadLine();
        Console.WriteLine("Enter customer name:");
        name = Console.ReadLine();
        Cart cart = new Cart() { CustomerAddress = customerAddress, CustomerEmail = customerEmail, CustomerName = name, Items = new List<BO.OrderItem>(), TotalPrice = 0 };

 

        string ch;
        do
        {
            /*
    public Cart AddProductToCart(Cart cart, int id);
    public Cart UpdateAmountInCart(Cart cart, int id, int amount);
    public void MakingAnOrder(Cart cart);
    
*/
            Console.WriteLine($@"
        choose one of the following commands:
        1-Add product to cart
        2-Update amount in cart
        3-Make an order
        e-home");
            ch = Console.ReadLine();
             switch (ch)
            {
                case "1"://Add product to cart
                    Console.WriteLine("Enter  Id:");
                    s = Console.ReadLine();
                    int.TryParse(s, out id);
                    try
                    {
                        cart=bl.Cart.AddProductToCart(cart,id);
                        Console.WriteLine(cart);

                    }
                    catch (BO.DoesntExistExeption e)
                    {
                        Console.WriteLine(e.Message);
                        Console.WriteLine(e.InnerException.Message);
                    }

                    break;

                case "2"://Update amount in cart  
                    Console.WriteLine("Enter  Id:");
                    s = Console.ReadLine();
                    int.TryParse(s, out id);
                    Console.WriteLine("Enter  amount:");
                    s = Console.ReadLine();
                    int.TryParse(s, out amount);

                    try
                    {
                        cart = bl.Cart.UpdateAmountInCart(cart,id,amount);
                        Console.WriteLine(cart);

                    }
                    catch (BO.DoesntExistExeption e)
                    {
                        Console.WriteLine(e.Message);
                        Console.WriteLine(e.InnerException.Message);
                    }
                    break;

                case "3"://Make an order  
                    bl.Cart.MakingAnOrder(cart);
                    break;
               
                case "e":  //to exit
                    break;
            }
        } while (ch != "e");
    }
    static void Main(string[] args)
    {
        IBl bl = new Bl();
        
        string ch;
        do
        {
            Console.WriteLine($@"
        choose one of the following commands:
        1-for a product
        2-for an order
        3-for a cart
        e-exit"); 
            ch = Console.ReadLine();
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
            }
        } while (ch != "e");



    }
}
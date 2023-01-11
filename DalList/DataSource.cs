using DO;
using System;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Threading.Channels;
using static Dal.DataSource;

namespace Dal;

internal class DataSource 
{
    
    private static readonly Random s_rand = new Random(); // randome

    internal static DataSource s_instance { get; } = new DataSource(); // called by the progarm

    private DataSource() { s_initialize(); } // initialize

    //lists
    internal List<Order?> _orders { get; } = new List<Order?> { };
    internal List<OrderItem?> _orderItems { get; } = new List<OrderItem?> { };
    internal List<Product?> _products { get; } = new List<Product?> { };

    private const int product_total = 20;
    private const int order_total = 10;

    String[] cities = { "Tel Aviv", "Jerusalem", "Haifa","Ashdod","Lod","Bnei Brak","Ramat Gan", "Giv'ataim",
        "Holon", "Bat Yam", "Rishon le-zion", "Karnei Shomron", "Ariel","Efrat","Nokdim","Ashkelon", "Kiriat Gat" };
    
    String[] customers = { "Miley Cyrus","Coco Chanel", "Demi Lovato", "Sabrina Carpenter","Jusstin Beiber", "Chris Hemsworth",
        "Cardi B", "Nicki MInaj","Katy Perry","Shawn Mendes","Kim Kardshian", "Selena Gomez", "Taylor Swift","Adam Levine",
        "Kylie Jenner","Camilla Cabello","Meghan Trainer", "Harry Styles", "Zara Larsson", "Gigi Hadid", "Bella Hadid","Dua Lipa",
        " ","Riahana"};
    
    String[] cars = {"X8","E20","LS","Q8","CLS","A9","E-tron","Class","Bentayaga","NT","Series 3","F-Type","XC40",
        "MODEL 2","INFINITI","XJ","Phantom"};

    private void createOrders()  // create orders list
    {       
        for (int i = order_total; i > 0; i--)  // initialize orders
        {
            int customer = s_rand.Next(customers.Length);
            bool shipped = s_rand.NextDouble() < 0.7D;
            bool delivered = s_rand.NextDouble() < 0.3D;
            DateTime order_date = DateTime.Now - new TimeSpan(s_rand.NextInt64(10L * 1000L * 1000L * 3600L * 24L * 100L));
            
            DateTime delivery_date = (DateTime.MinValue);
          
            if (i < order_total * 0.8)
            {
                if (i < order_total * 0.6)
                {
                    DateTime ship_date = (order_date + new TimeSpan(s_rand.NextInt64(10L * 1000L * 1000L * 3600L * 24L * 7L)));
                    delivery_date = ship_date + new TimeSpan(s_rand.NextInt64(10L * 1000L * 1000L * 3600L * 24L * 3L));

                    Order order = new()  // create new order
                    {
                        ID = config.NextOrderNumber,
                        CustomerName = customers[customer],
                        CustomerAddress = cities[s_rand.Next(cities.Length)],
                        CustomerEmail = customers[customer].Split(' ')[0]+ customers[customer].Split(' ')[1] + "@jctmail.com",
                        OrderDate = order_date,
                        IsDeleted = false,
                        DeliveryrDate = delivery_date,
                        ShipDate = ship_date
                    };
                    _orders.Add(order); // add the order
                }
                else
                {
                    DateTime ship_date = (order_date + new TimeSpan(s_rand.NextInt64(10L * 1000L * 1000L * 3600L * 24L * 7L)));

                    Order order = new()  // create new order
                    {
                        ID = config.NextOrderNumber,
                        CustomerName = customers[customer],
                        CustomerAddress = cities[s_rand.Next(cities.Length)],
                        CustomerEmail = customers[customer].Split(' ')[0] + customers[customer].Split(' ')[1] + "@jctmail.com",
                        OrderDate = order_date,
                        IsDeleted = false,
                        ShipDate = ship_date,
                    };
                    _orders.Add(order); // add the order
                }

            }
            else
            {
                    Order order = new()  // create new order
                    {
                        ID = config.NextOrderNumber,
                        CustomerName = customers[customer],
                        CustomerAddress = cities[s_rand.Next(cities.Length)],
                        CustomerEmail = customers[customer].Split(' ')[0] + customers[customer].Split(' ')[1] + "@jctmail.com",
                        OrderDate = order_date,
                        IsDeleted = false,

                    };
                _orders.Add(order); // add the order

            }
        }
    }

    private void createProducts()  // create products list
    {

        for (int i = 0; i < product_total; i++)  //  initlialize
        {
            int car = s_rand.Next(cars.Length);
            int category = s_rand.Next(Enum.GetNames(typeof(Category)).Length);
            int in_stock = 0;
            if (i > product_total * 0.05)
            {
                in_stock = s_rand.Next(1, 30);

            }
            _products.Add(
            new Product  // create new product
            {
                ID = configP.NextOrderNumber,
                Name = cars[car],
                Price = s_rand.Next(500000, 3000000),
                Category = (Category)category,
                InStock = in_stock,
                IsDeleted = false,

            }
            );
        }
    }
    
    private void createOrderItems()  // craete order items list
    {
        for (int i = 0; i < 40; i++)  // initialize
        {
            Product? product = _products[s_rand.Next(_products.Count)];
            _orderItems.Add(
                new OrderItem  // new order item
                {
                    OrderID = s_rand.Next(config.s_startOrderNumber-1, config.s_startOrderNumber + _orders.Count +1),
                    PrudoctID = product?.ID ?? 0,
                    Price = product?.Price ?? 0,
                    Amount = s_rand.Next(1,5),
                    ID = config.NextOrderNumber,
                    IsDeleted=false,

                });
        }
    }


    void s_initialize() // called by the program
    {
        createProducts();
        createOrders();
        createOrderItems();
    }

    internal static class config // for the order and order item id
    {
        internal const int s_startOrderNumber = 1000;
        private static int s_nextOrderNumber = s_startOrderNumber;
        internal static int NextOrderNumber { get => ++ s_nextOrderNumber; }

        
    }

    internal static class configP  // for the product ID
    {
        internal const int s_startOrderNumber = 100000;
        private static int s_nextOrderNumber = s_startOrderNumber;
        internal static int NextOrderNumber { get => ++s_nextOrderNumber; }


    }
}

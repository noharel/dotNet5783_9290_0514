using DO;
using System;
using System.Diagnostics;
using static Dal.DataSource;

namespace Dal;

internal class DataSource 
{
    private static readonly Random s_rand = new Random();
    //static Random s_rand = new Random(DateTime.Now.Millisecond);
    internal static DataSource s_instance { get; } = new DataSource();

    private DataSource() { s_initialize(); }

    //lists
    internal List<Order> _orders { get; } = new List<Order> { };
    internal List<OrderItem> _orderItems { get; } = new List<OrderItem> { };
    internal List<Product> _products { get; } = new List<Product> { };
    private const int product_total = 20;
    private const int order_total = 10;

    String[] cities = { "Tel Aviv", "Jerusalem", "Haifa","Ashdod","Lod","Bnei Brak","Ramat Gan", "Giv'ataim",
        "Holon", "Bat Yam", "Rishon le-zion", "Karnei Shomron", "Ariel","Efrat","Nokdim","Ashkelon", "Kiriat Gat" };
    String[] customers = { "Miley Cyrus","Coco Chanel", "Demi Lovato", "Sabrina Carpenter","Jusstin Beiber", "Chris Hemsworth",
        "Cardi B", "Nicki MInaj","Katy Perry","Shawn Mendes","Kim Kardshian", "Selena Gomez", "Taylor Swift","Adam Levine",
        "Kylie Jenner","Camilla Cabello","Meghan Trainer", "Harry Styles", "Zara Larsson", "Gigi Hadid", "Bella Hadid","Dua Lipa",
        "Ariana Grande","Riahana"};
    String[] cars = {"X8","E20","LS","Q8","CLS","A9","E-tron","Class","Bentayaga","NT","Series 3","F-Type","XC40",
        "MODEL 2","INFINITI","XJ","Phantom"};
    private void createOrders()
    {

        for (int i = order_total; i > 0; i--)
        {
            int customer = s_rand.Next(customers.Length);
            bool shipped = s_rand.NextDouble() < 0.7D;
            bool delivered = s_rand.NextDouble() < 0.3D;
            DateTime order_date = DateTime.Now - new TimeSpan(s_rand.NextInt64(10L * 1000L * 1000L * 3600L * 24L * 100L));
            // DateTime ship_date?? 0;
            DateTime ship_date = (DateTime.MinValue);
            if (i<order_total*0.8)
            {
                ship_date = (order_date - new TimeSpan(s_rand.NextInt64(10L * 1000L * 1000L * 3600L * 24L * 7L)));
            }
            DateTime delivery_date = (DateTime.MinValue);
            if (i < order_total * 0.6)
            {
               delivery_date = ship_date - new TimeSpan(s_rand.NextInt64(10L * 1000L * 1000L * 3600L * 24L * 7L));
            }
            Order order = new()
            {
                ID = config.NextOrderNumber,
                CustomerName = customers[customer],
                CustomerAddress = cities[s_rand.Next(cities.Length)],
                CustomerEmail = customers[customer] + "@jctmail.com",
                OrderDate = order_date,
                ShipDate=ship_date,
                DeliveryrDate=delivery_date,    
                IsDeleted = false,

            };
            _orders.Add(order);
        }
    }

    private void createProducts()
    {

        for (int i = 0; i < product_total; i++)
        {
            int car = s_rand.Next(cars.Length);
            int category = s_rand.Next(Enum.GetNames(typeof(Category)).Length);
            int in_stock = 0;
            if (i > product_total * 0.05)
            {
                in_stock = s_rand.Next(1, 30);

            }
                _products.Add(
                new Product
                {
                    ID = configP.NextOrderNumber,
                    Name = cars[car],
                    Price = s_rand.Next(500000, 3000000),
                    Category = (Category)category,
                    InStock= in_stock,
                    IsDeleted= false,   

                }
                ); ;
          
        }

        Product add_Product = new Product() { ID = s_rand.Next(100000, 1000000), };
    }
    
    private void createOrderItems()
    {
        for (int i = 0; i < 40; i++)
        {
            Product? product = _products[s_rand.Next(_products.Count)];
            _orderItems.Add(
                new OrderItem
                {
                    OrderID = s_rand.Next(config.s_startOrderNumber, config.s_startOrderNumber + _orders.Count),
                    PrudoctID = product?.ID ?? 0,
                    Price = product?.Price ?? 0,
                    Amount = s_rand.Next(1,5),
                    ID = config.NextOrderNumber,
                    IsDeleted=false,

                });
        }
    }


    void s_initialize()
    {
        createProducts();
        createOrders();
        createOrderItems();
    }

    internal static class config
    {
        internal const int s_startOrderNumber = 1000;
        private static int s_nextOrderNumber = s_startOrderNumber;
        internal static int NextOrderNumber { get => ++ s_nextOrderNumber; }

        
    }
    internal static class configP
    {
        internal const int s_startOrderNumber = 100000;
        private static int s_nextOrderNumber = s_startOrderNumber;
        internal static int NextOrderNumber { get => ++s_nextOrderNumber; }


    }
}

using DO;
using DalApi;
using System;


void product_func(Product product, string ch)
{
    if(ch=="a")
    {
        
    }
}
void order_func(Product product, string ch)
{

}
void orderItem_func(Product product, string ch)
{

}
void Main()
{
    Product product = new Product();
    Order order = new Order();
    OrderItem item = new OrderItem();
    string ch,ch2;
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
                Console.WriteLine($@"
        choose one of the following commands:
        a-Add
        b-get by ID
        c-get all
        d-update
        e-delete");
                ch2 = Console.ReadLine();
                product_func (product, ch2); 

                break;
            case "o":
                Console.WriteLine($@"
        choose one of the following commands:
        a-Add
        b-get by ID
        c-get all
        d-update
        e-delete");
                ch2 = Console.ReadLine();
                order_func(product, ch2);
                break;
            case "oI":
                Console.WriteLine($@"
        choose one of the following commands:
        a-Add
        b-get by ID
        c-get all
        d-update
        e-delete");
                ch2 = Console.ReadLine();
                orderItem_func(product, ch2);
                break;


        }

    } while (ch != "e");
}

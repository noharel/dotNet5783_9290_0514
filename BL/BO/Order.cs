using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO;
/// <summary>
/// Order for BO
/// </summary>
public class Order
{
    public int ID { get; set; }//Id for order
    public string? CustomerName { get; set; }//name of customer
    public string? CustomerEmail { get; set; }//email of customer
    public string? CustomerAddress { get; set; }//addres of customer
    public DateTime? OrderDate { get; set; }//date of order
    public DateTime? ShipDate { get; set; }//date of ship
    public DateTime? DeliveryrDate { get; set; }//date of delivery
    public OrderStatus Status { get; set; }//status of order
    public List<BO.OrderItem>? Items { get; set; }//list of items
    public double TotalPrice { get; set; }

    public override string ToString()//for printing
    {
        int i = 1;
        string s = "ID:" + ID + "\nCustomerName:" + CustomerName + "\nCustomerEmail:" + CustomerEmail + "\nCustomerAddress:" + CustomerAddress + "\nOrderDate:" + OrderDate + "\nShipDate:" + ShipDate + "\nDeliveryrDate:" + DeliveryrDate + "\nStatus:" + Status + "\nItems:" + "\n";
        if (Items != null)
        {
            foreach (BO.OrderItem var in Items)//for every item in items add it to the string
            {
                s += i + ":";
                s += var.ToString() + "\n";
                i++;
            }
        }
        return s;
    }



}

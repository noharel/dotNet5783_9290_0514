using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO;

public class Order
{
    public int ID { get; set; }
    public string CustomerName { get; set; }
    public string CustomerEmail { get; set; }
    public string CustomerAddress { get; set; }
    public DateTime? OrderDate { get; set; }
  
    public DateTime? ShipDate { get; set; }
    public DateTime? DeliveryrDate { get; set; }
    public OrderStatus Status { get; set; }
    public IEnumerable<BO.OrderItem> Items { get; set; }
    public double TotalPrice { get; set; }

    public override string ToString()
    {
        int i = 1;
        string s = "ID:" + ID + "\nCustomerName:" + CustomerName + "\nCustomerEmail:" + CustomerEmail + "\nCustomerAddress:" + CustomerAddress + "\nOrderDate:" + OrderDate+ "\nShipDate:" + ShipDate+ "\nDeliveryrDate:" + DeliveryrDate + "\nStatus:" + Status + "\nItems:" + "\n";
        foreach(BO.OrderItem var in Items)
        {
            s += i + ":";
            s += var.ToString() + "\n";
            i++;
        }
        return s;
    }
    


}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO;

public class Cart
{
    public string? CustomerName { get; set; }
    public string? CustomerEmail { get; set; }
    public string? CustomerAddress { get; set; }
    public List<BO.OrderItem>? Items { get; set; }
    public double TotalPrice { get; set; }

    public override string ToString()
    {
        int i = 1;
        string s = "CustomerName:" + CustomerName + "\nCustomerEmail:" + CustomerEmail + "\nCustomerAddress:" + CustomerAddress + "\nTotalPrice:" + TotalPrice + "\nItems:\n";
        foreach(BO.OrderItem var in Items )
        {
            s+= i + ":" + var.ToString() + "\n";
            i++;
        }
        return s;
    }

}

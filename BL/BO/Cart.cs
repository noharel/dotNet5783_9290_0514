using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO;
/// <summary>
/// For a Cart
/// </summary>
public class Cart
{
    public string? CustomerName { get; set; }//name of customer
    public string? CustomerEmail { get; set; }//email of customer
    public string? CustomerAddress { get; set; }//address of customer
    public List<BO.OrderItem>? Items { get; set; }//list of item
    public double TotalPrice { get; set; }

    public override string ToString()//for prinring
    {
        int i = 1;
        string s = "CustomerName:" + CustomerName + "\nCustomerEmail:" + CustomerEmail + "\nCustomerAddress:" + CustomerAddress + "\nTotalPrice:" + TotalPrice + "\nItems:\n";
        foreach (BO.OrderItem var in Items!)//for every item in items add it to the string
        {
            s += i + ":" + var.ToString() + "\n";
            i++;
        }
        return s;
    }

}

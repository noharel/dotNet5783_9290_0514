using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO;
/// <summary>
/// Order for list BO
/// </summary>
public class OrderForList
{
    public int ID { get; set; }//id of order for list
    public string? CustomerName { get; set; }//name of customer
    public OrderStatus Status { get; set; }//status of order for list
    public int AmountOfItems { get; set; }//amount of items
    public double TotalPrice { get; set; }//total price of order

    public override string ToString()//for printing
    { return ToolStringClass.ToStringProperty(this); }

}



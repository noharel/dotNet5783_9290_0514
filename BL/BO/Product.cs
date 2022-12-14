using DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO;
/// <summary>
/// Product for BO
/// </summary>
public class Product
{
    public int ID { get; set; }//product id
    public string? Name { get; set; }// name of product
    public double Price { get; set; }//price of product
    public Category? Category { get; set; }//category of product
    public int InStock { get; set; }//Instock for product

    public override string ToString()//for printing
    { return ToolStringClass.ToStringProperty(this); }
}

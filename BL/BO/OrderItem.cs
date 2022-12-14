

namespace BO;
/// <summary>
/// OrderItem for BO
/// </summary>
public class OrderItem
{
    public int ID { get; set; }//id of orderItem
    public string? Name { get; set; }//name of orderItem
    public int ProductID { get; set; }//product id of orderItem
    public double Price { get; set; }//price of orderItem
    public int Amount { get; set; }//amount of OrderItem
    public double TotalPrice { get; set; }//total price of orderItem

    public override string ToString()//for printing
    { return ToolStringClass.ToStringProperty(this); }
}

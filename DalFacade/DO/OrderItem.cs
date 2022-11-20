


using System.Xml.Linq;

namespace DO;
public struct OrderItem
{
    // Unique Id of the OrderItem
    public int ID { get; set; }
    //Id of the product that was ordered
    public int PrudoctID { get; set; }
    //Id of the order
    public int OrderID { get; set; }
    //The price of the orderitem
    public double Price { get; set; }
    //teh amount of the orederutem
    public int Amount { get; set; }
    //If the order item was deleted 
    public bool IsDeleted { get; set; }
    public override string ToString()
    {
        return ToolStringClass.ToStringProperty(this);
    }
}


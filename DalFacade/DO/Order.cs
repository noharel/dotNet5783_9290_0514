

namespace DO;
public struct Order
{
    // Unique Id of the order
    public int ID { get; set; }
    //The name of the customer who placed the order
    public string CustomerName { get; set; }
    //The email adress of the customer who placed the order
    public string CustomerEmail { get; set; }
    //The customer adress of the customer who placed the order
    public string CustomerAddress { get; set; }
    //The date the order that was placed
    public DateTime? OrderDate { get; set; }
    //The date the order that was shipped
    public DateTime? ShipDate { get; set; }
    //The date the order that was delivered
    public DateTime? DeliveryrDate { get; set; }
    //If the order item was deleted 
    public bool IsDeleted { get; set; }
    public override string ToString()
    {
        return ToolStringClass.ToStringProperty(this);
    }
}


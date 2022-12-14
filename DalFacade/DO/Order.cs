

namespace DO;
public struct Order
{
   
    public int ID { get; set; } // Unique Id of the order
    
    public string? CustomerName { get; set; }// The name of the customer who placed the order
    
    public string? CustomerEmail { get; set; }// The email adress of the customer who placed the order
    public string? CustomerAddress { get; set; }    // The customer adress of the customer who placed the order

    public DateTime? OrderDate { get; set; }    // The date the order that was placed

    public DateTime? ShipDate { get; set; }    // The date the order that was shipped

    public DateTime? DeliveryrDate { get; set; }    // The date the order that was delivered

    public bool IsDeleted { get; set; }    //  If the order item was deleted 

    public override string ToString() // FOR PRINTING
    {
        return ToolStringClass.ToStringProperty(this);
    }
}


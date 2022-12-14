


using System.Xml.Linq;

namespace DO;
public struct OrderItem
{
 
    public int ID { get; set; }   // Unique Id of the OrderItem
    
    public int PrudoctID { get; set; } // Id of the product that was ordered
    
    public int OrderID { get; set; } // Id of the order
    
    public double Price { get; set; } // The price of the orderitem
    
    public int Amount { get; set; } // the amount of the orederutem
   
    public bool IsDeleted { get; set; } // If the order item was deleted 
    public override string ToString() // for printing
    {
        return ToolStringClass.ToStringProperty(this);
    }
}


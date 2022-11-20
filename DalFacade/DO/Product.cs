

namespace DO;

public struct Product
{
    // Unique Id of the OrderItem
    public int ID { get; set; }
    //Name of the product
    public string Name { get; set; }
    //price of the product
    public double Price { get; set; }
    //Category of the product
    public Category Category { get; set; }
    //if the product is in stock
    public int InStock { get; set ; }
    //If the order item was deleted 
    public bool IsDeleted { get; set ; }

    public override string ToString()
    {
        return ToolStringClass.ToStringProperty(this);
    }
   
}


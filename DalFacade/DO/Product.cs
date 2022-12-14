namespace DO;
public struct Product
{
    
    public int ID { get; set; } // Unique Id of the OrderItem
    
    public string? Name { get; set; } // Name of the product
    
    public double Price { get; set; } // price of the product
    
    public Category? Category { get; set; } // Category of the product
    
    public int InStock { get; set ; } // if the product is in stock
    
    public bool IsDeleted { get; set ; } // If the order item was deleted 
    public override string ToString() // for printing
    {
        return ToolStringClass.ToStringProperty(this);
    }
}
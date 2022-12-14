
namespace BO;
/// <summary>
/// ProductItem for BO
/// </summary>
public class ProductItem
{
    public int ID { get; set; }//id of 
    public string? Name { get; set; }//name of ProductItem
    public double Price { get; set; }//price of ProductItem
    public Category? Category { get; set; }//category of ProductItem
    public int Amount { get; set; }//amount of ProductItem
    public bool InStock { get; set; }//instock of ProductItem

    public override string ToString()//for printing
    {
        Console.WriteLine("in to string product");
        return ToolStringClass.ToStringProperty(this);
    }

}
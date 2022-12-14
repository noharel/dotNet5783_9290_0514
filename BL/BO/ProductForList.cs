
namespace BO;
/// <summary>
/// ProductForList for BO
/// </summary>
public class ProductForList
{
    public int ID { get; set; }//id of product for list
    public string? Name { get; set; }//name of product for list
    public double Price { get; set; }//price of product for list
    public Category? Category { get; set; }//category of product for list

    public override string ToString()//for printing
    { return ToolStringClass.ToStringProperty(this); }
}

using DO;

namespace DalApi;
public interface IOrderItem : ICrud<OrderItem>   // ABSTRACT INTERFACE
{
    
    IEnumerable<OrderItem?> GetListOrder(int id); // GET ALL THE ITEMS IN THE SAME ORDER
    public OrderItem? GetProduct(int orderID, int itemID); // GET AN ITEM FROM THE ORDER

}


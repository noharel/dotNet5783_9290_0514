using DO;

namespace DalApi;
public interface IOrderItem : ICrud<OrderItem>   //abstract interface
{
    List<OrderItem> GetListOrder(int id);
    public OrderItem GetProduct(int orderID, int itemID);

}


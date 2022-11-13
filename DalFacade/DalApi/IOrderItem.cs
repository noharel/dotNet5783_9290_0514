using DO;

namespace DalApi;
public interface IOrderItem : ICrud<OrderItem>
{
    List<OrderItem> GetListOrder(int id);
    OrderItem GetProduct(int orderID, int itemID);

}


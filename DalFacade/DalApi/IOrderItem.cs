using DO;

namespace DalApi;
public interface IOrderItem : ICrud<OrderItem>
{
    List<Product> GetListOrder(int id);
    Product GetProduct(int idOrder, int idItem);

}


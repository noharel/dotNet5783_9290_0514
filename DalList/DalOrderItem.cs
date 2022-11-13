using DalApi;
using DO;


namespace Dal;

public class DalOrderItem:IOrderItem
{
    DataSource _ds = DataSource.s_instance;
    public int Add(OrderItem orderItem)
    {
        if (_ds._orderItems.FirstOrDefault() != null)
            throw new NotImplementedException();
        orderItem.ID = DataSource.config.NextOrderNumber;
        _ds._orderItems.Add(orderItem);
        return orderItem.ID;
    }
    public void Delete(int id)
    {
        if (_ds?._orderItems.RemoveAll(orderItem => orderItem?.ID == id) == 0)
        {
            throw new Exception("Can't delete that does not exist");
        }
    }
    public IEnumerable<OrderItem?> GetAll(Func<OrderItem?, bool>? filter) =>
        (filter == null ?
        _ds?._orderItems.Select(item => item) :
        _ds?._orderItems.Where(filter))
        ?? throw new Exception("Missing orderItem");

    public OrderItem GetById(int id) => _ds._orderItems.FirstOrDefault() ?? throw new Exception("Missing orderItem id");
    public void Update(OrderItem orderItem)
    {
        throw new NotImplementedException();
    }
    public IEnumerable<OrderItem> GetAll()
    {
        return (from OrderItem _orderItems in _ds._orderItems select _orderItems).ToList();
    }

    public List<OrderItem> GetListOrder(int id)
    {
        List<OrderItem> list = new List<OrderItem>();
        foreach (var orderItem in _ds._orderItems)
        {
            if(orderItem.OrderID==id)
            {
                list.Add(orderItem);
            }
        }
        return list;
    }
    OrderItem GetProduct(int orderID, int itemID)
    {
        foreach (var orderItem in _ds._orderItems)
        {
            if ((orderItem.OrderID == orderID)&(orderItem.PrudoctID==itemID))
            {
                return orderItem;
            }
        }
        return new OrderItem();
    }

}

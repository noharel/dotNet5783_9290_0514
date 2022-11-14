using DalApi;
using DO;


namespace Dal;

public class DalOrderItem:IOrderItem
{
    DataSource _ds = DataSource.s_instance;
    public int Add(OrderItem orderItem)
    {
        if (_ds._orderItems == null)
            throw new NotImplementedException();
        //orderItem.ID = DataSource.config.NextOrderNumber;
        _ds._orderItems.Add(orderItem);
        return orderItem.ID;
    }
    public void Delete(int id)
    {
        if (_ds?._orderItems.RemoveAll(orderItem => orderItem.ID == id) == 0)
        {
            throw new Exception("Can't delete that does not exist");
        }
        OrderItem oI = GetById(id);
        oI.IsDeleted = true;

    }
    public IEnumerable<OrderItem> GetAll(Func<OrderItem, bool>? filter) =>
        (filter == null ?
        _ds?._orderItems.Select(item => item) :
        _ds?._orderItems.Where((filter)))
        ?? throw new Exception("Missing orderItem");

    public OrderItem GetById(int id)
    {
        if (_ds._orders == null)
            throw new Exception("Missing order item id");
        foreach (OrderItem oI in _ds._orderItems)
        {
            if (oI.ID == id)
                return oI;
        }
        return new OrderItem();
    }
    public void Update(OrderItem orderItem)
    {
        if (_ds._orderItems == null) throw new NotImplementedException();

        _ds._orderItems.Remove(GetById(orderItem.ID));
        _ds._orderItems.Add(orderItem);

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
   public  OrderItem GetProduct(int orderID, int itemID)
    {
        OrderItem o = new OrderItem();
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

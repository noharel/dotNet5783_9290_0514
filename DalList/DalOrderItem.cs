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
        _ds._orderItems.Add(orderItem);
        return orderItem.ID;
    }
    public void Delete(int id)  //delete
    {
        if (_ds?._orderItems.RemoveAll(orderItem => orderItem.ID == id) == 0) //delete
        {
            throw new Exception("Can't delete that does not exist");
        }
        OrderItem oI = GetById(id);
        oI.IsDeleted = true;  //update IsDelteted

    }
    public IEnumerable<OrderItem> GetAll(Func<OrderItem, bool>? filter) =>
        (filter == null ?
        _ds?._orderItems.Select(item => item) :
        _ds?._orderItems.Where((filter)))
        ?? throw new Exception("Missing orderItem");

    public OrderItem GetById(int id)  //get by id
    {
        if (_ds._orders == null)
            throw new Exception("Missing order item id");
        foreach (OrderItem oI in _ds._orderItems)
        {
            if (oI.ID == id)
                return oI; //return the order item
        }
        return new OrderItem();
    }
    public void Update(OrderItem orderItem) //update
    {
        if (_ds._orderItems == null) throw new NotImplementedException();

        _ds._orderItems.Remove(GetById(orderItem.ID)); //remove the old one
        _ds._orderItems.Add(orderItem); //add the new one

    }
    public IEnumerable<OrderItem> GetAll() //get all order item
    {
        return (from OrderItem _orderItems in _ds._orderItems select _orderItems).ToList();
    }

    public IEnumerable<OrderItem> GetListOrder(int id) //get all the items in the same order
    {
        IEnumerable<OrderItem> list = new List<OrderItem>();
        foreach (var orderItem in _ds._orderItems)
        {
            if(orderItem.OrderID==id)
            {
                list.Add(orderItem); //add the product 
            }
        }
        return list; //return all the products
    }
   public  OrderItem GetProduct(int orderID, int itemID) //get product by order id and item id
    {
        OrderItem o = new OrderItem();
        foreach (var orderItem in _ds._orderItems)
        {
            if ((orderItem.OrderID == orderID)&(orderItem.PrudoctID==itemID))
            {
                return orderItem; //return product
            }
        }

        return new OrderItem();
    }

}

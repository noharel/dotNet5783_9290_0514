using DalApi;
using DO;


namespace Dal;
public class DalOrder : IOrder
{
    DataSource _ds = DataSource.s_instance;
    public int Add(Order order)
    {
        if (_ds._orders == null)
            throw new NotImplementedException();
        _ds._orders.Add(order); 
        return order.ID;
    }
    public void Delete(int id) //delete order
    {
        if (_ds?._orders.RemoveAll(order => order.ID == id) == 0) //delete order
        {
            throw new Exception("Can't delete that does not exist");
        }
        Order O = GetById(id); 
        O.IsDeleted = true; //update the IsDeleted
    }
    public IEnumerable<Order> GetAll(Func<Order, bool>? filter) =>
        (filter == null ?
        _ds?._orders.Select(item => item) :
        _ds?._orders.Where(filter))
        ?? throw new Exception("Missing order");

    public Order GetById(int id)  //get order by id
    {
        if (_ds._orders == null)
            throw new Exception("Missing order id");
        foreach (Order o in _ds._orders)
        {
            if (o.ID == id)
                return o;
        }
        return new Order() { ID=-1};

    }
    public void Update(Order order) //update the orderd
    {
        if (_ds._orders == null) throw new NotImplementedException();

        _ds._orders.Remove(GetById(order.ID)); //remove the old order
        _ds._orders.Add(order);  //add the new order

    }

    public IEnumerable<Order> GetAll()  //get all orders
    {
        return (from Order _orders in _ds._orders select _orders).ToList();
    }

}

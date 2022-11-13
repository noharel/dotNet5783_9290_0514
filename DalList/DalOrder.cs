using DalApi;
using DO;


namespace Dal;
internal class DalOrder : IOrder
{
    DataSource _ds = DataSource.s_instance;
    public int Add(Order order)
    {
        if (_ds._orders.FirstOrDefault() != null)
            throw new NotImplementedException();
        order.ID = DataSource.config.NextOrderNumber;
        _ds._orders.Add(order); 
        return order.ID;
    }
    public void Delete(int id)
    {
        if (_ds?._orders.RemoveAll(order => order?.ID == id) == 0)
        {
            throw new Exception("Can't delete that does not exist");
        }
    }
    public IEnumerable<Order?> GetAll(Func<Order?, bool>? filter) =>
        (filter == null ?
        _ds?._orders.Select(item => item) :
        _ds?._orders.Where(filter))
        ?? throw new Exception("Missing order");

    public Order GetById(int id) => _ds._orders.FirstOrDefault() ?? throw new Exception("Missing order id");
    public void Update(Order order)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Order> GetAll()
    {
        return (from Order _orders in _ds._orders select _orders).ToList();
    }

}

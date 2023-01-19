using DalApi;
using DO;
using System.Reflection.Metadata;

namespace Dal;

//linq done
public class DalOrder : IOrder // A CLASS THAT IMPLEMENTS THE INTERFACE IORDER
{
    DataSource _ds = DataSource.s_instance; // initialize

    /// <summary>
    /// ADD NEW ORDER
    /// </summary>
    /// <param name="order"></param>
    /// <returns></returns>
    /// <exception cref="DoesntExistExeption"></exception>
    public int Add(Order order)
    {
        if (_ds._orders == null) // there are no orders
            throw new DoesntExistExeption("Products list does not exist");

        List<Order?>? orderCheck;

        orderCheck = _ds._orders.Where(item => item?.ID == order.ID).ToList();

        if (orderCheck.Count() == 0) // if there is no order with the same id
        {
            // Add and return
            _ds._orders.Add(order);
            return order.ID;
        }
        else // The ID is in use
            throw new AlreadyExistExeption("The order ID number is already exict");
    }

    /// <summary>
    /// DELETE ORDER
    /// </summary>
    /// <param name="id"></param>
    /// <exception cref="DoesntExistExeption"></exception>
    public void Delete(int id)
    {
        try
        {
            // check if order exict
            Order o = GetById(id);
            o.IsDeleted = true;
        }
        catch (DO.DoesntExistExeption e)
        {
            throw e;
        }
        if (_ds?._orders.RemoveAll(order => order?.ID == id) == 0)
        {
            throw new DoesntExistExeption("can't delete, does not exict");
        }
        //if (_ds._orders.Where(order => order?.ID == id).ToList().Count() == 0) //delete 
        //{
        //    throw new DoesntExistExeption("Can't delete that does not exist");
        //}

        //Order O = GetById(id); 
        //O.IsDeleted = true; // update the IsDeleted
        //_ds._orders.Remove(O);
    }

    /// <summary>
    /// GET ALL ORDERS BY FILTER
    /// </summary>
    /// <param name="filter"></param>
    /// <returns></returns>
    /// <exception cref="DoesntExistExeption"></exception>
    public IEnumerable<Order?> GetAll(Func<Order?, bool>? filter) =>
        (filter == null ?
        _ds?._orders.Select(item => item) :
        _ds?._orders.Where(filter)) // choose only the orders by the filter
        ?? throw new DoesntExistExeption("Missing order");

    /// <summary>
    /// GET ORDER BY FILTER
    /// </summary>
    /// <param name="filter"></param>
    /// <returns></returns>
    /// <exception cref="DoesntExistExeption"></exception>
    public Order? GetById(Func<Order?, bool>? filter) =>
        (filter == null ?
        _ds?._orders.Select(item => item).FirstOrDefault() :
        _ds?._orders.Where(filter).FirstOrDefault()) // choose the order by the filter
        ?? throw new DoesntExistExeption("Missing order");

    /// <summary>
    /// GET ORDER BY ID
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    /// <exception cref="DoesntExistExeption"></exception>
    public Order GetById(int id)
    {
        if (_ds._orders == null)
            throw new DoesntExistExeption("Missing order id");
        return _ds!._orders.Where(o => o?.ID == id).FirstOrDefault() //choose the order by the id
            ?? throw new DoesntExistExeption("Missing order id");

    }

    /// <summary>
    /// UPDATE ORDER INFORMATION
    /// </summary>
    /// <param name="order"></param>
    /// <exception cref="DoesntExistExeption"></exception>
    public void Update(Order order)
    {
        if (_ds._orders == null) throw new DoesntExistExeption("Products list does not exist");

        _ds._orders.Remove(GetById(order.ID)); // remove the old order
        _ds._orders.Add(order);  // add the new order

    }

    /// <summary>
    /// GET ALL ORDERS
    /// </summary>
    /// <returns></returns>
    public IEnumerable<Order?> GetAll()
    {
        IEnumerable<Order?> list = (from Order? _orders in _ds._orders select _orders).ToList();
        if (list == null)
            new DoesntExistExeption("Missing order");
        return list!; // return the orders
    }

    /// <summary>
    /// no need 
    /// </summary>
    /// <param name="asked"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public int getRunningId(string asked)
    {
        if (asked == "OrderID")
        {
            return _ds._orders.LastOrDefault()!.Value.ID +1;
            
        }
        if (asked == "OrderItemID")
        {
            return _ds._orderItems.LastOrDefault()!.Value.ID + 1;
        }
        return 0;
    }
}

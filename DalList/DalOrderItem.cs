using DalApi;
using DO;


namespace Dal;

//linq done
public class DalOrderItem: IOrderItem  // A CLASS THAT IMPLEMENTS THE INTERFACE IOrderItem
{
    DataSource _ds = DataSource.s_instance; //initialize

    /// <summary>
    /// ADD ORDER ITEM
    /// </summary>
    /// <param name="orderItem"></param>
    /// <returns></returns>
    /// <exception cref="DoesntExistExeption"></exception>
    public int Add(OrderItem orderItem) 
    { 
        if (_ds._orderItems == null) // there are no order items
            throw new DoesntExistExeption("Products list does not exist");

        List<OrderItem?>? orderitemCheck;
        orderitemCheck = _ds._orderItems.Where(item => item?.ID == orderItem.ID).ToList(); // get the order item with the same id

       
        if (orderitemCheck.Count() == 0) // if there is no order with the same id
        {
            // Add and return
            _ds._orderItems.Add(orderItem);
            return orderItem.ID;
        }
        else // The ID is in use
            throw new AlreadyExistExeption("The order item ID number is already exict");
    }

    /// <summary>
    /// DELETE ORDER ITEM
    /// </summary>
    /// <param name="id"></param>
    /// <exception cref="DoesntExistExeption"></exception>
    public void Delete(int id)  
    {
        if (_ds._orderItems.RemoveAll(orderItem => orderItem?.ID == id) == 0) //delete
        {
            throw new DoesntExistExeption("Can't delete that does not exist");
        }
        OrderItem oI = GetById(id);
        oI.IsDeleted = true;  //update IsDelteted

    }

    /// <summary>
    /// GET ALL ORDER ITEMS BY FILTER
    /// </summary>
    /// <param name="filter"></param>
    /// <returns></returns>
    /// <exception cref="DoesntExistExeption"></exception>
    public IEnumerable<OrderItem?> GetAll(Func<OrderItem?, bool>? filter) =>
        (filter == null ?
        _ds?._orderItems.Select(item => item) :
        _ds?._orderItems.Where((filter))) // choose by the filter
        ?? throw new DoesntExistExeption("Missing orderItem");

    /// <summary>
    /// GET ORDER ITEM BY FILTER
    /// </summary>
    /// <param name="filter"></param>
    /// <returns></returns>
    /// <exception cref="DoesntExistExeption"></exception>
    public OrderItem? GetById(Func<OrderItem?, bool>? filter) =>
     (filter == null ?
     _ds?._orderItems.Select(item => item).FirstOrDefault() :
     _ds?._orderItems.Where(filter).FirstOrDefault()) // choose cy the filter
     ?? throw new DoesntExistExeption("Missing orderItem");

    /// <summary>
    /// GET ORDER ITEM BY ID
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    /// <exception cref="DoesntExistExeption"></exception>
    public OrderItem GetById(int id)  
    {
        if (_ds?._orders == null)
            throw new DoesntExistExeption("Missing order item id");
        return _ds._orderItems.Where(oI => oI?.ID == id).FirstOrDefault() //chhose order item by id
            ?? throw new DoesntExistExeption("Missing order item id");
    
    }

    /// <summary>
    /// UPDATE ORDER ITEM
    /// </summary>
    /// <param name="orderItem"></param>
    /// <exception cref="DoesntExistExeption"></exception>
    public void Update(OrderItem orderItem) 
    {
        if (_ds._orderItems == null) throw new DoesntExistExeption("Products list does not exist");

        _ds._orderItems.Remove(GetById(orderItem.ID)); //remove the old one
        _ds._orderItems.Add(orderItem); //add the new one

    }

    /// <summary>
    /// GET ALL ORDER ITEMS
    /// </summary>
    /// <returns></returns>
    public IEnumerable<OrderItem?> GetAll() 
    {
        IEnumerable<OrderItem?> list = (from OrderItem? _orderItems in _ds._orderItems select _orderItems).ToList();
        if (list == null) // there are on order items
            throw new DoesntExistExeption("Missing order item");
        else
            return list; //return all the orderitems
        
    }

    /// <summary>
    /// GET LIST ORDER - RETURN ALL THE ITEMS IN THE SAME ORDER
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public IEnumerable<OrderItem?> GetListOrder(int id) 
    {
        return _ds._orderItems.Where(orderItem => orderItem?.OrderID == id);//retruns a list of products with the id
       
    }

    /// <summary>
    /// GET ITEM FROM ORDER, BY ID
    /// </summary>
    /// <param name="orderID"></param>
    /// <param name="itemID"></param>
    /// <returns></returns>
   public  OrderItem? GetProduct(int orderID, int itemID)
    {
        //return the order item with the order id and product id wanted
        return _ds._orderItems.Where(orderItem => ((orderItem?.OrderID == orderID) && (orderItem?.PrudoctID == itemID))).FirstOrDefault() ?? throw new DoesntExistExeption("order item does not exict in this order");
        
    }

}

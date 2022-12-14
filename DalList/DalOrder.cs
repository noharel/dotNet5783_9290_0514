﻿using DalApi;
using DO;


namespace Dal;


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
        _ds._orders.Add(order); // add
        return order.ID; 
    }

    /// <summary>
    /// DELETE ORDER
    /// </summary>
    /// <param name="id"></param>
    /// <exception cref="DoesntExistExeption"></exception>
    public void Delete(int id) 
    {
        if (_ds._orders.RemoveAll(order => order?.ID == id) == 0) //delete 
        {
            throw new DoesntExistExeption("Can't delete that does not exist");
        }
        Order O = GetById(id); 
        O.IsDeleted = true; // update the IsDeleted
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
    public Order? GetById(Func<Order?, bool>? filter)=>
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
        foreach (Order? o in _ds._orders) // find the order with the same id
        {
            if (o?.ID == id)
                return (Order)o; // return the order
        }

        throw new DoesntExistExeption("Missing order id");
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
        IEnumerable<Order?> list=(from Order? _orders in _ds._orders select _orders).ToList();
        if (list == null)
            new DoesntExistExeption("Missing order");
        return list!; // return the orders
    }

}

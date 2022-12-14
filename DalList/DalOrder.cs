﻿using DalApi;
using DO;


namespace Dal;
public class DalOrder : IOrder
{
    DataSource _ds = DataSource.s_instance;
    public int Add(Order order)
    {
        if (_ds._orders == null)
            throw new DoesntExistExeption("Products list does not exist");
        _ds._orders.Add(order); 
        return order.ID;
    }
    public void Delete(int id) //delete order
    {
        if (_ds._orders.RemoveAll(order => order?.ID == id) == 0) //delete order
        {
            throw new DoesntExistExeption("Can't delete that does not exist");
        }
        Order O = GetById(id); 
        O.IsDeleted = true; //update the IsDeleted
    }
    public IEnumerable<Order?> GetAll(Func<Order?, bool>? filter) =>
        (filter == null ?
        _ds?._orders.Select(item => item) :
        _ds?._orders.Where(filter))
        ?? throw new DoesntExistExeption("Missing order");

    public Order? GetById(Func<Order?, bool>? filter)=>
        (filter == null ?
        _ds?._orders.Select(item => item).FirstOrDefault() :
        _ds?._orders.Where(filter).FirstOrDefault())
        ?? throw new DoesntExistExeption("Missing order");

    public Order GetById(int id)  //get order by id
    {
        if (_ds._orders == null)
            throw new DoesntExistExeption("Missing order id");
        foreach (Order? o in _ds._orders)
        {
            if (o?.ID == id)
                return (Order)o;
        }

        throw new DoesntExistExeption("Missing order id");
    }
    public void Update(Order order) //update the orderd
    {
        if (_ds._orders == null) throw new DoesntExistExeption("Products list does not exist");

        _ds._orders.Remove(GetById(order.ID)); //remove the old order
        _ds._orders.Add(order);  //add the new order

    }

    public IEnumerable<Order?> GetAll()  //get all orders
    {
        IEnumerable<Order?> list=(from Order? _orders in _ds._orders select _orders).ToList();
        if (list == null) new DoesntExistExeption("Missing order");
        return list!;
    }

}

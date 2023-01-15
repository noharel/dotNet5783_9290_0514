using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Xml.Serialization;

using DalApi;
using DO;

namespace Dal;

internal class Order : IOrder
{
    //DalApi.IDal Dal = DalApi.Factory.Get(); //DalList object Type

    const string s_orders = "orders"; //XML Serializer

    public IEnumerable<DO.Order?> GetAll(Func<DO.Order?, bool>? filter = null) //get all orders by filter
    {
        var listOrders = XMLTools.LoadListFromXMLSerializer<DO.Order>(s_orders)!; //get all orders from xml
        return filter == null ? listOrders.OrderBy(o => ((DO.Order)o!).ID)
                              : listOrders.Where(filter).OrderBy(o => ((DO.Order)o!).ID);
    }

    public int Add(DO.Order order) // add an order
    {
    
        var listOrders = XMLTools.LoadListFromXMLSerializer<DO.Order>(s_orders); // get all orders from xml

        if (listOrders.Exists(o => o?.ID == order.ID)) // id already exist
            throw new DO.AlreadyExistExeption("id already exist");

        listOrders.Add(order); //add

        XMLTools.SaveListToXMLSerializer(listOrders, s_orders); //to xml

        return order.ID;
    }

    public void Delete(int id) // delete an order
    {
        var listOrders = XMLTools.LoadListFromXMLSerializer<DO.Order>(s_orders); // get all orders from xml

        if (listOrders.RemoveAll(p => p?.ID == id) == 0) // delete
            throw new DO.DoesntExistExeption("missing id");

        XMLTools.SaveListToXMLSerializer(listOrders, s_orders); //save at xml
    }

   

    public IEnumerable<DO.Order?> GetAll() // get all orders
    {
        var listOrders = XMLTools.LoadListFromXMLSerializer<DO.Order>(s_orders)!;
        return listOrders.OrderBy(o => ((DO.Order)o!).ID);
    }

    public DO.Order GetById(int id) // get order by id
    {
        return XMLTools.LoadListFromXMLSerializer<DO.Order>(s_orders).FirstOrDefault(o => o?.ID == id) 
            ?? throw new DO.DoesntExistExeption("missing id");
    }



    public void Update(DO.Order order) // update an order
    {
        Delete(order.ID);
        Add(order);
    }

    public DO.Order? GetById(Func<DO.Order?, bool>? filter) // get an order by filter
    {

        var listOrders = XMLTools.LoadListFromXMLSerializer<DO.Order>(s_orders)!;
        return listOrders.Where(filter!).OrderBy(o => ((DO.Order)o!).ID).FirstOrDefault();
    }
}

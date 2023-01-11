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

    public IEnumerable<DO.Order?> GetAll(Func<DO.Order?, bool>? filter = null)
    {
        var listOrders = XMLTools.LoadListFromXMLSerializer<DO.Order>(s_orders)!;
        return filter == null ? listOrders.OrderBy(o => ((DO.Order)o!).ID)
                              : listOrders.Where(filter).OrderBy(o => ((DO.Order)o!).ID);
    }

    public int Add(DO.Order order)
    {
    
        var listLecturers = XMLTools.LoadListFromXMLSerializer<DO.Order>(s_orders);

        if (listLecturers.Exists(o => o?.ID == order.ID))
            throw new DO.AlreadyExistExeption("id already exist");

        listLecturers.Add(order);

        XMLTools.SaveListToXMLSerializer(listLecturers, s_orders);

        return order.ID;
    }

    public void Delete(int id)
    {
        var listOrders = XMLTools.LoadListFromXMLSerializer<DO.Order>(s_orders);

        if (listOrders.RemoveAll(p => p?.ID == id) == 0)
            throw new DO.DoesntExistExeption("missing id");

        XMLTools.SaveListToXMLSerializer(listOrders, s_orders);
    }

   

    public IEnumerable<DO.Order?> GetAll()
    {
        var listOrders = XMLTools.LoadListFromXMLSerializer<DO.Order>(s_orders)!;
        return listOrders.OrderBy(o => ((DO.Order)o!).ID);
    }

    public DO.Order GetById(int id)
    {
        return XMLTools.LoadListFromXMLSerializer<DO.Order>(s_orders).FirstOrDefault(o => o?.ID == id) 
            ?? throw new DO.DoesntExistExeption("missing id");
    }



    public void Update(DO.Order order)
    {
        Delete(order.ID);
        Add(order);
    }

    public DO.Order? GetById(Func<DO.Order?, bool>? filter)
    {

        var listOrders = XMLTools.LoadListFromXMLSerializer<DO.Order>(s_orders)!;
        return listOrders.Where(filter!).OrderBy(o => ((DO.Order)o!).ID).FirstOrDefault();
    }
}

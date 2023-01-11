using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using DalApi;
using DO;

namespace Dal;

internal class Order : IOrder
{
    const string s_orders = "orders"; //XML Serializer

    public IEnumerable<DO.Order?> GetAll(Func<DO.Order?, bool>? filter = null)
    {
        var listOrders = XMLTools.LoadListFromXMLSerializer<DO.Order>(s_orders)!;
        return filter == null ? listOrders.OrderBy(lec => ((DO.Order)lec!).ID)
                              : listOrders.Where(filter).OrderBy(lec => ((DO.Order)lec!).ID);
    }

    DalApi.IDal Dal = DalApi.Factory.Get(); //DalList object Type
    public int Add(DO.Order item)
    {
        int id = Dal.Order.Add(item);
        return Dal.Order.Add(item);
    }

    public void Delete(int id)
    {
        
    }

   

    public IEnumerable<DO.Order?> GetAll()
    {
        throw new NotImplementedException();
    }

    public DO.Order GetById(int id)
    {
        throw new NotImplementedException();
    }

    public DO.Order? GetById(Func<DO.Order?, bool>? filter)
    {
        throw new NotImplementedException();
    }

    public void Update(DO.Order item)
    {
        throw new NotImplementedException();
    }
}

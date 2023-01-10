using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using DalApi;
using DO;

namespace Dal;

internal class Order : IOrder
{
    DalApi.IDal Dal = DalApi.Factory.Get(); //DalList object Type
    public int Add(DO.Order item)
    {
        int id = Dal.Order.Add(item);
        return Dal.Order.Add(item);
    }

    public void Delete(int id)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<DO.Order?> GetAll(Func<DO.Order?, bool>? filter = null)
    {
        throw new NotImplementedException();
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

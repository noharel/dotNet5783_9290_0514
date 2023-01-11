using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DO;
using DalApi;

namespace Dal;

internal class OrderItem : IOrderItem
{
    const string s_orderItems = "orderItems"; //XML Serializer
    public int Add(DO.OrderItem orderItem)
    {
        var listOrderItems = XMLTools.LoadListFromXMLSerializer<DO.OrderItem>(s_orderItems);

        if (listOrderItems.Exists(o => o?.ID == orderItem.ID))
            throw new DO.AlreadyExistExeption("id already exist");

        listOrderItems.Add(orderItem);

        XMLTools.SaveListToXMLSerializer(listOrderItems, s_orderItems);

        return orderItem.ID;
    }

    public void Delete(int id)
    {
        var listOrderItems = XMLTools.LoadListFromXMLSerializer<DO.OrderItem>(s_orderItems);

        if (listOrderItems.RemoveAll(p => p?.ID == id) == 0)
            throw new DO.DoesntExistExeption("missing id");

        XMLTools.SaveListToXMLSerializer(listOrderItems, s_orderItems);
    }

    public IEnumerable<DO.OrderItem?> GetAll(Func<DO.OrderItem?, bool>? filter = null)
    {
        var listOrderItems = XMLTools.LoadListFromXMLSerializer<DO.OrderItem>(s_orderItems)!;
        return filter == null ? listOrderItems.OrderBy(o => ((DO.OrderItem)o!).ID)
                              : listOrderItems.Where(filter).OrderBy(o => ((DO.OrderItem)o!).ID);
    }

    public IEnumerable<DO.OrderItem?> GetAll()
    {
        var listOrderItems = XMLTools.LoadListFromXMLSerializer<DO.OrderItem>(s_orderItems)!;
        return listOrderItems.OrderBy(o => ((DO.OrderItem)o!).ID);

    }

    public DO.OrderItem GetById(int id)
    {

        return XMLTools.LoadListFromXMLSerializer<DO.OrderItem>(s_orderItems).FirstOrDefault(o => o?.ID == id)
            ?? throw new DO.DoesntExistExeption("missing id");
    }

    public DO.OrderItem? GetById(Func<DO.OrderItem?, bool>? filter)
    {
        var listOrderItems = XMLTools.LoadListFromXMLSerializer<DO.OrderItem>(s_orderItems)!;
        return listOrderItems.Where(filter!).OrderBy(o => ((DO.OrderItem)o!).ID).FirstOrDefault();
    }

    public void Update(DO.OrderItem orderItem)
    {
        Delete(orderItem.ID);
        Add(orderItem);
    }

    public IEnumerable<DO.OrderItem?> GetListOrder(int id)
        // GET ALL THE ITEMS IN THE SAME ORDER
    {
        var listOrderItems = XMLTools.LoadListFromXMLSerializer<DO.OrderItem>(s_orderItems)!;
        return listOrderItems.Where(o => o?.OrderID == id) 
            ?? throw new DO.DoesntExistExeption("there are no order items");
    }

    public DO.OrderItem? GetProduct(int orderID, int itemID)
        // GET AN ITEM FROM THE ORDER
    {

        var listOrderItems = XMLTools.LoadListFromXMLSerializer<DO.OrderItem>(s_orderItems)!;
        return listOrderItems.Where(o => ((o?.OrderID == orderID) && (o?.PrudoctID == itemID))).FirstOrDefault() 
            ?? throw new DoesntExistExeption("order item does not exict in this order");

    }


}

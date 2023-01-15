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
    const string s_orderItems = "OrderItem"; //XML Serializer
    public int Add(DO.OrderItem orderItem) // add an order item
    {
        var listOrderItems = XMLTools.LoadListFromXMLSerializer<DO.OrderItem>(s_orderItems); // get all order items from xml

        if (listOrderItems.Exists(o => o?.ID == orderItem.ID)) // id already exist
            throw new DO.AlreadyExistExeption("id already exist");

        listOrderItems.Add(orderItem); // add to list

        XMLTools.SaveListToXMLSerializer(listOrderItems, s_orderItems); // save to xml

        return orderItem.ID;
    }

    public void Delete(int id) // delete an order item
    {
        var listOrderItems = XMLTools.LoadListFromXMLSerializer<DO.OrderItem>(s_orderItems); // get from xml

        if (listOrderItems.RemoveAll(p => p?.ID == id) == 0) // delete
            throw new DO.DoesntExistExeption("missing id");

        XMLTools.SaveListToXMLSerializer(listOrderItems, s_orderItems); // save to xml
    }

    public IEnumerable<DO.OrderItem?> GetAll(Func<DO.OrderItem?, bool>? filter = null)  // get all by filter
    {
        var listOrderItems = XMLTools.LoadListFromXMLSerializer<DO.OrderItem>(s_orderItems)!;
        return filter == null ? listOrderItems.OrderBy(o => ((DO.OrderItem)o!).ID)
                              : listOrderItems.Where(filter).OrderBy(o => ((DO.OrderItem)o!).ID);
    }

    public IEnumerable<DO.OrderItem?> GetAll() // get all order items
    {
        var listOrderItems = XMLTools.LoadListFromXMLSerializer<DO.OrderItem>(s_orderItems)!;
        return listOrderItems.OrderBy(o => ((DO.OrderItem)o!).ID);

    }

    public DO.OrderItem GetById(int id) // get an order item by filter
    {

        return XMLTools.LoadListFromXMLSerializer<DO.OrderItem>(s_orderItems).FirstOrDefault(o => o?.ID == id)
            ?? throw new DO.DoesntExistExeption("missing id");
    }

    public DO.OrderItem? GetById(Func<DO.OrderItem?, bool>? filter)
    {
        var listOrderItems = XMLTools.LoadListFromXMLSerializer<DO.OrderItem>(s_orderItems)!;
        return listOrderItems.Where(filter!).OrderBy(o => ((DO.OrderItem)o!).ID).FirstOrDefault();
    }

    public void Update(DO.OrderItem orderItem) // update an order item
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

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlApi;
using BO;
using DO;

namespace BlImplementation;

/// <summary>
/// AN ORDER CLASS THAT INMPLEMENTS AN INTERFACE
/// </summary>
internal class Order : BlApi.IOrder
{
    DalApi.IDal Dal = DalApi.Factory.Get(); // DalList object Type

    /// <summary>
    /// GET LIST OF ALL THE ORDERS - FOR MANAGER
    /// </summary>
    /// <returns></returns>
    /// <exception cref="BO.DoesntExistExeption"></exception>
    public IEnumerable<BO.OrderForList> GetOrders()
    {
        IEnumerable<DO.Order?> orderList;
        List<BO.OrderForList> orderForList = new List<BO.OrderForList>();
        try
        {
            orderList = Dal.Order.GetAll(); // GET ALL THE ORDERS FROM DAL
            foreach (DO.Order? var in orderList) // GO THROWGH ALL THE ORDERS
            {
                BO.OrderStatus stat = 0;
                if (var?.ShipDate != null) // ORDER SHIPED
                    stat = (BO.OrderStatus)1;

                if (var?.DeliveryrDate != null) // ORDER DELIVERED
                    stat = (BO.OrderStatus)2;

                int id = (int)(var?.ID)!; 
                IEnumerable<DO.OrderItem?> listOrderItem = Dal.OrderItem.GetListOrder(id); // GET ALL ITEMS IN SAME ORDER
                double price = 0;
                foreach (DO.OrderItem? orderItem in listOrderItem)
                {
                    price += (double)(orderItem?.Price * orderItem?.Amount)!; // CALCULATE THE TOTAL PRICE FOR EACH ORDER
                }
                // MAKES THE NEW ORDER
                BO.OrderForList newOrderForList = new BO.OrderForList { ID =id, CustomerName = var?.CustomerName, Status = stat, AmountOfItems = listOrderItem.Count(), TotalPrice = price };
                orderForList.Add(newOrderForList); // ADD THE NEW ORDER TO THE LIST
            }
            return orderForList; // RETURNS THE LIST WITH ALL THE ORDERS
        }
        catch (DO.DoesntExistExeption e) // CATCH GETALL FUNCTION EXCEPTION
        {
            throw new BO.DoesntExistExeption("Couldn't get list of orders", e);
        }
    }

    /// <summary>
    /// GET ORDER INFORMATION
    /// </summary>
    /// <param name="orderId"></param>
    /// <returns></returns>
    /// <exception cref="BO.DoesntExistExeption"></exception>
    /// <exception cref="BO.InvalidInputExeption"></exception>
    public BO.Order OrderInfo(int orderId)
    {
        if ((orderId >= 0)&& (orderId.ToString().Length == 4))  // VALID ORDER ID
        {
            try
            {
                DO.Order newOrder = Dal.Order.GetById(orderId); // GET THE ORDER
                try
                {
                    IEnumerable<DO.OrderItem?> listOrder = Dal.OrderItem.GetListOrder(orderId); // GET ALL THE ITEMS IN THE ORDER
                    List<BO.OrderItem> BOlistOrder = new List<BO.OrderItem>();
                    double totalPriceOrder = 0;
                    foreach (DO.OrderItem? item in listOrder) // GO THROWGH ALL THE ITEMS
                    {
                        // MAKES ITEM
                        BO.OrderItem newOrderItem = new BO.OrderItem() { ID = (int)item?.ID!, Amount = (int)item?.Amount!, Name = newOrder.CustomerName, Price = (double)item?.Price!, ProductID = (int)item?.PrudoctID!, TotalPrice =(int)( item?.Amount * item?.Price)! };
                        totalPriceOrder += (double)(item?.Price*item?.Amount)!; // CALCULATE THE TOTAL PRICE

                        BOlistOrder.Add(newOrderItem); // ADD THE ITEM
                    }
                    BO.OrderStatus status = BO.OrderStatus.ordered;
                    if (newOrder.ShipDate != null)
                        status = BO.OrderStatus.shipped;
                    if (newOrder.DeliveryrDate != null)
                        status = BO.OrderStatus.arrived;
                    return new BO.Order() { Items = BOlistOrder, ID = newOrder.ID, CustomerAddress = newOrder.CustomerAddress, CustomerEmail = newOrder.CustomerEmail, ShipDate = newOrder.ShipDate, OrderDate = newOrder.OrderDate, DeliveryrDate = newOrder.DeliveryrDate, CustomerName = newOrder.CustomerName, TotalPrice = totalPriceOrder, Status = status };
                }
                catch (DO.DoesntExistExeption e) // CATCH GETLISTORDER FUNCTION EXCPETION 
                {
                    throw new BO.DoesntExistExeption("Couldn't Get order info",e);
                }

            }
            catch (DO.DoesntExistExeption e) // CATCH GETBYID FUNCTION EXCEPTION
            {
                throw new BO.DoesntExistExeption("couldn't find order", e);
            }
        }
        else //INVALID ORDER ID
        {
            throw new BO.InvalidInputExeption("Invalid order Id");
        }
    }

    public BO.Order UpdateShip(int orderId)
    {

        
        try
        {
            DO.Order DOorder = Dal.Order.GetById(orderId);
            BO.Order BOorder;

            if ((DOorder.ID >= 0) && (((DOorder.ID).ToString()).Length == 4))
            {
                if (DOorder.ShipDate == null)
                {
                    List<BO.OrderItem> BOlistOrder = new List<BO.OrderItem>();
                    IEnumerable<DO.OrderItem?> listOrder = Dal.OrderItem.GetListOrder(orderId);

                    double totalPriceOrder = 0;
                    foreach (DO.OrderItem? item in listOrder)
                    {
                        BO.OrderItem newOrderItem = new BO.OrderItem() { ID = (int)item?.ID!, Amount = (int)item?.Amount!, Name = DOorder.CustomerName, Price = (double)item?.Price!, ProductID = (int)item?.PrudoctID!, TotalPrice = (double)(item?.Amount * item?.Price)! };
                        BOlistOrder.Add(newOrderItem);
                        totalPriceOrder += newOrderItem.TotalPrice;

                    }
                    DOorder.ShipDate = DateTime.Now;
                    BOorder = new BO.Order() { ID = DOorder.ID, ShipDate = DOorder.ShipDate, CustomerAddress = DOorder.CustomerAddress, CustomerEmail = DOorder.CustomerEmail, CustomerName = DOorder.CustomerName, DeliveryrDate = DOorder.DeliveryrDate, Items = BOlistOrder, TotalPrice = totalPriceOrder, OrderDate = DOorder.OrderDate, Status = OrderStatus.shipped };
                    try
                    {
                        Dal.Order.Update(DOorder);
                        return BOorder;
                    }
                    catch (DO.DoesntExistExeption e)
                    {
                        throw new BO.DoesntExistExeption("coudln't update", e);
                    }
                }
                else
                {
                    throw new ContradictoryDataExeption("already shipped");
                }
            }
            else
            {
                throw new BO.DoesntExistExeption("order does not exsist");
            }
        }
        catch (DO.DoesntExistExeption e)
        { 
            throw new BO.DoesntExistExeption("couldn't get order", e); 
        }
    }
    public BO.Order UpdateDelivery(int orderId)
    {
        
        try
        {
            DO.Order DOorder = Dal.Order.GetById(orderId);
            BO.Order BOorder;

            if ((DOorder.ID >= 0) && (((DOorder.ID).ToString()).Length == 4))//if order exsist
            {
                if ((DOorder.ShipDate != null) && (DOorder.DeliveryrDate == null))
                {
                    List<BO.OrderItem> BOlistOrder = new List<BO.OrderItem>();
                    IEnumerable<DO.OrderItem?> listOrder = Dal.OrderItem.GetListOrder(orderId);

                    double totalPriceOrder = 0;
                    foreach (DO.OrderItem? item in listOrder)
                    {
                        BO.OrderItem newOrderItem = new BO.OrderItem() { ID = (int)item?.ID!, Amount = (int)item?.Amount!, Name = DOorder.CustomerName, Price = (double)item?.Price!, ProductID = (int)item?.PrudoctID!, TotalPrice = (double)(item?.Amount * item?.Price)! };
                        BOlistOrder.Add(newOrderItem);
                        totalPriceOrder += newOrderItem.TotalPrice;

                    }
                    DOorder.DeliveryrDate = DateTime.Now;
                    BOorder = new BO.Order() { ID = DOorder.ID, ShipDate = DOorder.ShipDate, CustomerAddress = DOorder.CustomerAddress, CustomerEmail = DOorder.CustomerEmail, CustomerName = DOorder.CustomerName, DeliveryrDate = DOorder.DeliveryrDate, Items = BOlistOrder, TotalPrice = totalPriceOrder, OrderDate = DOorder.OrderDate, Status = OrderStatus.arrived };
                    try
                    {
                        Dal.Order.Update(DOorder);
                        return BOorder;
                    }
                    catch (DO.DoesntExistExeption e)
                    {
                        throw new BO.DoesntExistExeption("couldn't find", e);
                    }
                }
                else
                {
                    throw new BO.ContradictoryDataExeption("order already deliverd or not shipped yet");
                }
            }
            else
            {
                throw new BO.DoesntExistExeption("order does not exsist");
            }
        }
        catch (DO.DoesntExistExeption e)
        {
            throw new BO.DoesntExistExeption("couldn't get order", e);
        }

    }
    public BO.OrderTracking TrackingOrder(int orderId)
    {
        try
        {
            DO.Order DOorder = Dal.Order.GetById(orderId);
            if ((DOorder.ID >= 0) && ((((DOorder.ID).ToString()).Length == 4)))//if order exist
            {
                List<(DateTime?, string)> tupList = new List<(DateTime?, string)>();
                BO.OrderStatus status = BO.OrderStatus.ordered;
                tupList.Add((DOorder.OrderDate, "Order was placed"));
                if (DOorder.ShipDate != null)
                {
                    status = BO.OrderStatus.shipped;
                    tupList.Add((DOorder.ShipDate, "Order was shipped"));

                }
                if (DOorder.DeliveryrDate != null)
                {
                    status = BO.OrderStatus.arrived;
                    tupList.Add((DOorder.DeliveryrDate, "Order was arrived"));
                }
                BO.OrderTracking orderTracking = new BO.OrderTracking() { ID = orderId, Status = status, tuplesList = tupList! };
                return orderTracking;
            }
            else
            {
                throw new BO.DoesntExistExeption("order does not exsist");
            }
        }
        catch (DO.DoesntExistExeption e)
        {
            throw new BO.DoesntExistExeption("couldn't get order", e);
        }

    }
    public void UpdateByManager(int orderID, int orderItemId, int amount )//bonus
    {
        try
        {
            DO.Order DOorder = Dal.Order.GetById(orderID);
            if ((DOorder.ID >= 0) && ((((DOorder.ID).ToString()).Length == 4)))//if order exist
            {
                if (DOorder.ShipDate == null)
                {
                    DO.OrderItem DOorderItem = (DO.OrderItem)Dal.OrderItem.GetProduct(orderID, orderItemId)!;
                    DOorderItem.Amount += amount;
                    Dal.OrderItem.Update((DO.OrderItem)DOorderItem);
                    //we're not sure if instock needs to be updated
                }
                else
                {
                    throw new BO.ContradictoryDataExeption("Order was already shipped, can not update it");
                }
            }
            else
            {
                throw new BO.DoesntExistExeption("order does not exsist");
            }
        }
        catch (DO.DoesntExistExeption e)
        {
            throw new BO.DoesntExistExeption("couldn't get order", e);
        }

    }
}

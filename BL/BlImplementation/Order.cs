using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlApi;

namespace BlImplementation;

internal class Order : BlApi.IOrder
{


    DalApi.IDal Dal = DalApi.Factory.Get();
    public IEnumerable<BO.OrderForList> GetOrders()
    {
        IEnumerable<DO.Order> orderList;
        IEnumerable<BO.OrderForList> orderForList = new List<BO.OrderForList>();
        orderList = Dal.Order.GetAll();
        foreach (DO.Order var in orderList)
        {
            BO.OrderStatus stat = (BO.OrderStatus)0;
            if (var.ShipDate != DateTime.MinValue)
                stat = (BO.OrderStatus)1;

            if (var.DeliveryrDate != DateTime.MinValue)
                stat = (BO.OrderStatus)2;

            IEnumerable<DO.OrderItem> listOrderItem = Dal.OrderItem.GetListOrder(var.ID);
            double price = 0;
            foreach (DO.OrderItem orderItem in listOrderItem)
            {
                price += orderItem.Price;
            }
            BO.OrderForList newOrderForList = new BO.OrderForList { ID = var.ID, CustomerName = var.CustomerName, Status = stat, AmountOfItems = listOrderItem.Count(), TotalPrice = price };
            orderForList.Append(newOrderForList);
        }
        return orderForList;
    }
    public BO.Order OrderInfo(int orderId)
    {
        if (orderId > 0)
        {
            try
            {
                DO.Order newOrder = Dal.Order.GetById(orderId);
                try
                {
                    IEnumerable<DO.OrderItem> listOrder = Dal.OrderItem.GetListOrder(orderId);
                    IEnumerable<BO.OrderItem> BOlistOrder = new List<BO.OrderItem>();
                    double totalPriceOrder = 0;
                    foreach (DO.OrderItem item in listOrder)
                    {
                        BO.OrderItem newOrderItem = new BO.OrderItem() { ID = item.ID, Amount = item.Amount, Name = newOrder.CustomerName, Price = item.Price, ProductID = item.PrudoctID, TotalPrice = item.Amount * item.Price };
                        BOlistOrder.Append(newOrderItem);
                        totalPriceOrder += newOrderItem.TotalPrice;

                    }
                    BO.OrderStatus status = BO.OrderStatus.ordered;
                    if (newOrder.ShipDate != null)
                        status = BO.OrderStatus.shipped;
                    if (newOrder.DeliveryrDate != null)
                        status = BO.OrderStatus.arrived;
                    return new BO.Order() { Items = BOlistOrder, ID = newOrder.ID, CustomerAddress = newOrder.CustomerAddress, CustomerEmail = newOrder.CustomerEmail, ShipDate = newOrder.ShipDate, OrderDate = newOrder.OrderDate, DeliveryrDate = newOrder.DeliveryrDate, CustomerName = newOrder.CustomerName, TotalPrice = totalPriceOrder, Status = status };
                }
                catch (Exception e)
                {
                    throw e;
                }

            }
            catch (Exception e)
            {
                throw e;
            }
        }
        else
        {
            throw new Exception("Invalid order Id");
        }
    }
    public BO.Order UpdateShip(int orderId)
    {

        DO.Order DOorder = Dal.Order.GetById(orderId);
        BO.Order BOorder;

        if (DOorder.ID > 0)
        {
            if (DOorder.ShipDate == null)
            {
                IEnumerable<BO.OrderItem> BOlistOrder = new List<BO.OrderItem>();
                IEnumerable<DO.OrderItem> listOrder = Dal.OrderItem.GetListOrder(orderId);

                double totalPriceOrder = 0;
                foreach (DO.OrderItem item in listOrder)
                {
                    BO.OrderItem newOrderItem = new BO.OrderItem() { ID = item.ID, Amount = item.Amount, Name = DOorder.CustomerName, Price = item.Price, ProductID = item.PrudoctID, TotalPrice = item.Amount * item.Price };
                    BOlistOrder.Append(newOrderItem);
                    totalPriceOrder += newOrderItem.TotalPrice;

                }
                DOorder.ShipDate = DateTime.Now;
                BOorder = new BO.Order() { ID = DOorder.ID, ShipDate = DOorder.ShipDate, CustomerAddress = DOorder.CustomerAddress, CustomerEmail = DOorder.CustomerEmail, CustomerName = DOorder.CustomerName, DeliveryrDate = DOorder.DeliveryrDate, Items = BOlistOrder, TotalPrice = totalPriceOrder, OrderDate = DOorder.OrderDate, Status = OrderStatus.shipped };
                try
                {
                    Dal.Order.Update(DOorder);
                    return BOorder;
                }
                catch (Exception e)
                {
                    throw e;
                }
            }
            else
            {
                throw new Exception("already shipped");
            }
        }
        else
        {
            throw new Exception("order does not exsist");
        }
    }
    public BO.Order UpdateDelivery(int orderId)
    {
        DO.Order DOorder = Dal.Order.GetById(orderId);
        BO.Order BOorder;

        if (DOorder.ID > 0)//if order exsist
        {
            if ((DOorder.ShipDate != null) && (DOorder.DeliveryrDate == null))
            {
                IEnumerable<BO.OrderItem> BOlistOrder = new List<BO.OrderItem>();
                IEnumerable<DO.OrderItem> listOrder = Dal.OrderItem.GetListOrder(orderId);

                double totalPriceOrder = 0;
                foreach (DO.OrderItem item in listOrder)
                {
                    BO.OrderItem newOrderItem = new BO.OrderItem() { ID = item.ID, Amount = item.Amount, Name = DOorder.CustomerName, Price = item.Price, ProductID = item.PrudoctID, TotalPrice = item.Amount * item.Price };
                    BOlistOrder.Append(newOrderItem);
                    totalPriceOrder += newOrderItem.TotalPrice;

                }
                DOorder.DeliveryrDate = DateTime.Now;
                BOorder = new BO.Order() { ID = DOorder.ID, ShipDate = DOorder.ShipDate, CustomerAddress = DOorder.CustomerAddress, CustomerEmail = DOorder.CustomerEmail, CustomerName = DOorder.CustomerName, DeliveryrDate = DOorder.DeliveryrDate, Items = BOlistOrder, TotalPrice = totalPriceOrder, OrderDate = DOorder.OrderDate, Status = OrderStatus.shipped };
                try
                {
                    Dal.Order.Update(DOorder);
                    return BOorder;
                }
                catch (Exception e)
                {
                    throw e;
                }
            }
            else
            {
                throw new Exception("order already deliverd or not shipped yet");
            }
        }
        else
        {
            throw new Exception("order does not exsist");
        }
    }
    public BO.OrderTracking TrackingOrder(int orderId)
    {


        DO.Order DOorder = Dal.Order.GetById(orderId);
        // BO.Order BOorder;

        if (DOorder.ID > 0)//if order exist
        {
            IEnumerable<(DateTime?, string)> tupList = new List<(DateTime?, string)>();
            BO.OrderStatus status = BO.OrderStatus.ordered;
            tupList.Append((DOorder.OrderDate, "ההזמנה נוצרה"));
            if (DOorder.ShipDate != null)
            {
                status = BO.OrderStatus.shipped;
                tupList.Append((DOorder.ShipDate, "ההזמנה נשלחה"));

            }
            if (DOorder.DeliveryrDate != null)
            {
                status = BO.OrderStatus.arrived;
                tupList.Append((DOorder.DeliveryrDate, "ההזמנה סופקה"));
            }

            BO.OrderTracking orderTracking = new BO.OrderTracking() { ID = orderId, Status = status, tuplesList = tupList };
            return orderTracking;
        }
        else
        {
            throw new Exception("order does not exsist");
        }
    }
    public void UpdateByManager(int orderID, int orderItemId, int amount )//bonus
    {
        DO.Order DOorder = Dal.Order.GetById(orderID);
        if (DOorder.ID > 0)//if order exist
        {
            if(DOorder.ShipDate == null)
            {
                DO.OrderItem DOorderItem = Dal.OrderItem.GetProduct(orderID, orderItemId);
                DOorderItem.Amount += amount;
                Dal.OrderItem.Update(DOorderItem);
                //we're not sure if instock needs to be updated
            }
            else
            {
                throw new Exception("Order was already shipped, can not update it");
            }
        }
        else
        {
            throw new Exception("order does not exsist");
        }

    }
}

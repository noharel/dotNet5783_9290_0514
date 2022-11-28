using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlApi;
using BO;

namespace BlImplementation;

internal class Order: BlApi.IOrder
{
    DalApi.IDal Dal = DalList();
    public IEnumerable<OrderForList> GetOrders()
    {
        IEnumerable<DO.Order> orderList;
        IEnumerable<OrderForList> orderForList=new List<OrderForList>();
        orderList = Dal.Order.GetAll();
        foreach(DO.Order var in orderList)
        {
            OrderStatus stat = (OrderStatus)0;
            if (var.ShipDate != DateTime.MinValue)
                stat = (OrderStatus)1;

            if (var.DeliveryrDate != DateTime.MinValue)
                stat = (OrderStatus)2;

            IEnumerable<DO.OrderItem> listOrderItem=Dal.OrderItem.GetListOrder(var.ID);
            double price = 0;
            foreach(DO.OrderItem orderItem in listOrderItem)
            {
                price += orderItem.Price;
            }
            OrderForList newOrderForList =new OrderForList { ID=var.ID,CustomerName=var.CustomerName,Status=stat,AmountOfItems=listOrderItem.Count(),TotalPrice=price};
            orderForList.Append(newOrderForList);
        }
        return orderForList;
    }
    public Order OrderInfo(int orderId)
    {

    }
    public Order UpdateDelivery(int orderId);
    public Order UpdateStock(int orderId);
    public OrderTracking TrackingOrder(int orderId);

    public Order UpdateByManager(int orderItemId, int amount);
}

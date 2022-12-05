using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BO;

namespace BlApi;

public interface IOrder
{
    public IEnumerable<OrderForList> GetOrders();
    public BO.Order OrderInfo(int orderId);
    public BO.Order UpdateShip(int orderId);
    public BO.Order UpdateDelivery(int orderId);
    public BO.OrderTracking TrackingOrder(int orderId);

    public void UpdateByManager(int orderID,int orderItemId, int amount);//bonus
}

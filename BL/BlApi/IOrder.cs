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
    public Order OrderInfo(int orderId);
    public Order UpdateDelivery(int orderId);
    public Order UpdateStock(int orderId);
    public OrderTracking TrackingOrder(int orderId);

    public Order UpdateByManager(int orderItemId, int amount);
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BO;

namespace BlApi;

/// <summary>
/// INTERFACE FOR ORDER FUNCTIONS
/// </summary>
public interface IOrder
{
    /// <summary>
    /// GET LIST OF ALL THE ORDERS - FOR MANAGER
    /// </summary>
    /// <returns></returns>
    public IEnumerable<OrderForList?> GetOrders();

    /// <summary>
    /// GET ORDER INFORMATION
    /// </summary>
    /// <param name="orderId"></param>
    /// <returns></returns>
    public BO.Order OrderInfo(int orderId);

    /// <summary>
    /// UPDATE SHIP DATE - FOR MANAGER
    /// </summary>
    /// <param name="orderId"></param>
    /// <returns></returns>
    public BO.Order UpdateShip(int orderId, DateTime? dat = null);

    /// <summary>
    /// UPDATE DELIVERY DATE - FOR MANAGER
    /// </summary>
    /// <param name="orderId"></param>
    /// <returns></returns>
    public BO.Order UpdateDelivery(int orderId, DateTime? dat = null);

    /// <summary>
    /// TRACKING ORDER - FOR MANAGER
    /// </summary>
    /// <param name="orderId"></param>
    /// <returns></returns>
    public BO.OrderTracking TrackingOrder(int orderId);

    /// <summary>
    /// BONUS FUNCTION - UPDATE AMOUNT BY MANAGER - FOR MANAGER
    /// </summary>
    /// <param name="orderID"></param>
    /// <param name="orderItemId"></param>
    /// <param name="amount"></param>
    public void UpdateByManager(int orderID,int orderItemId, int amount);//bonus

    
}

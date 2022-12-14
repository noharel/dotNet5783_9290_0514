using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BO;

namespace BlApi;
/// <summary>
/// 
/// </summary>
public interface IOrder
{
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public IEnumerable<OrderForList?> GetOrders();

    /// <summary>
    /// 
    /// </summary>
    /// <param name="orderId"></param>
    /// <returns></returns>
    public BO.Order OrderInfo(int orderId);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="orderId"></param>
    /// <returns></returns>
    public BO.Order UpdateShip(int orderId);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="orderId"></param>
    /// <returns></returns>
    public BO.Order UpdateDelivery(int orderId);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="orderId"></param>
    /// <returns></returns>
    public BO.OrderTracking TrackingOrder(int orderId);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="orderID"></param>
    /// <param name="orderItemId"></param>
    /// <param name="amount"></param>
    public void UpdateByManager(int orderID,int orderItemId, int amount);//bonus
}

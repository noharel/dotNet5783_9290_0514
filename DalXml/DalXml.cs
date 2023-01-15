using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DO;
using DalApi;
using System.Diagnostics;


namespace Dal;

sealed class DalXml:IDal
{
    public static IDal Instance { get; } = new DalXml(); // singelton

    private DalXml() { }
    public IOrder Order { get; } = new Dal.Order();
    public IProduct Product { get; } = new Dal.Product();
    public IOrderItem OrderItem { get; } = new Dal.OrderItem();
}
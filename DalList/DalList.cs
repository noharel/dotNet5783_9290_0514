using DalApi;

namespace Dal;

// to get all the functions of order product and ordrt item
sealed internal class DalList :IDal
{
    public static IDal Instance { get;  }= new DalList(); // singelton
    private DalList() { }
    public IOrder Order => new DalOrder();
    public IProduct Product => new DalProduct();
    public IOrderItem OrderItem => new DalOrderItem();
}
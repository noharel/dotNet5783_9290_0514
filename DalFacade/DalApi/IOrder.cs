using DO;

namespace DalApi;
public interface IOrder : ICrud<Order> 
{
    public int getRunningId(string asked);
}  



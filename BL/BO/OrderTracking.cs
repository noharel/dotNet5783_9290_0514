

namespace BO;
/// <summary>
/// Tracking of order 
/// </summary>
public class OrderTracking
{
    public int ID { get; set; }//ID for order we are Tracking
    public OrderStatus Status { get; set; }//status of order we are tracking
    public IEnumerable<(DateTime?, string?)>? tuplesList; //the info of traking


    public override string ToString()//for printing
    {
        string s;
        s = "ID: " + ID + "\nStatus:" + Status + "\n";
        if (tuplesList != null)//for each item in tupleslist add it to the string
        {

            foreach (var tuple in tuplesList)
            {
                s += tuple.ToString() + "\n";
            }
        }
        return s;
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO;

public class OrderTracking
{
    public int ID { get; set; }
    public OrderStatus Status { get; set; }
    public IEnumerable<(DateTime?, string)> tuplesList;
    

    public override string ToString()
    { 
        string s;
        s = "ID: " + ID + "\nStatus:" + Status + "\n";
        foreach(var tuple in tuplesList)
        {
            s+=tuple.ToString()+"\n";
        }
        return s;
    }
}

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
    public IEnumerable<(DateTime?, string?)>? tuplesList; //nullable?
    

    public override string ToString()
    { 
        string s;
        s = "ID: " + ID + "\nStatus:" + Status + "\n";
        if(tuplesList != null)
        {

            foreach (var tuple in tuplesList)
            {
                s += tuple.ToString() + "\n";
            }
        }
        return s;
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


// לעבוד על ההוראות בתרגיל 4 לא כתבנו את כל המחלקה הנתונה שם
namespace BlApi;
public class Factory  // מחלקת יצרן של שכבת DAL
{
    //?????
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public static IBl Get()
    {
        return new BlImplementation.Bl();
    }
}

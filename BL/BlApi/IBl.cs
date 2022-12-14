using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BO;

namespace BlApi;

/// <summary>
/// ריכוז הממשקים של השכבה לקריאה בלבד
/// </summary>
public interface IBl
{
    /// <summary>
    /// for getting a product
    /// </summary>
    public IProduct Product { get;} 

    /// <summary>
    /// 
    /// </summary>
    public IOrder Order { get;}

    /// <summary>
    /// 
    /// </summary>
    public ICart Cart { get;}
}

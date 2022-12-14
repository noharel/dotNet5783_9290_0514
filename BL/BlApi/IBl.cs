using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BO;

namespace BlApi;

/// <summary>
/// ריכוז הממשקים של השכבה, לקריאה בלבד
/// </summary>
public interface IBl
{
    /// <summary>
    /// GET A PRODUCT FONCTION
    /// </summary>
    public IProduct Product { get;} 

    /// <summary>
    /// GET AN ORDER FUNCTION
    /// </summary>
    public IOrder Order { get;}

    /// <summary>
    /// GET A CARY FUNCTION
    /// </summary>
    public ICart Cart { get;}
}

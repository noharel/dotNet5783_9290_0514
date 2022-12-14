using BlApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlImplementation;

/// <summary>
/// A CLASS THAT IMPLEMENTS THE INTERFACE IBL
/// </summary>
sealed public class Bl:IBl
{

    /// <summary>
    /// TO GET CART FANCTIONS
    /// </summary>
    public ICart Cart=> new Cart();

    /// <summary>
    /// TO GET ORDER FUNCTIONS
    /// </summary>
    public IOrder Order => new Order();

    /// <summary>
    /// TO GET PRODUCTS FUNCTIONS
    /// </summary>
    public IProduct Product => new Product();
}

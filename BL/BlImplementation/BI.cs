using BlApi;
using DalApi;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlImplementation;

/// <summary>
/// A CLASS THAT IMPLEMENTS THE INTERFACE IBL
/// </summary>
sealed public class Bl:IBl
{
    public static IBl Instance { get; } = new Bl(); // singelton
    /// <summary>
    /// TO GET CART FANCTIONS
    /// </summary>
    public ICart Cart { get; } = new Cart();

    /// <summary>
    /// TO GET ORDER FUNCTIONS
    /// </summary>
    public BlApi.IOrder Order { get; } = new Order();

    /// <summary>
    /// TO GET PRODUCTS FUNCTIONS
    /// </summary>
    public BlApi.IProduct Product { get; } = new Product();
}

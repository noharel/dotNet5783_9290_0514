using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BO;

namespace BlApi;

/// <summary>
/// INTERFACE FOR CART FUNCTION
/// </summary>
public interface ICart
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="cart"></param>
    /// <param name="id"></param>
    /// <returns></returns>
    public Cart AddProductToCart(Cart cart, int id);


    /// <summary>
    /// 
    /// </summary>
    /// <param name="cart"></param>
    /// <param name="id"></param>
    /// <param name="amount"></param>
    /// <returns></returns>
    public Cart UpdateAmountInCart(Cart cart, int id, int amount);
    

    /// <summary>
    /// 
    /// </summary>
    /// <param name="cart"></param>
    public void MakingAnOrder(Cart cart);
    

}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BO;

namespace BlApi;

/// <summary>
/// INTERFACE FOR CART FUNCTIONS
/// </summary>
public interface ICart
{
    /// <summary>
    /// ADD A PRODUCT TO CART
    /// </summary>
    /// <param name="cart"></param>
    /// <param name="id"></param>
    /// <returns></returns>
    public Cart AddProductToCart(Cart cart, int id);


    /// <summary>
    /// UPDATE AMOUNT OF A CERTAIN ITEM IN THE CART
    /// </summary>
    /// <param name="cart"></param>
    /// <param name="id"></param>
    /// <param name="amount"></param>
    /// <returns></returns>
    public Cart UpdateAmountInCart(Cart cart, int id, int amount);


    /// <summary>
    /// MAKING AN ORDER - UPDATE THE ORDERS LIST
    /// </summary>
    /// <param name="cart"></param>
    public void MakingAnOrder(Cart cart);
    public BO.ProductItem ProductItemWithAmount(int id);
}

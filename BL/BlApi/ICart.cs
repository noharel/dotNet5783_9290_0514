using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BO;

namespace BlApi;

public interface ICart
{
    public Cart AddProductToCart(Cart cart, int id);
    public Cart UpdateAmountInCart(Cart cart, int id, int amount);
    public void MakingAnOrder(Cart cart);
    

}

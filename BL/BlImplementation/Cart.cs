using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlApi;
using DalApi;
using DO;

namespace BlImplementation;

internal class Cart :ICart
{
    IDal Dal = DalList();
    public Cart AddProductToCart(BO.Cart cart, int id)
    {
        bool flag=false;
        BO.OrderItem orderItem=new BO.OrderItem();
        foreach(BO.OrderItem var in cart.Items )
        {
            if (var.ID == id)
            {
                flag = true;
                orderItem = var;
            }
        }
        if (flag)
        {
            if(Dal.Product.GetById(id).InStock>0)
            {
                orderItem.Amount++;
                orderItem.Price+= Dal.Product.GetById(id).Price;
                cart.TotalPrice += Dal.Product.GetById(id).Price;

            }
            else
            {
                throw new Exception("Not in stock");
            }
        }
        else
        {
            try
            {
                DO.Product product=Dal.Product.GetById(id);
                if(product.InStock>0)
                {
                    //BO.OrderItem BOOrderItem=new BO.OrderItem({ ID=product.ID,Name=});
                    orderItem.
                    cart.Items.Append();
                }
                else
                {
                    throw new Exception("product does not exsist");
                }

            }
            catch (Exception e)
            {
                throw e;
            }
            return new Cart();
    }

        public Cart UpdateAmountInCart(Cart cart, int id, int amount);
       
    public void MakingAnOrder(Cart cart);


}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BO;

namespace BlApi;

/// <summary>
/// INTERFACE FOR PRODUCT FUNCTIONS
/// </summary>
public interface IProduct 
{
    /// <summary>
    /// GET LIST OF PRODUCTS
    /// </summary>
    /// <returns></returns>
    public IEnumerable<ProductForList?> GetListProduct();//בקשת רשימת מוצרים

    /// <summary>
    /// GET LIST OF PRODUCTS WITH FILTER
    /// </summary>
    /// <param name="filter"></param>
    /// <returns></returns>
    public IEnumerable<ProductForList?> GetListProduct(Func<ProductForList?, bool>? filter);//בקשת רשימת מוצרים עם סינון

    /// <summary>
    /// GET PRODUCT INFO - FOR MANAGER
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public Product GetProductInfo_manager(int id);//בקשת פרטי מוצר עבור מסך מנהל

    /// <summary>
    /// GET PRODUCT INFORMATION - FOR CLIENT
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cart"></param>
    /// <returns></returns>
    public ProductItem GetProductInfo_client(int id,Cart cart);//בקשת פרטי מוצר עבור מסך לקוח

    /// <summary>
    /// ADD PRODUCT - FOR MANAGER
    /// </summary>
    /// <param name="product"></param>
    public void AddProdut(Product product);

    /// <summary>
    /// DELETE PRODUCT -  FOR MANAGER
    /// </summary>
    /// <param name="id"></param>
    public void DeleteProduct(int id);

    /// <summary>
    /// UPDATE PRODUCT INFORMATION - FOR MANAGER
    /// </summary>
    /// <param name="product"></param>
    public void UpdateProduct(Product product);

}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BO;

namespace BlApi;

public interface IProduct 
{
    public IEnumerable<ProductForList?> GetListProduct();//בקשת רשימת מוצרים
    public Product GetProductInfo_manager(int id);//בקשת פרטי מוצר עבור מסך מנהל
    public ProductItem GetProductInfo_client(int id,Cart cart);//בקשת פרטי מוצר עבור מסך לקוח

    public void AddProdut(Product product);
    public void DeleteProduct(int id);
    public void UpdateProduct(Product product);

}

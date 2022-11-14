using DalApi;
using DO;
using System.Linq.Expressions;

namespace Dal;

public class DalProduct: IProduct
{
    DataSource _ds = DataSource.s_instance;
    public int Add(Product product)
    {
        if (_ds._products == null)
            throw new NotImplementedException();
        //product.ID = DataSource.configP.NextOrderNumber;
        _ds._products.Add(product);
        return product.ID;
    }
    public void Delete(int id)
    {
        if (_ds?._products.RemoveAll(product => product.ID == id) == 0)
        {
            throw new Exception("Can't delete that does not exist");
        }
        Product p= GetById(id);  
        p.IsDeleted = true;
    }
    public IEnumerable<Product> GetAll(Func<Product, bool>? filter) =>
        (filter == null ?
        _ds?._products.Select(item => item) :
        _ds?._products.Where(filter))
        ?? throw new Exception("Missing product");

    public Product GetById(int id)
    {
        if (_ds._products == null)
            throw new Exception("Missing order id");
        foreach (Product p in _ds._products)
        {
            if (p.ID == id)
                return p;
        }
        return new Product();

    }
    public void Update(Product product)
    {
        if (_ds._products == null) throw new NotImplementedException();

        _ds._products.Remove(GetById(product.ID));
        _ds._products.Add(product);
    }
    public IEnumerable<Product> GetAll()
    {
        return (from Product _products in _ds._products select _products).ToList();
    }

}

using DalApi;
using DO;

namespace Dal;

public class DalProduct: IProduct
{
    DataSource _ds = DataSource.s_instance;
    public int Add(Product product)
    {
        if (_ds._products.FirstOrDefault() != null)
            throw new NotImplementedException();
        product.ID = DataSource.configP.NextOrderNumber;
        _ds._products.Add(product);
        return product.ID;
    }
    public void Delete(int id)
    {
        if (_ds?._products.RemoveAll(product => product?.ID == id) == 0)
        {
            throw new Exception("Can't delete that does not exist");
        }
    }
    public IEnumerable<Product?> GetAll(Func<Product?, bool>? filter) =>
        (filter == null ?
        _ds?._products.Select(item => item) :
        _ds?._products.Where(filter))
        ?? throw new Exception("Missing product");

    public Product GetById(int id) => _ds._products.FirstOrDefault() ?? throw new Exception("Missing product id");
    public void Update(Product product)
    {
        throw new NotImplementedException();
    }
    public IEnumerable<Product> GetAll()
    {
        return (from Product _products in _ds._products select _products).ToList();
    }

}

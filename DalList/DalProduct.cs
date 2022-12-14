using DalApi;
using DO;
using System.Linq.Expressions;

namespace Dal;

public class DalProduct: IProduct
{
    DataSource _ds = DataSource.s_instance;
    public int Add(Product product)  //add new product
    {
        if (_ds._products == null)
            throw new DoesntExistExeption("Products list does not exist");
        _ds._products.Add(product);
        return product.ID;
    }
    public void Delete(int id) //delete
    {
        try
        {
            Product p = GetById(id);
            p.IsDeleted = true; //update IsDeleted
        }
        catch (DO.DoesntExistExeption e)
        {
            throw e;
        }
        if (_ds?._products.RemoveAll(product => product?.ID == id) == 0)  //delete
        {
            throw new DoesntExistExeption("Can't delete that does not exist");
        }
        
    }
    public IEnumerable<Product?> GetAll(Func<Product?, bool>? filter) =>
        (filter == null ?
        _ds?._products.Select(item => item) :
        _ds?._products.Where(filter))
        ?? throw new DoesntExistExeption("Missing product");

    public Product? GetById(Func<Product?, bool>? filter) =>
     (filter == null ?
     _ds?._products.Select(item => item).FirstOrDefault() :
     _ds?._products.Where(filter).FirstOrDefault())
     ?? throw new DoesntExistExeption("Missing product");

    public Product GetById(int id)  //get by id
    {
        if (_ds._products == null)
            throw new DoesntExistExeption("there are no products");
        foreach (Product? p in _ds._products)
        {
            if (p?.ID == id)
                return (Product)p; //return product
        }
        throw new DoesntExistExeption("Missing product id");

    }
    public void Update(Product product)  //update
    {
        if (_ds._products == null) throw new DoesntExistExeption("Products list does not exist");

        _ds._products.Remove(GetById(product.ID)); //remove the old one
        _ds._products.Add(product);  //add new one
    }
    public IEnumerable<Product?> GetAll() //get all products
    {
        IEnumerable<Product?> list=(from Product? _products in _ds._products select _products).ToList();
        if(list==null)throw new DoesntExistExeption("Missing product");
        else return list;
    }

}

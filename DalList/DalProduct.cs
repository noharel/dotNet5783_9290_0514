using DalApi;
using DO;
using System.Linq.Expressions;

namespace Dal;


public class DalProduct: IProduct // A CLASS THAT IMPLEMENTS THE INTERFACE IProduct
{
    DataSource _ds = DataSource.s_instance; //initialize

    /// <summary>
    /// ADD PRODUCT
    /// </summary>
    /// <param name="product"></param>
    /// <returns></returns>
    /// <exception cref="DoesntExistExeption"></exception>
    public int Add(Product product)  
    {
        if (_ds._products == null) // there are no products
            throw new DoesntExistExeption("Products list does not exist");
        _ds._products.Add(product); // add
        return product.ID;
    }

    /// <summary>
    /// DELETE PRODUCT
    /// </summary>
    /// <param name="id"></param>
    /// <exception cref="DoesntExistExeption"></exception>
    public void Delete(int id) 
    {
        try
        {
            // check if the product is exict
            Product p = GetById(id);
            p.IsDeleted = true; //update IsDeleted
        }
        catch (DO.DoesntExistExeption e) // the product is not exict
        {
            throw e;
        }
        if (_ds?._products.RemoveAll(product => product?.ID == id) == 0)  //delete
        {
            throw new DoesntExistExeption("Can't delete that does not exist");
        }
        
    }

    /// <summary>
    /// GET ALL PRODUCTS BY FILTER
    /// </summary>
    /// <param name="filter"></param>
    /// <returns></returns>
    /// <exception cref="DoesntExistExeption"></exception>
    public IEnumerable<Product?> GetAll(Func<Product?, bool>? filter) =>
        (filter == null ?
        _ds?._products.Select(item => item) :
        _ds?._products.Where(filter)) // choose products by the filter
        ?? throw new DoesntExistExeption("Missing product");

    /// <summary>
    /// GET PRODUCT BY FILTER
    /// </summary>
    /// <param name="filter"></param>
    /// <returns></returns>
    /// <exception cref="DoesntExistExeption"></exception>
    public Product? GetById(Func<Product?, bool>? filter) =>
     (filter == null ?
     _ds?._products.Select(item => item).FirstOrDefault() :
     _ds?._products.Where(filter).FirstOrDefault()) // choose product filter
     ?? throw new DoesntExistExeption("Missing product");

    /// <summary>
    /// GET PRODUCT BY ID
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    /// <exception cref="DoesntExistExeption"></exception>
    public Product GetById(int id) 
    {
        if (_ds._products == null) // there are no products
            throw new DoesntExistExeption("there are no products");
        foreach (Product? p in _ds._products)
        {
            if (p?.ID == id)
                return (Product)p; //return product
        }
        throw new DoesntExistExeption("Missing product id");

    }

    /// <summary>
    /// UPDATE PRODUCT INFORMATION
    /// </summary>
    /// <param name="product"></param>
    /// <exception cref="DoesntExistExeption"></exception>
    public void Update(Product product)  
    {
        if (_ds._products == null) throw new DoesntExistExeption("Products list does not exist");

        _ds._products.Remove(GetById(product.ID)); //remove the old one
        _ds._products.Add(product);  //add new one
    }

    /// <summary>
    /// GET ALL PRODUCTS
    /// </summary>
    /// <returns></returns>
    /// <exception cref="DoesntExistExeption"></exception>
    public IEnumerable<Product?> GetAll() 
    {
        IEnumerable<Product?> list=(from Product? _products in _ds._products select _products).ToList();
        if(list == null) // there are on products
            throw new DoesntExistExeption("Missing product");
        else 
            return list; //return all the rpoducts
    }

}

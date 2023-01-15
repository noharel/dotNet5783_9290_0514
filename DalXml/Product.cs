using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using DO;
using DalApi;
using System.Xml.Serialization;
using System.Xml.Linq;
using static System.Net.WebRequestMethods;

namespace Dal;
/// <summary>
/// using xml to linq, WElement
/// </summary>
internal class Product : IProduct
{
    const string s_products = "Products"; //Linq to XML
    /// <summary>
    /// get Product from xml file
    /// </summary>
    /// <param name="p"></param>
    /// <returns></returns>
    static DO.Product? getProduct(XElement p) =>
        p.ToIntNullable("ID") is null ? null : new DO.Product()
        {
            ID = (int)p.Element("ID")!,
            Name = (string?)p.Element("Name"),
            Price = (double?)p.Element("Price"),
            Category = p.ToEnumNullable<DO.Category>("Category"),
            InStock =(int) p.Element("InStock")!,
            IsDeleted = (bool)p.Element("IsDeleted")!,
        };
    /// <summary>
    /// create elemnt for the product
    /// </summary>
    /// <param name="product"></param>
    /// <returns></returns>
    static IEnumerable<XElement> createProductElement(DO.Product product)
    {
        yield return new XElement("ID", product.ID);
        if (product.Name is not null)
            yield return new XElement("Name", product.Name);
        if (product.Price is not null)
            yield return new XElement("Price", product.Price);
        if (product.Category is not null)
            yield return new XElement("Category", product.Category);
        if (product.InStock is not null)
            yield return new XElement("InStock", product.InStock);
        if (product.IsDeleted is not null)
            yield return new XElement("IsDeleted", product.IsDeleted);
    }
    /// <summary>
    /// add product to list of products in xml file
    /// </summary>
    /// <param name="product"></param>
    /// <returns></returns>
    /// <exception cref="AlreadyExistExeption"></exception>
    public int Add(DO.Product product)
    { 
        XElement productsRootElem = XMLTools.LoadListFromXMLElement(s_products);
        if (XMLTools.LoadListFromXMLElement(s_products)?.Elements()
            .FirstOrDefault(st => st.ToIntNullable("ID") == product.ID) is not null)
            throw new AlreadyExistExeption("The prouct ID number is already exist");
        productsRootElem.Add(new XElement("Product", createProductElement(product)));
        XMLTools.SaveListToXMLElement(productsRootElem, s_products);
        return product.ID; ;
    }
    /// <summary>
    /// get product with the sent id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    /// <exception cref="DoesntExistExeption"></exception>
    public DO.Product GetById(int id) =>
        (DO.Product)getProduct(XMLTools.LoadListFromXMLElement(s_products)?.Elements()
        .FirstOrDefault(st => st.ToIntNullable("ID") == id)
            ?? throw new DoesntExistExeption("Missing product id"))!;

    /// <summary>
    /// update product with id of product that was sent to info of product that was sent
    /// </summary>
    /// <param name="product"></param>
    public void Update(DO.Product product)
    {
        Delete(product.ID);
        Add(product);
    }
    /// <summary>
    /// delete product with id that was sent
    /// </summary>
    /// <param name="id"></param>
    /// <exception cref="DoesntExistExeption"></exception>
    public void Delete(int id)
    {
        XElement productsRootElem = XMLTools.LoadListFromXMLElement(s_products);
        (productsRootElem.Elements()
            .FirstOrDefault(st => (int?)st.Element("ID") == id) ?? throw new DoesntExistExeption("Missing product id"))
            .Remove();
        XMLTools.SaveListToXMLElement(productsRootElem, s_products);
    }
    /// <summary>
    /// get list of products using a sent filter
    /// </summary>
    /// <param name="filter"></param>
    /// <returns></returns>
    public IEnumerable<DO.Product?> GetAll(Func<DO.Product?, bool>? filter)
    => filter is null
        ? XMLTools.LoadListFromXMLElement(s_products).Elements().Select(p => getProduct(p))
        : XMLTools.LoadListFromXMLElement(s_products).Elements().Select(p => getProduct(p)).Where(filter);

    /// <summary>
    /// get product with filter that was sent
    /// </summary>
    /// <param name="filter"></param>
    /// <returns></returns>
    public DO.Product? GetById(Func<DO.Product?, bool>? filter)
        => filter is null
        ? XMLTools.LoadListFromXMLElement(s_products).Elements().Select(p => getProduct(p)).FirstOrDefault()
        : XMLTools.LoadListFromXMLElement(s_products).Elements().Select(p => getProduct(p)).Where(filter).FirstOrDefault();
    
    /// <summary>
    /// get list of all products
    /// </summary>
    /// <returns></returns>
    public IEnumerable<DO.Product?> GetAll()
            => XMLTools.LoadListFromXMLElement(s_products).Elements().Select(p => getProduct(p));


}
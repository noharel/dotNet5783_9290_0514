using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using DO;
using DalApi;
using System.Xml.Serialization;

namespace Dal;

internal class Product : IProduct
{
    int ICrud<DO.Product>.Add(DO.Product product)
    {
        XmlSerializer myDeserilizer = new XmlSerializer(typeof(Dal.Product));
        FileStream myfilestream = new FileStream("\\dotNet5783_9290_0514\\xml\\product.xml\"", FileMode.Open);
        var loadedData = myDeserilizer.Deserialize(myfilestream);
        List<DO.Product?>? productCheck = (List<DO.Product?>?)loadedData;
        myfilestream.Close();
        productCheck = productCheck!.Where(item => item?.ID == product.ID).ToList(); //GET ALL THE PRODUCTS WITH THE SAME ID
        if (productCheck!.Count == 0) // if there is no product with the same id
        {
            // Add and return
            TextWriter writer = new StreamWriter("\\dotNet5783_9290_0514\\xml\\product.xml\"", true);
            myDeserilizer.Serialize(writer, product);
            myfilestream.Close();
            //4_ds._products.Add(product);
            return product.ID;
        }
        else // The ID is in use
        {
            myfilestream.Close();
            throw new AlreadyExistExeption("The prouct ID number is already exist");
        }
    }

    DO.Product ICrud<DO.Product>.GetById(int id)
    {
        XmlSerializer myDeserilizer = new XmlSerializer(typeof(Dal.Product));
        FileStream myfilestream = new FileStream("\\dotNet5783_9290_0514\\xml\\product.xml\"", FileMode.Open);
        var loadedData = myDeserilizer.Deserialize(myfilestream);
        List<DO.Product?>? productCheck = (List<DO.Product?>?)loadedData;

        myfilestream.Close();
        if (productCheck == null) // there are no products
            throw new DoesntExistExeption("there are no products");
        return productCheck.Where(p => p!.Value.ID == id).FirstOrDefault() // choose product by id
            ?? throw new DoesntExistExeption("Missing product id");
    }

    void ICrud<DO.Product>.Update(DO.Product product)
    {
        XmlSerializer myDeserilizer = new XmlSerializer(typeof(Dal.Product));
        FileStream myfilestream = new FileStream("\\dotNet5783_9290_0514\\xml\\product.xml\"", FileMode.Open);
        var loadedData = myDeserilizer.Deserialize(myfilestream);
        List<DO.Product?>? productCheck = (List<DO.Product?>?)loadedData;
        DO.Product? prod;
        if (productCheck == null) // there are no products
            throw new DoesntExistExeption("there are no products");
        else prod = productCheck.Where(p => p!.Value.ID == product.ID).FirstOrDefault() // choose product by id
            ?? throw new DoesntExistExeption("Missing product id");
        if (prod == null) throw new DoesntExistExeption("Products list does not exist");
        productCheck.Remove(prod); //remove the old one
        productCheck.Add(product);  //add new one
        TextWriter writer = new StreamWriter("\\dotNet5783_9290_0514\\xml\\product.xml\"", false);

        myDeserilizer.Serialize(writer, product);
        myfilestream.Close();
    }

    void ICrud<DO.Product>.Delete(int id)
    {
        XmlSerializer myDeserilizer = new XmlSerializer(typeof(Dal.Product));
        FileStream myfilestream = new FileStream("\\dotNet5783_9290_0514\\xml\\product.xml\"", FileMode.Open);
        var loadedData = myDeserilizer.Deserialize(myfilestream);
        List<DO.Product?>? productCheck = (List<DO.Product?>?)loadedData;
        DO.Product? prod;
        if (productCheck == null) // there are no products
            throw new DoesntExistExeption("there are no products");
        else prod = productCheck.Where(p => p!.Value.ID == id).FirstOrDefault() // choose product by id
            ?? throw new DoesntExistExeption("Missing product id");

        if (productCheck!.RemoveAll(product => product?.ID == id) == 0)  //delete
        {
            TextWriter writer = new StreamWriter("\\dotNet5783_9290_0514\\xml\\product.xml\"", false);

            myDeserilizer.Serialize(writer, productCheck);
            myfilestream.Close();

            throw new DoesntExistExeption("Can't delete that does not exist");
        }
    }

    IEnumerable<DO.Product?> ICrud<DO.Product>.GetAll(Func<DO.Product?, bool>? filter)
    {
        XmlSerializer myDeserilizer = new XmlSerializer(typeof(Dal.Product));
        FileStream myfilestream = new FileStream("\\dotNet5783_9290_0514\\xml\\product.xml\"", FileMode.Open);
        var loadedData = myDeserilizer.Deserialize(myfilestream);
        List<DO.Product?>? productCheck = (List<DO.Product?>?)loadedData;
        myfilestream.Close();
        return (filter == null ?
        productCheck!.Select(item => item) :
        productCheck!.Where(filter)) // choose products by the filter
        ?? throw new DoesntExistExeption("Missing product");
    }

    DO.Product? ICrud<DO.Product>.GetById(Func<DO.Product?, bool>? filter)
    {
        XmlSerializer myDeserilizer = new XmlSerializer(typeof(Dal.Product));
        FileStream myfilestream = new FileStream("\\dotNet5783_9290_0514\\xml\\product.xml\"", FileMode.Open);
        var loadedData = myDeserilizer.Deserialize(myfilestream);
        List<DO.Product?>? productCheck = (List<DO.Product?>?)loadedData;
        myfilestream.Close();

        return (filter == null ?
     productCheck!.Select(item => item).FirstOrDefault() :
     productCheck!.Where(filter).FirstOrDefault()) // choose product filter
     ?? throw new DO.DoesntExistExeption("Missing product");
    }

    IEnumerable<DO.Product?> ICrud<DO.Product>.GetAll()
    {
        XmlSerializer myDeserilizer = new XmlSerializer(typeof(Dal.Product));
        FileStream myfilestream = new FileStream("\\dotNet5783_9290_0514\\xml\\product.xml\"", FileMode.Open);
        var loadedData = myDeserilizer.Deserialize(myfilestream);
        myfilestream.Close();
        List<DO.Product?>? productCheck = (List<DO.Product?>?)loadedData;

        if ((IEnumerable<Product?>)loadedData! == null) // there are on products
            throw new DoesntExistExeption("Missing product");
        else
            return productCheck!; //return all the rpoducts    }
    }
}
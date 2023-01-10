using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DO;
using DalApi;

namespace Dal;

internal class Product: IProduct
{
    int Add(T item) //ADD T
    {

    }
    T GetById(int id)//GET T BY ID
    {

    }
    void Update(T item) // UPDATE T
    {

    }
    void Delete(int id) // DELETE T BY ID
    {

    }

    IEnumerable<T?> GetAll(Func<T?, bool>? filter = null)  // GET ALL T BY CERTAIN FILTER
    {

    }
    T? GetById(Func<T?, bool>? filter) // GET T BY CERTAIN FILTER
    {

    }
    IEnumerable<T?> GetAll() // GET ALL T IN THE COLLECTION
    {
       
        XmlSerializer myDeserilizer = new XmlSerializer(typeof(Dal.Product));
        FileStream myfilestream = new FileStream("sth.xml", FileMode.Open);
        var loadedData = (Class1)myDeserilizer.Deserialize(myfilestream);
        myfilestream.Close();
        return

    }
}

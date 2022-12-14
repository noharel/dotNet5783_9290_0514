using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DalApi;

public interface ICrud<T> where T : struct // abstarct interface
{
    int Add(T item); //ADD T
    T GetById(int id); //GET T BY ID
    void Update(T item); // UPDATE T
    void Delete(int id); // DELETE T BY ID

    IEnumerable<T?> GetAll(Func<T?, bool>? filter = null); // GET ALL T BY CERTAIN FILTER
    T? GetById(Func<T?, bool>? filter); // GET T BY CERTAIN FILTER
    IEnumerable<T?> GetAll(); // GET ALL T IN THE COLLECTION
}


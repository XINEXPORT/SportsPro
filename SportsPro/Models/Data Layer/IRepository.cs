using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using SportsPro.Models;

//The repository pattern encapsulates the logic required to access data sources.
//This interface defines the CRUD operation for any entity type.
//This allows the same interface aka the IRepsitory to be used with multiple entities.
//IRepository is a generic interface with a type parameter T, only entity classes can be used with a T type.

namespace SportsPro.Data.Configuration
{
    public interface IRepository<T>
        where T : class
    {
        IEnumerable<T> GetAll();
        T GetById(int id);
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
        void Save();
    }
}

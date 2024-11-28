using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using SportsPro.Models;

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

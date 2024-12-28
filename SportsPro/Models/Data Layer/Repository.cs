using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using SportsPro.Models;

//The Repository type T will be used for the Repository classes.

namespace SportsPro.Data.Configuration
{
    public class Repository<T> : IRepository<T>
        where T : class
    {
        protected SportsProContext _context { get; set; } //use the DBContext
        private DbSet<T> _dbSet { get; set; } //DBSet is the database table

        public Repository(SportsProContext context) //constructor initializes the context and table with type T
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        //List all the entitities
        public virtual IEnumerable<T> List(QueryOptions<T> options)
        {
            IQueryable<T> query = BuildQuery(options);
            return query.ToList();
        }

        //Build a queryable LINQ object
        //start by looking for a dbSet table
        //it needs to include, where, and hasOrderBy <--the query options
        private IQueryable<T> BuildQuery(QueryOptions<T> options)
        {
            IQueryable<T> query = _dbSet;
            foreach (string include in options.GetIncludes())
            {
                query = query.Include(include);
            }
            if (options.HasWhere)
            {
                query = query.Where(options.Where);
            }
            if (options.HasOrderBy)
            {
                query = query.OrderBy(options.OrderBy);
            }

            return query;
        }

        //overload the get method
        //get by int id, string id, and return the first entity matching the int or string id
        public virtual T? Get(int id) => _dbSet.Find(id);

        public virtual T? Get(string id) => _dbSet.Find(id);

        public virtual T? Get(QueryOptions<T> options)
        {
            IQueryable<T> query = BuildQuery(options);
            return query.FirstOrDefault();
        }

        //CRUD Methods
        public IEnumerable<T> GetAll() => _dbSet.ToList();

        public T GetById(int id) => _dbSet.Find(id);

        public void Add(T entity) => _dbSet.Add(entity);

        public void Update(T entity) => _dbSet.Update(entity);

        public void Delete(T entity) => _dbSet.Remove(entity);

        public void Save() => _context.SaveChanges();
    }
}

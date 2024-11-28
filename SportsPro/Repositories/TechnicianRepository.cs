using System.Collections.Generic;
using System.Linq;
using SportsPro.Data.Configuration;
using SportsPro.Models;

namespace SportsPro.Data
{
    public class TechnicianRepository : IRepository<Technician>
    {
        private readonly SportsProContext _context;

        // Constructor to inject the DbContext
        public TechnicianRepository(SportsProContext context)
        {
            _context = context;
        }

        // Implementing GetAll to return IEnumerable<Technician>
        public IEnumerable<Technician> GetAll()
        {
            return _context.Technicians.ToList();
        }

        // Implementing GetById
        public Technician GetById(int id)
        {
            return _context.Technicians.Find(id);
        }

        // Adding a new Technician entity
        public void Add(Technician entity)
        {
            _context.Technicians.Add(entity);
            _context.SaveChanges();
        }

        // Updating an existing Technician entity
        public void Update(Technician entity)
        {
            _context.Technicians.Update(entity);
            _context.SaveChanges();
        }

        // Deleting a Technician entity
        public void Delete(Technician entity)
        {
            _context.Technicians.Remove(entity);
            _context.SaveChanges();
        }

        // Save method to commit changes
        public void Save()
        {
            _context.SaveChanges();
        }
    }
}

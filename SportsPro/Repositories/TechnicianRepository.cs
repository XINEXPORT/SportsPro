using System.Linq;
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

        // Change return type to IQueryable<Technician>
        public IQueryable<Technician> GetAll()
        {
            return _context.Technicians.AsQueryable();
        }

        public Technician Get(int id)
        {
            return _context.Technicians.Find(id);
        }

        public void Add(Technician entity)
        {
            _context.Technicians.Add(entity);
            _context.SaveChanges();
        }

        public void Update(Technician entity)
        {
            _context.Technicians.Update(entity);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var technician = _context.Technicians.Find(id);
            if (technician != null)
            {
                _context.Technicians.Remove(technician);
                _context.SaveChanges();
            }
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}

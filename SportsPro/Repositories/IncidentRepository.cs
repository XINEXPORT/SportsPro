using System.Linq;
using SportsPro.Models;

namespace SportsPro.Data
{
    public class IncidentRepository : IRepository<Incident>
    {
        private readonly SportsProContext _context;

        // Constructor to inject the DbContext
        public IncidentRepository(SportsProContext context)
        {
            _context = context;
        }

        // Change return type to IQueryable<Incident>
        public IQueryable<Incident> GetAll()
        {
            return _context.Incidents.AsQueryable();
        }

        public Incident Get(int id)
        {
            return _context.Incidents.Find(id);
        }

        public void Add(Incident entity)
        {
            _context.Incidents.Add(entity);
            _context.SaveChanges();
        }

        public void Update(Incident entity)
        {
            _context.Incidents.Update(entity);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var incident = _context.Incidents.Find(id);
            if (incident != null)
            {
                _context.Incidents.Remove(incident);
                _context.SaveChanges();
            }
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}

using SportsPro.Data.Configuration;
using SportsPro.Models;

namespace SportsPro.Data
{
    public class IncidentRepository : IRepository<Incident>
    {
        private readonly SportsProContext _context;

        public IncidentRepository(SportsProContext context)
        {
            _context = context;
        }

        public IEnumerable<Incident> GetAll()
        {
            return _context.Incidents.ToList();
        }

        public Incident GetById(int id)
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

        public void Delete(Incident entity)
        {
            _context.Incidents.Remove(entity);
            _context.SaveChanges();
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}

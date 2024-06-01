using GospodaWiki.Data;
using GospodaWiki.Interfaces;
using GospodaWiki.Models;

namespace GospodaWiki.Repository
{
    public class RpgSystemRepository : IRpgSystemInterface
    {
        private readonly DataContext _context;

        public RpgSystemRepository(DataContext context)
        {
            _context = context;
        }

        public ICollection<RpgSystem> GetRpgSystems()
        {
            return _context.RpgSystems.ToList();
        }
        public RpgSystem GetRpgSystem(int id)
        {
            return _context.RpgSystems.Where(p => p.Id == id).FirstOrDefault();
        }   
        public RpgSystem GetRpgSystem(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentException("Name cannot be empty or null.");
            }

            return _context.RpgSystems.FirstOrDefault(c => c.Name == name);
        }
        public bool RpgSystemExists(int rpgSystemId)
        {
            return _context.RpgSystems.Any(p => p.Id == rpgSystemId);
        }
        public bool CreateRpgSystem(RpgSystem rpgSystem)
        {
            if (rpgSystem == null)
            {
                throw new ArgumentNullException(nameof(rpgSystem));
            }

            _context.RpgSystems.Add(rpgSystem);
            return Save();
        }
        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved >= 0;
        }
        public bool UpdateRpgSystem(RpgSystem rpgSystem)
        {
            if (rpgSystem == null)
            {
                throw new ArgumentNullException(nameof(rpgSystem));
            }

            _context.RpgSystems.Update(rpgSystem);
            return Save();
        }
        public bool DeleteRpgSystem(RpgSystem rpgSystem)
        {
            if (rpgSystem == null)
            {
                throw new ArgumentNullException(nameof(rpgSystem));
            }

            _context.RpgSystems.Remove(rpgSystem);
            return Save();
        }
    }
}

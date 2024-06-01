using AutoMapper;
using GospodaWiki.Data;
using GospodaWiki.Interfaces;
using GospodaWiki.Models;

namespace GospodaWiki.Repository
{
    public class LocationRepository : ILocationInterface
    {
        private readonly DataContext _context;

        public LocationRepository(DataContext context)
        {
            _context = context;
        }

        public ICollection<Location> GetLocations()
        {
            return _context.Locations.ToList();
        }
        public Location GetLocation(int locationId)
        {
            return _context.Locations.Where(l => l.Id == locationId).FirstOrDefault();
        }
        public Location GetLocation(string locationName)
        {
            if(string.IsNullOrEmpty(locationName))
            {
                throw new ArgumentException("Location name cannot be empty or null.");
            }
        
            return _context.Locations.FirstOrDefault(l => l.Name.Trim().ToUpper() == locationName.Trim().ToUpper());
        }
        public bool LocationExists(int locationId)
        {
            return _context.Locations.Any(l => l.Id == locationId);
        }
        public bool CreateLocation(Location location)
        {
            if(location == null)
            {
                throw new ArgumentNullException(nameof(location));
            }

            _context.Locations.Add(location);
            return Save();
        }
        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved >=0;
        }
        public bool UpdateLocation(Location location)
        {
            if(location == null)
            {
                throw new ArgumentNullException(nameof(location));
            }
            _context.Locations.Update(location);
            return Save();
        }
        public bool DeleteLocation(Location location)
        {
            if (location == null)
            {
                throw new ArgumentNullException(nameof(location));
            }

            _context.Locations.Remove(location);
            return Save();
        }
    }
}

using AutoMapper;
using GospodaWiki.Data;
using GospodaWiki.Dto.Location;
using GospodaWiki.Interfaces;
using GospodaWiki.Models;
using Microsoft.EntityFrameworkCore;

namespace GospodaWiki.Repository
{
    public class LocationRepository : ILocationInterface
    {
        private readonly DataContext _context;

        public LocationRepository(DataContext context)
        {
            _context = context;
        }

        public ICollection<LocationsDto> GetLocations()
        {
            var locations = _context.Locations.ToList();
            var locationsDto = new List<LocationsDto>();

            foreach (var location in locations)
            {
                locationsDto.Add(new LocationsDto
                {
                    LocationId = location.LocationId,
                    Name = location.Name,
                    Address = location.Address,
                    City = location.City,
                    LocationURL = location.LocationURL
                });
            }
            
            return locationsDto;
        }

        public LocationDetailsDto GetLocation(int locationId)
        {
            var Location = _context.Locations.Where(l => l.LocationId == locationId).FirstOrDefault();
            var Events = _context.Events.Where(e => e.LocationId == locationId).Select(e => e.Name);

            var locationDetailsDto = new LocationDetailsDto
            {
                LocationId = Location.LocationId,
                Name = Location.Name,
                Address = Location.Address,
                City = Location.City,
                LocationURL = Location.LocationURL,
                Events = Events.ToList()
            };

            return locationDetailsDto;
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
            return _context.Locations.Any(l => l.LocationId == locationId);
        }
        public bool CreateLocation(PostLocationDto location)
        {
            if(location == null)
            {
                throw new ArgumentNullException(nameof(location));
            }

            var locationDto = new Location
            {
                Name = location.Name,
                Address = location.Address,
                City = location.City,
                LocationURL = location.LocationURL
            };

            _context.Locations.Add(locationDto);
            return Save();
        }
        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved >=0;
        }
        public async Task<bool> SaveAsync()
        {
            var saved = await _context.SaveChangesAsync();
            return saved >= 0;
        }
        public async Task<bool> UpdateLocation(PatchLocationDto locationToUpdate, int locationId)
        {
            if(locationToUpdate == null)
            {
                throw new ArgumentNullException(nameof(locationToUpdate));
            }

            var locationContext = await _context.Locations
                .Include(c => c.Events)
                .FirstOrDefaultAsync(l => l.LocationId == locationId);

            if(locationContext == null)
            {
                throw new ArgumentNullException(nameof(locationToUpdate));
            }

            locationContext.Name = locationToUpdate.Name ?? locationContext.Name;
            locationContext.Address = locationToUpdate.Address ?? locationContext.Address;
            locationContext.City = locationToUpdate.City ?? locationContext.City;
            locationContext.LocationURL = locationToUpdate.LocationURL ?? locationContext.LocationURL;

            if(locationToUpdate.EventId != null)
            {                 
                locationContext.Events?.Clear();
                if(locationToUpdate.EventId.Any())
                {
                    var events = await _context.Events
                        .Where(e => locationToUpdate.EventId.Contains(e.EventId))
                        .ToListAsync();
                    locationContext.Events = events;
                }
            }

            _context.Locations.Update(locationContext);
            return await SaveAsync();
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

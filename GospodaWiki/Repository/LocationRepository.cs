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
            var locations = _context.Locations.Where(l => l.isPublished).ToList();
            var locationsDto = new List<LocationsDto>();

            foreach (var location in locations)
            {
                locationsDto.Add(new LocationsDto
                {
                    LocationId = location.LocationId,
                    Name = location.Name,
                    Address = location.Address,
                    City = location.City,
                    LocationURL = location.LocationURL,
                    isPublished = location.isPublished
                });
            }
            
            return locationsDto;
        }
        public ICollection<LocationsDto> GetUnpublishedLocations()
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
                    LocationURL = location.LocationURL,
                    isPublished = location.isPublished
                });
            }

            return locationsDto;
        }
        public LocationDetailsDto GetLocation(int locationId)
        {
            var Location = _context.Locations.Where(l => l.LocationId == locationId && l.isPublished).FirstOrDefault();
            var Events = _context.Events.Where(e => e.LocationId == locationId).Select(e => e.Name);

            var locationDetailsDto = new LocationDetailsDto
            {
                LocationId = Location.LocationId,
                Name = Location.Name,
                Address = Location.Address,
                City = Location.City,
                LocationURL = Location.LocationURL,
                Events = Events.ToList(),
                isPublished = Location.isPublished
            };

            return locationDetailsDto;
        }
        public LocationDetailsDto GetUnpublishedLocation(int locationId)
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
                Events = Events.ToList(),
                isPublished = Location.isPublished
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
        public async Task<bool> LocationExists(int locationId)
        {
            var location = await _context.Locations.AnyAsync(l => l.LocationId == locationId);
            return location;
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
        public async Task<bool> UpdateLocation(PutLocationDto locationToUpdate, int locationId)
        {
            if(locationToUpdate == null)
            {
                throw new ArgumentNullException(nameof(locationToUpdate));
            }

            var locationContext = await _context.Locations
                .FirstOrDefaultAsync(l => l.LocationId == locationId);

            if(locationContext == null)
            {
                throw new ArgumentNullException(nameof(locationToUpdate));
            }
            var events = await _context.Events.Where(e => locationToUpdate.EventId.Contains(e.EventId)).ToListAsync();

            var locationDto =  new Location
            {
                Name = locationToUpdate.Name,
                Address = locationToUpdate.Address,
                City = locationToUpdate.City,
                LocationURL = locationToUpdate.LocationURL,
                Events = events
            };

            _context.Locations.Update(locationDto);
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
        public async Task<bool> PublishLocation(int locationId)
        {
            var location = await _context.Locations.FirstOrDefaultAsync(l => l.LocationId == locationId);
            if(location == null)
            {
                return false;
            }

            location.isPublished = true;
            _context.Locations.Update(location);
            return await SaveAsync();
        }
    }
}

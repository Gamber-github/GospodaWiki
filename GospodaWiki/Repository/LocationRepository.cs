using Ganss.Xss;
using GospodaWiki.Data;
using GospodaWiki.Dto.Event;
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

        public ICollection<GetLocationsDto> GetLocations()
        {
            var locations = _context.Locations.Where(l => l.isPublished).ToList();
            var locationsDto = new List<GetLocationsDto>();

            foreach (var location in locations)
            {
                locationsDto.Add(new GetLocationsDto
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
        public ICollection<GetLocationsDto> GetUnpublishedLocations()
        {
            var locations = _context.Locations.ToList();
            var locationsDto = new List<GetLocationsDto>();

            foreach (var location in locations)
            {
                locationsDto.Add(new GetLocationsDto
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
            var locationContext = _context.Locations
            .Include(l => l.Events)
            .FirstOrDefault(l => l.LocationId == locationId && l.isPublished);
        
            var Events = _context.Events.Where(e => e.LocationId == locationContext.LocationId);    

            var locationDetailsDto = new LocationDetailsDto
            {
                LocationId = locationContext.LocationId,
                Name = locationContext.Name,
                Address = locationContext.Address,
                City = locationContext.City,
                LocationURL = locationContext.LocationURL,
                Events = Events.Select(e => new Dto.Event.GetEventReferenceDto
                {
                    EventId
                    = e.EventId,
                    Name = e.Name
                }).ToList(),
                isPublished = locationContext.isPublished
            };

            return locationDetailsDto;
        }
        public LocationDetailsDto GetUnpublishedLocation(int locationId)
        {
            var locationContext = _context.Locations
                .Include(l => l.Events)
                .FirstOrDefault(l => l.LocationId == locationId);

            var Events = _context.Events.Where(e => e.LocationId == locationContext.LocationId);

            var locationDetailsDto = new LocationDetailsDto
            {
                LocationId = locationContext.LocationId,
                Name = locationContext.Name,
                Address = locationContext.Address,
                City = locationContext.City,
                LocationURL = locationContext.LocationURL,
                Events = Events.Select(e => new Dto.Event.GetEventReferenceDto
                {
                    EventId = e.EventId,
                    Name = e.Name
                }).ToList(),
                isPublished = locationContext.isPublished
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
            var sanitizer = new HtmlSanitizer();

            if(locationToUpdate == null)
            {
                throw new ArgumentNullException(nameof(locationToUpdate));
            }

            var locationContext = await _context.Locations
                .Include(l => l.Events)
                .FirstOrDefaultAsync(l => l.LocationId == locationId);

            if(locationContext == null)
            {
                throw new ArgumentNullException(nameof(locationToUpdate));
            }
            var events = await _context.Events.Where(e => locationToUpdate.EventIds.Contains(e.EventId)).ToListAsync();

            if (locationToUpdate.EventIds != null)
            {
                locationContext.Events.Clear();
                if (locationToUpdate.EventIds.Any())
                {
                    var @Events = _context.Events
                        .Where(e => locationToUpdate.EventIds.Contains(e.EventId))
                        .ToList();

                    locationContext.Events = @Events;           
                }
            }

            locationContext.LocationId = locationId;
            locationContext.Name = sanitizer.Sanitize(locationToUpdate.Name);
            locationContext.Address = sanitizer.Sanitize(locationToUpdate.Address);
            locationContext.City = sanitizer.Sanitize(locationToUpdate.City);
            locationContext.LocationURL = sanitizer.Sanitize(locationToUpdate.LocationURL);

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
        public async Task<bool> PublishLocation(int locationId)
        {
            var location = await _context.Locations.FirstOrDefaultAsync(l => l.LocationId == locationId);
            if(location == null)
            {
                return false;
            }

            location.isPublished = !location.isPublished;
            _context.Locations.Update(location);
            return await SaveAsync();
        }

        public bool DeleteLocation(int locationId)
        {
            var location = _context.Locations.FirstOrDefault(l => l.LocationId == locationId);
            if(location == null)
            {
                return false;
            }

            _context.Locations.Remove(location);
            return Save();
           
        }
    }
}

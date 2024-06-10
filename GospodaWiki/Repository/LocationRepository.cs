using AutoMapper;
using GospodaWiki.Data;
using GospodaWiki.Dto.Location;
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
        public bool UpdateLocation(PatchLocationDto location)
        {
            if(location == null)
            {
                throw new ArgumentNullException(nameof(location));


            }

            var locationContext = _context.Locations.FirstOrDefault(l => l.LocationId == location.LocationId);

            if(locationContext == null)
            {
                throw new ArgumentNullException(nameof(location));
            }

            locationContext.Name = location.Name;
            locationContext.Address = location.Address;
            locationContext.City = location.City;
            locationContext.LocationURL = location.LocationURL;
            locationContext.Events = location.EventId != null ? location.EventId.Select(e => new Event { EventId = e }).ToList() : new List<Event>();


            _context.Locations.Update(locationContext);
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

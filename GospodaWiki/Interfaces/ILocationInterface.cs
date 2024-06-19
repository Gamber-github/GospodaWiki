using GospodaWiki.Dto.Location;
using GospodaWiki.Models;

namespace GospodaWiki.Interfaces
{
    public interface ILocationInterface
    {
        public ICollection<LocationsDto> GetLocations();
        public ICollection<LocationsDto> GetUnpublishedLocations();
        LocationDetailsDto GetLocation(int locationId);
        LocationDetailsDto GetUnpublishedLocation(int locationId);
        Task<bool> LocationExists(int locationId);
        bool CreateLocation(PostLocationDto location);
        Task<bool> UpdateLocation(PutLocationDto location, int locationId);
        Task<bool> PublishLocation(int locationId);
        bool DeleteLocation(Location location);
        bool Save();
        Task<bool> SaveAsync();
    }
}

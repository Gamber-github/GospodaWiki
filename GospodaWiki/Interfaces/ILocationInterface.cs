using GospodaWiki.Dto.Location;
using GospodaWiki.Models;

namespace GospodaWiki.Interfaces
{
    public interface ILocationInterface
    {
        public ICollection<LocationsDto> GetLocations();
        LocationDetailsDto GetLocation(int locationId);
        bool LocationExists(int locationId);
        bool CreateLocation(PostLocationDto location);
        Task<bool> UpdateLocation(PatchLocationDto location, int locationId);
        bool DeleteLocation(Location location);
        bool Save();
        Task<bool> SaveAsync();
    }
}

using GospodaWiki.Models;

namespace GospodaWiki.Interfaces
{
    public interface ILocationInterface
    {
        ICollection<Location> GetLocations();
        Location GetLocation(int locationId);
        bool LocationExists(int locationId);
        bool CreateLocation(Location location);
        bool Save();
        bool UpdateLocation(Location location);
        bool DeleteLocation(Location location);
    }
}

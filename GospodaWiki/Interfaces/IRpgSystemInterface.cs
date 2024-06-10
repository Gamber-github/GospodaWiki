using GospodaWiki.Dto.RpgSystem;
using GospodaWiki.Models;

namespace GospodaWiki.Interfaces
{
    public interface IRpgSystemInterface
    {
        ICollection<RpgSystemsDto> GetRpgSystems();
        RpgSystemDetailsDto GetRpgSystem(int id);
        RpgSystem GetRpgSystem(string name);
        bool RpgSystemExists(int rpgSystemId);
        bool CreateRpgSystem(RpgSystem rpgSystem);
        bool UpdateRpgSystem(PutRpgSystemDto rpgSystem);
        bool Save();
    }
}

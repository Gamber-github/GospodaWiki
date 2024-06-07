using GospodaWiki.Models;

namespace GospodaWiki.Interfaces
{
    public interface IRpgSystemInterface
    {
        ICollection<RpgSystem> GetRpgSystems();
        RpgSystem GetRpgSystem(int id);
        RpgSystem GetRpgSystem(string name);
        bool RpgSystemExists(int rpgSystemId);
        bool CreateRpgSystem(RpgSystem rpgSystem);
        bool UpdateRpgSystem(RpgSystem rpgSystem);
        bool DeleteRpgSystem(RpgSystem rpgSystem);
        bool Save();
    }
}

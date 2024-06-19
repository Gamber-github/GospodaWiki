using GospodaWiki.Dto.RpgSystem;
using GospodaWiki.Models;

namespace GospodaWiki.Interfaces
{
    public interface IRpgSystemInterface
    {
        ICollection<GetRpgSystemsDto> GetRpgSystems();
        ICollection<GetRpgSystemsDto> GetUnpublishedRpgSystems();
        GetRpgSystemDetailsDto GetRpgSystem(int rpgSystemId);
        GetRpgSystemDetailsDto GetUnpublishedRpgSystem(int rpgSystemId);
        bool RpgSystemExists(int rpgSystemId);
        bool CreateRpgSystem(PostRpgSystemDto rpgSystem);
        bool UpdateRpgSystem(PutRpgSystemDto rpgSystem, int rpgSystemId);
        bool PublishRpgSystem (int rpgSystemId);
        bool Save();
        Task<bool> SaveAsync();
    }
}

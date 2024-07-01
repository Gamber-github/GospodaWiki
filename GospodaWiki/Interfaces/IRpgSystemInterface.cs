using GospodaWiki.Dto.RpgSystem;
using GospodaWiki.Models;

namespace GospodaWiki.Interfaces
{
    public interface IRpgSystemInterface
    {
        public ICollection<GetRpgSystemsDto> GetRpgSystems();
        public ICollection<GetRpgSystemsDto> GetUnpublishedRpgSystems();
        public GetRpgSystemDetailsDto GetRpgSystem(int rpgSystemId);
        public GetRpgSystemDetailsDto GetUnpublishedRpgSystem(int rpgSystemId);
        public bool RpgSystemExists(int rpgSystemId);
        public bool CreateRpgSystem(PostRpgSystemDto rpgSystem);
        public bool UpdateRpgSystem(PutRpgSystemDto rpgSystem, int rpgSystemId);
        public bool PublishRpgSystem (int rpgSystemId);
        public bool Save();
    }
}

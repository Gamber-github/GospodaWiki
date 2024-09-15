using GospodaWiki.Dto.Adventure;

namespace GospodaWiki.Interfaces
{
    public interface IAdventureInterface
    {
        ICollection<GetAdventuresDto> GetAdventures();
        ICollection<GetAdventuresDto> GetUnpublishedAdventures();
        GetAdventureDetailsDto GetAdventure(int id);
        GetAdventureDetailsDto GetUnpublishedAdventure(int adventureId);
        bool AdventureExists(int adventureId);
        bool CreateAdventure(PostAdventureDto adventure);
        bool UpdateAdventure(PutAdventureDto adventure, int adventureId);
        bool Save();
        bool PublishAdventure(int adventureId);
        bool DeleteAdventure(int adventureId);
    }
}

using GospodaWiki.Dto.Character;
namespace GospodaWiki.Interfaces
{
    public interface ICharacterInterface
    {
        ICollection<CharactersDto> GetCharacters();
        ICollection<CharactersDto> GetUnpublishedCharacters();
        CharacterDetailsDto GetCharacter(int id);
        CharacterDetailsDto GetUnpublishedCharacter(int characterId);
        bool CharacterExists(int characterId);
        bool CreateCharacter(PostCharacterDto character);
        bool UpdateCharacter(PutCharacterDto character, int characterId);
        bool Save();
        Task<bool> SaveAsync();
        bool PublishCharacter(int characterId);
    }
}

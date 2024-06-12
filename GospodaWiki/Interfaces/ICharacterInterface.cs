using GospodaWiki.Dto.Character;
using GospodaWiki.Models;

namespace GospodaWiki.Interfaces
{
    public interface ICharacterInterface
    {
        ICollection<CharactersDto> GetCharacters();
        CharacterDetailsDto GetCharacter(int id);
        bool CharacterExists(int characterId);
        bool CreateCharacter(PostCharacterDto character);
        Task<bool> UpdateCharacter(PatchCharacterDto character, int characterId);
        bool Save();
        Task<bool> SaveAsync();
    }
}

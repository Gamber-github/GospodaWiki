using GospodaWiki.Dto;
using GospodaWiki.Models;

namespace GospodaWiki.Interfaces
{
    public interface ICharacterInterface
    {
        ICollection<Character> GetCharacters();
        Character GetCharacter(int id);
        bool CharacterExists(int characterId);
        bool CreateCharacter(Character character);
        bool UpdateCharacter(Character character);
        bool Save();
    }
}

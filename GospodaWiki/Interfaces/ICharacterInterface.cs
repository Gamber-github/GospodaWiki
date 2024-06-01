using GospodaWiki.Models;

namespace GospodaWiki.Interfaces
{
    public interface ICharacterInterface
    {
        ICollection<Character> GetCharacters();
        Character GetCharacter(int id);
        Character GetCharcater(string name);
        bool CharacterExists(int characterId);
        bool CreateCharacter(Character character);
        bool UpdateCharacter(Character character);
        bool Save();
    }
}

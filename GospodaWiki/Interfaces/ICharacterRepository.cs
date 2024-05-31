using GospodaWiki.Models;

namespace GospodaWiki.Interfaces
{
    public interface ICharacterRepository
    {
        ICollection<Character> GetCharacters();
        Character GetCharacter(int id);
        Character GetCharcater(string name);
        bool CharacterExists(int characterId);
    }
}

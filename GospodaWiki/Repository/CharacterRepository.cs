using GospodaWiki.Data;
using GospodaWiki.Interfaces;
using GospodaWiki.Models;

namespace GospodaWiki.Repository
{
    public class CharacterRepository : ICharacterInterface
    {
        private readonly DataContext _context;
        public CharacterRepository(DataContext context)
        {
            _context = context;
        }
        public ICollection<Character> GetCharacters()
        {
            return _context.Characters.OrderBy(p => p.Id).ToList();
        }
        public Character GetCharacter(int id)
        {
            return _context.Characters.Where(p => p.Id == id).FirstOrDefault();
        } 
        public Character GetCharcater(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentException("Name cannot be empty or null.");
            }

            return _context.Characters.FirstOrDefault(c => $"{c.FirstName} {c.LastName}" == name);
        }
        public bool CharacterExists(int characterId)
        {
            return _context.Characters.Any(p => p.Id == characterId);
        }
    }
}

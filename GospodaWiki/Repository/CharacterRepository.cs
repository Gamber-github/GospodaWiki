using GospodaWiki.Data;
using GospodaWiki.Interfaces;
using GospodaWiki.Models;
using GospodaWiki.Dto;

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
                return _context.Characters.ToList();
        }
        public Character GetCharacter(int id)
        {
            var query = _context.Characters
                .Where(p => p.CharacterId == id)
                .Select(p => new Character
                {
                    CharacterId = p.CharacterId,
                    FirstName = p.FirstName,
                    LastName = p.LastName,
                    Age = p.Age,
                    Birthday = p.Birthday,
                    Biography = p.Biography,
                    City = p.City,
                    Country = new Country
                    {
                        CountryId = p.Country.CountryId,
                        Name = p.Country.Name,
                    },
                    RpgSystem = new RpgSystem
                    {
                        RpgSystemId = p.RpgSystem.RpgSystemId,
                        Name = p.RpgSystem.Name,
                        Description = p.RpgSystem.Description
                    },
                    Abilities = p.Abilities.Select(a => new Ability
                    {
                        AbilityId = a.AbilityId,
                        Name = a.Name,
                        Description = a.Description,
                        Type = a.Type
                    }).ToList()
                }).ToList();

             return query.FirstOrDefault();
        } 
        public bool CharacterExists(int characterId)
        {
            return _context.Characters.Any(p => p.CharacterId == characterId);
        }
        public bool CreateCharacter(Character character)
        {
            if (character == null)
            {
                throw new ArgumentNullException(nameof(character));
            }

            _context.Characters.Add(character);
            return Save();
        }
        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved >= 0;
        }

        public bool UpdateCharacter(Character character)
        {
            if (character == null)
            {
                throw new ArgumentNullException(nameof(character));
            }

            _context.Characters.Update(character);
            return Save();
        }
    }
}

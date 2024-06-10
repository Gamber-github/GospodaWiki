using GospodaWiki.Data;
using GospodaWiki.Dto.Character;
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
        public ICollection<CharactersDto> GetCharacters()
        {
            var characters = _context.Characters.ToList();
            var charactersDto = new List<CharactersDto>();
            foreach (var character in characters) {
                var series = _context.Series.FirstOrDefault(s => s.SeriesId == character.SeriesId);
                var rpgSystem = _context.RpgSystems.FirstOrDefault(r => r.RpgSystemId == character.RpgSystemId);
                charactersDto.Add(new CharactersDto
                {
                    CharacterId = character.CharacterId,
                    FullName = $"{character.FirstName} {character.LastName}",
                    SeriesName = series != null ? series.Name : "",
                    RpgSystemName = rpgSystem != null ? rpgSystem.Name : ""
                });
            }

            return charactersDto;
        }
        public CharacterDetailsDto GetCharacter(int id)
        {
            var character = _context.Characters.FirstOrDefault(p => p.CharacterId == id);
            var series = _context.Series.FirstOrDefault(s => s.SeriesId == character.SeriesId);
            var tags = _context.Tags.Where(t => t.Characters.Any(c => c.CharacterId == character.CharacterId)).Select(t => t.Name);
            var items = _context.Items.Where(t => t.Characters.Any(c => c.CharacterId == character.CharacterId)).Select(i => i.Name);
            var rpgSystem = _context.RpgSystems.FirstOrDefault(r => r.RpgSystemId == character.RpgSystemId);

            var characterDetailsDto = new CharacterDetailsDto
            {
                CharacterId = character.CharacterId,
                FirstName = character.FirstName,
                LastName = character.LastName,
                ImagePath = character.ImagePath,
                Age = character.Age,
                Description = character.Description,
                Series = series != null ? series.Name : "" ,
                RpgSystem = rpgSystem != null ? rpgSystem.Name : "",
                Tags = tags.ToList(),
                Items = items.ToList()
            };

            return characterDetailsDto;
        }
        public bool CharacterExists(int characterId)
        {
            return _context.Characters.Any(p => p.CharacterId == characterId);
        }
        public bool CreateCharacter(PostCharacterDto characterCreate)
        {
            if (characterCreate == null)
            {
                throw new ArgumentNullException(nameof(characterCreate));
            }

            var character = new Character
            {
                FirstName = characterCreate.FirstName,
                LastName = characterCreate.LastName,
                ImagePath = characterCreate.ImagePath,
                Age = characterCreate.Age,
                Description = characterCreate.Description,
                SeriesId = characterCreate.SeriesId,
                RpgSystemId = characterCreate.RpgSystemId 
            };

            _context.Characters.Add(character);
            return Save();
        }
        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved >= 0;
        }

        public bool UpdateCharacter(PatchCharacterDto character, int characterId)
        {
            if (character == null)
            {
                throw new ArgumentNullException(nameof(character));
            }

            var characterToUpdate = _context.Characters.FirstOrDefault(p => p.CharacterId == characterId);

            if (characterToUpdate == null)
            {
                throw new ArgumentNullException(nameof(character));
            }

            characterToUpdate.FirstName = character.FirstName ?? characterToUpdate.FirstName;
            characterToUpdate.LastName = character.LastName ?? characterToUpdate.LastName;
            characterToUpdate.ImagePath = character.ImagePath ?? characterToUpdate.ImagePath;
            characterToUpdate.Age = character.Age ?? characterToUpdate.Age;
            characterToUpdate.Description = character.Description ?? characterToUpdate.Description;
            characterToUpdate.SeriesId = character.Series ?? characterToUpdate.SeriesId;
            characterToUpdate.RpgSystemId = character.RpgSystemId ?? characterToUpdate.RpgSystemId;
            characterToUpdate.Tags = _context.Tags.Where(t => character.TagsId.Contains(t.TagId)).ToList();
            characterToUpdate.Items = _context.Items.Where(i => character.ItemsId.Contains(i.ItemId)).ToList();

            _context.Characters.Update(characterToUpdate);
            return Save();
        }
    }
}

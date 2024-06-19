using GospodaWiki.Data;
using GospodaWiki.Dto.Character;
using GospodaWiki.Interfaces;
using GospodaWiki.Models;
using Microsoft.EntityFrameworkCore;

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
            var characters = _context.Characters
                                     .Where(c => c.isPublished)
                                     .Include(c => c.Series)
                                     .Include(c => c.RpgSystem)
                                     .ToList();

            var charactersDto = characters.Select(character => new CharactersDto
            {
                CharacterId = character.CharacterId,
                FullName = $"{character.FirstName} {character.LastName}",
                SeriesName = character.Series?.Name ?? "",
                RpgSystemName = character.RpgSystem?.Name ?? "",
                isPublished = character.isPublished
            }).ToList();

            return charactersDto;
        }
        public ICollection<CharactersDto> GetUnpublishedCharacters()
        {
            var characters =  _context.Characters.ToList();
            var charactersDto = new List<CharactersDto>();
            foreach (var character in characters)
            {
                var series =  _context.Series.FirstOrDefault(s => s.SeriesId == character.SeriesId);
                var rpgSystem =  _context.RpgSystems.FirstOrDefault(r => r.RpgSystemId == character.RpgSystemId);
                charactersDto.Add(new CharactersDto
                {
                    CharacterId = character.CharacterId,
                    FullName = $"{character.FirstName} {character.LastName}",
                    SeriesName = series != null ? series.Name : "",
                    RpgSystemName = rpgSystem != null ? rpgSystem.Name : "",
                    isPublished = character.isPublished
                });
            }

            return charactersDto;
        }
        public CharacterDetailsDto GetCharacter(int id)
        {
            var character = _context.Characters.FirstOrDefault(p => p.CharacterId == id && p.isPublished);

            if (character == null)
            {
                return null;
            }

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
                Items = items.ToList(),
                isPublished = character.isPublished
            };

            return characterDetailsDto;
        }
        public CharacterDetailsDto GetUnpublishedCharacter(int characterId)
        {
            var character =  _context.Characters.Where(p => p.CharacterId == characterId).FirstOrDefault();
            var series =  _context.Series.FirstOrDefault(s => s.SeriesId == character.SeriesId);
            var tags = _context.Tags.Where(t => t.Characters.Any(c => c.CharacterId == character.CharacterId)).Select(t => t.Name);
            var items = _context.Items.Where(t => t.Characters.Any(c => c.CharacterId == character.CharacterId)).Select(i => i.Name);
            var rpgSystem =  _context.RpgSystems.FirstOrDefault(r => r.RpgSystemId == character.RpgSystemId);

            var characterDetailsDto = new CharacterDetailsDto
            {
                CharacterId = character.CharacterId,
                FirstName = character.FirstName,
                LastName = character.LastName,
                ImagePath = character.ImagePath,
                Age = character.Age,
                Description = character.Description,
                Series = series != null ? series.Name : "",
                RpgSystem = rpgSystem != null ? rpgSystem.Name : "",
                Tags = tags.ToList(),
                Items = items.ToList(),
                isPublished = character.isPublished
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
        public async Task<bool> SaveAsync()
        {
            var saved = await _context.SaveChangesAsync();
            return saved >= 0;
        }
        public bool UpdateCharacter(PutCharacterDto character, int characterId)
        {
            if (character == null)
            {
                throw new ArgumentNullException(nameof(character));
            }

            var characterContext = _context.Characters
                .Include(c=> c.Tags)
                .Include(i => i.Items)
                .FirstOrDefault(p => p.CharacterId == characterId);

            if (characterContext == null)
            {
                throw new ArgumentNullException(nameof(character));
            }

            characterContext.Tags.Clear();
            characterContext.Items.Clear();

            var series =  _context.Series.FirstOrDefault(s => s.SeriesId == character.SeriesId);
            var tags =  _context.Tags.Where(t => character.TagsId.Contains(t.TagId)).ToList();
            foreach (var tag in tags)
            {
                characterContext.Tags.Add(tag);
            }

            var items =  _context.Items.Where(i => character.ItemsId.Contains(i.ItemId)).ToList();
            foreach (var item in items)
            {
                characterContext.Items.Add(item);
            }

            characterContext.CharacterId = characterId;
            characterContext.FirstName = character.FirstName;
            characterContext.LastName = character.LastName;
            characterContext.ImagePath = character.ImagePath;
            characterContext.Age = character.Age;
            characterContext.Description = character.Description;
            characterContext.SeriesId = series.SeriesId;
            characterContext.RpgSystemId = character.RpgSystemId;

            _context.Characters.Update(characterContext);
            return Save();
        }
        public bool PublishCharacter(int characterId)
        {
            var character =  _context.Characters.FirstOrDefault(p => p.CharacterId == characterId);
            if (character == null)
            {
                return false;
            }

            character.isPublished = true;
            _context.Characters.Update(character);
            return Save();
        }
    }
}

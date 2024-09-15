using Ganss.Xss;
using GospodaWiki.Data;
using GospodaWiki.Dto.Adventure;
using GospodaWiki.Dto.Character;
using GospodaWiki.Dto.RpgSystem;
using GospodaWiki.Dto.Series;
using GospodaWiki.Dto.Tag;
using GospodaWiki.Interfaces;
using GospodaWiki.Models;
using Microsoft.EntityFrameworkCore;

namespace GospodaWiki.Repository
{
    public class AdventureRepository : IAdventureInterface
    {
        private readonly DataContext _context;
        public AdventureRepository(DataContext context)
        {
            _context = context;
        }
        public bool AdventureExists(int adventureId)
        {
            return _context.Adventures.Any(a => a.AdventureId == adventureId);
        }
        public bool Save()
        {
            return _context.SaveChanges() >= 0;
        }
        public bool DeleteAdventure(int adventureId)
        {
            var adventureContext = _context.Adventures.FirstOrDefault(a => a.AdventureId == adventureId);
            if (adventureContext == null)
            {
                return false;
            }
            _context.Adventures.Remove(adventureContext);
            return Save();
        }
        public ICollection<GetAdventuresDto> GetAdventures()
        {
            var adventures = _context.Adventures
                .Where(a => a.isPublished)
                .ToList();

            var adventuresDto = new List<GetAdventuresDto>();

            foreach (var a in adventures)
            {
                adventuresDto.Add(new GetAdventuresDto
                {
                    AdventureId = a.AdventureId,
                    Title = a.Title,
                    isPublished = a.isPublished
                });
            }

            return adventuresDto;
        }
        public ICollection<GetAdventuresDto> GetUnpublishedAdventures()
        {
            var adventures = _context.Adventures
                .ToList();

            var adventuresDto = new List<GetAdventuresDto>();

            foreach (var a in adventures)
            {
                adventuresDto.Add(new GetAdventuresDto
                {
                    AdventureId = a.AdventureId,
                    Title = a.Title,
                    isPublished = a.isPublished
                });
            }

            return adventuresDto;
        }
        public GetAdventureDetailsDto GetAdventure(int adventureId)
        {
            var adventureContext = _context.Adventures
                .Where(a => a.isPublished)
                .Include(a => a.Characters)
                .Include(a => a.RpgSystem)
                .Include(a => a.Series)
                .Include(a => a.Tags)
                .FirstOrDefault(a => a.AdventureId == adventureId);

            var characters = _context.Characters.Where(c => c.Adventures.Any(a => a.AdventureId == adventureContext.AdventureId)).Select(c => new GetCharacterReferenceDto { CharacterId = c.CharacterId, FirstName = c.FirstName , LastName = c.LastName }).ToList();

            var tags = _context.Tags.Where(t => t.Adventures.Any(a => a.AdventureId == adventureContext.AdventureId)).Select(t => new GetTagDetailsDto { TagId = t.TagId, Name = t.Name }).ToList();
            var series = _context.Series.FirstOrDefault(s => s.SeriesId == adventureContext.SeriesId);
            var rpgSystem = _context.RpgSystems.FirstOrDefault(r => r.RpgSystemId == adventureContext.RpgSystemId);

            var adventureDetailsDto = new GetAdventureDetailsDto
            {
                AdventureId = adventureContext.AdventureId,
                Title = adventureContext.Title,
                Description = adventureContext.Description,
                Characters = characters,
                Tags = tags,
                Series = series != null ? new GetSeriesReferenceDto
                {
                    Name = series.Name,
                    SeriesId = series.SeriesId
                } : new GetSeriesReferenceDto(),
                RpgSystem = rpgSystem != null ? new GetRpgSystemReferenceDto
                {
                    Name = rpgSystem.Name,
                    RpgSystemId = rpgSystem.RpgSystemId
                } : new GetRpgSystemReferenceDto(),
                isPublished = adventureContext.isPublished
            };

            return adventureDetailsDto;
        }
        public GetAdventureDetailsDto GetUnpublishedAdventure(int adventureId)
        {
            var adventureContext = _context.Adventures
                .Include(a => a.Characters)
                .Include(a => a.RpgSystem)
                .Include(a => a.Series)
                .Include(a => a.Tags)
                .FirstOrDefault(a => a.AdventureId == adventureId);

            var characters = _context.Characters.Where(c => c.Adventures.Any(a => a.AdventureId == adventureContext.AdventureId)).Select(c => new GetCharacterReferenceDto { CharacterId = c.CharacterId, FirstName = c.FirstName, LastName = c.LastName }).ToList();
            var tags = _context.Tags.Where(t => t.Adventures.Any(a => a.AdventureId == adventureContext.AdventureId)).Select(t => new GetTagDetailsDto { TagId = t.TagId, Name = t.Name }).ToList();
            var series = _context.Series.FirstOrDefault(s => s.SeriesId == adventureContext.SeriesId);
            var rpgSystem = _context.RpgSystems.FirstOrDefault(r => r.RpgSystemId == adventureContext.RpgSystemId);

            var adventureDetailsDto = new GetAdventureDetailsDto
            {
                AdventureId = adventureContext.AdventureId,
                Title = adventureContext.Title,
                Description = adventureContext.Description,
                Characters = characters,
                Tags = tags,
                Series = series != null ? new GetSeriesReferenceDto
                {
                    Name = series.Name,
                    SeriesId = series.SeriesId
                } : new GetSeriesReferenceDto(),
                RpgSystem = rpgSystem != null ? new GetRpgSystemReferenceDto
                {
                    Name = rpgSystem.Name,
                    RpgSystemId = rpgSystem.RpgSystemId
                } : new GetRpgSystemReferenceDto(),
                isPublished = adventureContext.isPublished
            };

            return adventureDetailsDto;
        }
        public bool PublishAdventure(int adventureId)
        {
            var adventureContext = _context.Adventures.FirstOrDefault(a => a.AdventureId == adventureId);

            if (adventureContext == null)
            {
                return false;
            }

            adventureContext.isPublished = !adventureContext.isPublished;
            _context.Adventures.Update(adventureContext);
            return Save();
        }
        public bool UpdateAdventure(PutAdventureDto adventureToUpdate, int adventureId)
        {
            var sanitizer = new HtmlSanitizer();

            if(adventureToUpdate == null)
            {
                throw new ArgumentNullException(nameof(adventureToUpdate));
            }

            var adventureContext = _context.Adventures
                .Include(a => a.Characters)
                .Include(a => a.Tags)
                .Include(a => a.Series)
                .Include(a => a.RpgSystem)
                .FirstOrDefault(a => a.AdventureId == adventureId);

            if (adventureContext == null)
            {
                throw new ArgumentNullException(nameof(adventureContext));
            }

            adventureContext.Title = sanitizer.Sanitize(adventureToUpdate.Title);
            adventureContext.Description = sanitizer.Sanitize(adventureToUpdate.Description);
            adventureContext.RpgSystemId = adventureToUpdate.RpgSystemId;
            adventureContext.SeriesId = adventureToUpdate.SeriesId;

            if(adventureToUpdate.CharactersIds != null)
            {
                adventureContext.Characters?.Clear();
                if (adventureToUpdate.CharactersIds.Any())
                {
                    var characters = _context.Characters
                        .Where(c => adventureToUpdate.CharactersIds.Contains(c.CharacterId))
                        .ToList();
                    adventureContext.Characters = characters;
                }
            }
            if (adventureToUpdate.TagsIds != null)
            {
                adventureContext.Tags?.Clear();
                if (adventureToUpdate.TagsIds.Any())
                {
                    var tags = _context.Tags
                        .Where(t => adventureToUpdate.TagsIds.Contains(t.TagId))
                        .ToList();
                    adventureContext.Tags = tags;
                }
            }

            _context.Adventures.Update(adventureContext);
            return Save();
        }
        public bool CreateAdventure(PostAdventureDto adventureCreate)
        {
            var sanitizer = new HtmlSanitizer();

            if (adventureCreate == null)
            {
                return false;
            }

            var adventure = new Adventure
            {
                Title = sanitizer.Sanitize(adventureCreate.Title),
                Description = sanitizer.Sanitize(adventureCreate.Description),
                RpgSystemId = adventureCreate.RpgSystemId,
                SeriesId = adventureCreate.SeriesId,
            };

            _context.Adventures.Add(adventure);
            return Save();
        }
    }
}

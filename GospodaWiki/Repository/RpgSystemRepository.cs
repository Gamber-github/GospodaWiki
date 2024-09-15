using Ganss.Xss;
using GospodaWiki.Data;
using GospodaWiki.Dto.Character;
using GospodaWiki.Dto.RpgSystem;
using GospodaWiki.Dto.Series;
using GospodaWiki.Dto.Tag;
using GospodaWiki.Interfaces;
using GospodaWiki.Models;
using Microsoft.EntityFrameworkCore;

namespace GospodaWiki.Repository
{
    public class RpgSystemRepository : IRpgSystemInterface
    {
        private readonly DataContext _context;

        public RpgSystemRepository(DataContext context)
        {
            _context = context;
        }

        public ICollection<GetRpgSystemsDto> GetRpgSystems()
        {
            var rpgSystems = _context.RpgSystems
                .Where(rpg => rpg.isPublished)
                .ToList();
            var rpgSystemsDto = new List<GetRpgSystemsDto>();

            foreach (var rpgSystem in rpgSystems)
            {
                rpgSystemsDto.Add(new GetRpgSystemsDto
                {
                    RpgSystemId = rpgSystem.RpgSystemId,
                    Name = rpgSystem.Name,
                    isPublished = rpgSystem.isPublished
                });
            }

            return rpgSystemsDto;
        }

        public GetRpgSystemDetailsDto GetRpgSystem(int id)
        {
            var rpgSystemContext = _context.RpgSystems
                .Where(rpg => rpg.isPublished)
                .Include(r => r.Characters)
                .Include(r => r.Series)
                .Include(r => r.Tags)
                .FirstOrDefault(c => c.RpgSystemId == id);

            var tags = _context.Tags.Where(t => t.RpgSystems.Any(r => r.RpgSystemId == rpgSystemContext.RpgSystemId)).Where(t => t.isPublished).Select(t => new TagReferenceDTO { Name = t.Name, TagId = t.TagId });
            var characters = _context.Characters.Where(c => c.RpgSystemId == rpgSystemContext.RpgSystemId).Where(c => c.isPublished).Select(c => new GetCharacterReferenceDto {   CharacterId = c.CharacterId, FirstName = c.FirstName , LastName = c.LastName}).ToList();
            var series = _context.Series.Where(s => s.RpgSystemId == rpgSystemContext.RpgSystemId).Where(s => s.isPublished).Select(s => new GetSeriesReferenceDto { Name = s.Name , SeriesId = s.SeriesId}).ToList();

            var rpgSystemDetailsDto = new GetRpgSystemDetailsDto
            {
                RpgSystemId = rpgSystemContext.RpgSystemId,
                Name = rpgSystemContext.Name,
                Description = rpgSystemContext.Description,
                ImagePath = rpgSystemContext.ImagePath,
                Characters = characters,
                Series = series.ToList(),
                Tags = tags.ToList(),

                isPublished = rpgSystemContext.isPublished
            };

            return rpgSystemDetailsDto;
        }

        public bool RpgSystemExists(int rpgSystemId)
        {
            return _context.RpgSystems.Any(p => p.RpgSystemId == rpgSystemId);
        }

        public bool CreateRpgSystem(PostRpgSystemDto rpgSystem)
        {
            if (rpgSystem == null)
            {
                throw new ArgumentNullException(nameof(rpgSystem));
            }

            var rpgSystemDto = new RpgSystem
            {
                Name = rpgSystem.Name,
                Description = rpgSystem.Description
            };

            _context.RpgSystems.Add(rpgSystemDto);
            return Save();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved >= 0;
        }

        public bool UpdateRpgSystem(PutRpgSystemDto rpgSystemToUpdate, int rpgSystemId)
        {
            var sanitizer = new HtmlSanitizer();

            if (rpgSystemToUpdate == null)
            {
                throw new ArgumentNullException(nameof(rpgSystemToUpdate));
            }

            var rpgSystemContext = _context.RpgSystems
                .Include(c => c.Characters)
                .Include(c => c.Series)
                .Include(c => c.Tags)
                .FirstOrDefault(c => c.RpgSystemId == rpgSystemId);

            if (rpgSystemContext == null)
            {
                throw new ArgumentNullException(nameof(rpgSystemToUpdate));
            }

            rpgSystemContext.Name = sanitizer.Sanitize(rpgSystemToUpdate.Name);
            rpgSystemContext.ImagePath = rpgSystemToUpdate.ImagePath;
            rpgSystemContext.Description = sanitizer.Sanitize(rpgSystemToUpdate.Description);

            if (rpgSystemToUpdate.CharactersIds != null)
            {
                rpgSystemContext.Characters.Clear();
                var characters = _context.Characters
                    .Where(c => rpgSystemToUpdate.CharactersIds.Contains(c.CharacterId))
                    .ToList();
                rpgSystemContext.Characters = characters;
            }

            if (rpgSystemToUpdate.SeriesIds != null)
            {
                rpgSystemContext.Series.Clear();
                var series = _context.Series
                    .Where(s => rpgSystemToUpdate.SeriesIds.Contains(s.SeriesId))
                    .ToList();
                rpgSystemContext.Series = series;
            }

            if (rpgSystemToUpdate.TagsIds != null)
            {
                rpgSystemContext.Tags.Clear();
                if (rpgSystemToUpdate.TagsIds.Count > 0)
                {
                    var tags = _context.Tags
                        .Where(t => rpgSystemToUpdate.TagsIds.Contains(t.TagId))
                        .ToList();
                    rpgSystemContext.Tags = tags;
                }
            }

            _context.RpgSystems.Update(rpgSystemContext);
            return Save();
        }

        public bool DeleteRpgSystem(RpgSystem rpgSystem)
        {
            if (rpgSystem == null)
            {
                throw new ArgumentNullException(nameof(rpgSystem));
            }

            _context.RpgSystems.Remove(rpgSystem);
            return Save();
        }

        public ICollection<GetRpgSystemsDto> GetUnpublishedRpgSystems()
        {
            var rpgSystems = _context.RpgSystems
                .ToList();

            var rpgSystemsDto = new List<GetRpgSystemsDto>();

            foreach (var rpgSystem in rpgSystems)
            {
                rpgSystemsDto.Add(new GetRpgSystemsDto
                {
                    RpgSystemId = rpgSystem.RpgSystemId,
                    Name = rpgSystem.Name,
                    isPublished = rpgSystem.isPublished
                });
            }

            return rpgSystemsDto;
        }

        public GetRpgSystemDetailsDto GetUnpublishedRpgSystem(int rpgSystemId)
        {
            var rpgSystemContext = _context.RpgSystems
                .Include(r => r.Characters)
                .Include(r => r.Series)
                .Include(r => r.Tags)
                .FirstOrDefault(c => c.RpgSystemId == rpgSystemId);


            var tags = _context.Tags.Where(t => t.RpgSystems.Any(r => r.RpgSystemId == rpgSystemContext.RpgSystemId)).Select(t => new TagReferenceDTO { Name = t.Name, TagId = t.TagId });
            var characters = _context.Characters.Where(c => c.RpgSystemId == rpgSystemContext.RpgSystemId).Select(c => new GetCharacterReferenceDto {  CharacterId = c.CharacterId, FirstName = c.FirstName , LastName = c.LastName }).ToList();
            var series = _context.Series.Where(s => s.RpgSystemId == rpgSystemContext.RpgSystemId).Select(s => new GetSeriesReferenceDto { Name = s.Name, SeriesId = s.SeriesId }).ToList();


            var rpgSystemDetailsDto = new GetRpgSystemDetailsDto
            {
                RpgSystemId = rpgSystemContext.RpgSystemId,
                Name = rpgSystemContext.Name,
                Description = rpgSystemContext.Description,
                ImagePath = rpgSystemContext.ImagePath,
                Characters = characters,
                Series = series,
                Tags = tags.ToList(),
                isPublished = rpgSystemContext.isPublished
            };

            return rpgSystemDetailsDto;
        }

        public bool PublishRpgSystem(int rpgSystemId)
        {
            var rpgSystem = _context.RpgSystems.FirstOrDefault(r => r.RpgSystemId == rpgSystemId);
            if (rpgSystem == null)
            {
                return false;
            }

            rpgSystem.isPublished = !rpgSystem.isPublished;
            _context.RpgSystems.Update(rpgSystem);
            return Save();
        }

        public bool DeleteRpgSystem(int rpgSystemId)
        {
            var rpgSystem = _context.RpgSystems.FirstOrDefault(r => r.RpgSystemId == rpgSystemId);

            if (rpgSystem == null)
            {
                return false;
            }

            _context.RpgSystems.Remove(rpgSystem);
            return Save();
        }
    }
}

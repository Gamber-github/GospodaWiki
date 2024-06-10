using GospodaWiki.Data;
using GospodaWiki.Dto.RpgSystem;
using GospodaWiki.Interfaces;
using GospodaWiki.Models;

namespace GospodaWiki.Repository
{
    public class RpgSystemRepository : IRpgSystemInterface
    {
        private readonly DataContext _context;

        public RpgSystemRepository(DataContext context)
        {
            _context = context;
        }

        public ICollection<RpgSystemsDto> GetRpgSystems()
        {
            var rpgSystems = _context.RpgSystems.ToList();
            var rpgSystemsDto = new List<RpgSystemsDto>();

            foreach (var rpgSystem in rpgSystems)
            {
                rpgSystemsDto.Add(new RpgSystemsDto
                {
                    RpgSystemId = rpgSystem.RpgSystemId,
                    Name = rpgSystem.Name
                });
            }

            return rpgSystemsDto;
        }
        public RpgSystemDetailsDto GetRpgSystem(int id)
        {
            var rpgSystemContext = _context.RpgSystems.FirstOrDefault(c => c.RpgSystemId == id);
            var story = _context.Stories.FirstOrDefault(s => s.RpgSystemId == rpgSystemContext.RpgSystemId);
            var tags = _context.Tags.Where(t => t.RpgSystems.Any(r => r.RpgSystemId == rpgSystemContext.RpgSystemId)).Select(t => t.Name);
            var characters = _context.Characters.Where(c => c.RpgSystemId == rpgSystemContext.RpgSystemId).Select(c => c.FirstName + " " + c.LastName).ToList();
            var series = _context.Series.Where(s => s.RpgSystemId == rpgSystemContext.RpgSystemId).Select(s => s.Name).ToList();

            var rpgSystemDetailsDto = new RpgSystemDetailsDto
            {
                RpgSystemId = rpgSystemContext.RpgSystemId,
                Name = rpgSystemContext.Name,
                Description = rpgSystemContext.Description,
                StoryName = story?.Name,
                ImagePath = rpgSystemContext.ImagePath,
                Characters = characters.ToArray(),
                Series = series.ToList(),
                Tags = tags.ToList()
            };

            return rpgSystemDetailsDto;
        }
        public RpgSystem GetRpgSystem(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentException("Name cannot be empty or null.");
            }

            return _context.RpgSystems.FirstOrDefault(c => c.Name == name);
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
        public bool UpdateRpgSystem(PatchRpgSystemDto rpgSystemUpdate, int rpgSystemId)
        {
            if (rpgSystemUpdate == null)
            {
                throw new ArgumentNullException(nameof(rpgSystemUpdate));
            }

            var rpgSystemContext = _context.RpgSystems.FirstOrDefault(c => c.RpgSystemId == rpgSystemId);

            if (rpgSystemContext == null)
            {
                throw new ArgumentNullException(nameof(rpgSystemUpdate));
            }

            rpgSystemContext.Name = rpgSystemUpdate.Name ?? rpgSystemContext.Name;
            rpgSystemContext.Description = rpgSystemUpdate.Description ?? rpgSystemContext.Description;
            rpgSystemContext.ImagePath = rpgSystemUpdate.ImagePath ?? rpgSystemContext.ImagePath;
            rpgSystemContext.StoryName = rpgSystemUpdate.StoryName ?? rpgSystemContext.StoryName;
            rpgSystemContext.Tags = rpgSystemUpdate.TagsIds.Count > 0 ? rpgSystemUpdate.TagsIds.Select(t => new Tag { TagId = t }).ToList() : [];
            rpgSystemContext.Characters = rpgSystemUpdate.CharactersIds != null ? rpgSystemUpdate.CharactersIds.Select(c => new Character { CharacterId = c }).ToList() : [];
            rpgSystemContext.Series = rpgSystemUpdate.SeriesIds != null ? rpgSystemUpdate.SeriesIds.Select(s => new Series { SeriesId = s }).ToList() : [];


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
    }
}

using GospodaWiki.Data;
using GospodaWiki.Dto.RpgSystem;
using GospodaWiki.Dto.Tag;
using GospodaWiki.Interfaces;
using GospodaWiki.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

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
            var tags = _context.Tags.Where(t => t.RpgSystems.Any(r => r.RpgSystemId == rpgSystemContext.RpgSystemId)).Select(t => new TagDetailsDto { Name = t.Name, TagId = t.TagId });
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
        public async Task<bool> UpdateRpgSystem(PatchRpgSystemDto rpgSystemToUpdate, int rpgSystemId)
        {
            if (rpgSystemToUpdate == null)
            {
                throw new ArgumentNullException(nameof(rpgSystemToUpdate));
            }

            var rpgSystemContext = await _context.RpgSystems
                .Include(c => c.Characters)
                .Include(c => c.Series)
                .Include(c => c.Tags) 
                .FirstOrDefaultAsync(c => c.RpgSystemId == rpgSystemId);

            if (rpgSystemContext == null)
            {
                throw new ArgumentNullException(nameof(rpgSystemToUpdate));
            }

            rpgSystemContext.Name = rpgSystemToUpdate.Name ?? rpgSystemContext.Name;
            rpgSystemContext.Description = rpgSystemToUpdate.Description ?? rpgSystemContext.Description;
            rpgSystemContext.ImagePath = rpgSystemToUpdate.ImagePath ?? rpgSystemContext.ImagePath;
            rpgSystemContext.StoryName = rpgSystemToUpdate.StoryName ?? rpgSystemContext.StoryName;

            if (rpgSystemToUpdate.CharactersIds != null)
            {
                rpgSystemContext.Characters.Clear();
                var characters = await _context.Characters
                    .Where(c => rpgSystemToUpdate.CharactersIds.Contains(c.CharacterId))
                    .ToListAsync();
                rpgSystemContext.Characters = characters;
            }

            if (rpgSystemToUpdate.SeriesIds != null)
            {
                rpgSystemContext.Series.Clear();
                var series = await _context.Series
                    .Where(s => rpgSystemToUpdate.SeriesIds.Contains(s.SeriesId))
                    .ToListAsync();
                rpgSystemContext.Series = series;
            }

            if (rpgSystemToUpdate.Tags != null)
            {
                rpgSystemContext.Tags.Clear();
                if (rpgSystemToUpdate.Tags.Count > 0)
                {
                    var tags = await _context.Tags
                        .Where(t => rpgSystemToUpdate.Tags.Contains(t.TagId))
                        .ToListAsync();
                    rpgSystemContext.Tags = tags;
                }
            }

            _context.RpgSystems.Update(rpgSystemContext);
            return await SaveAsync();
        }
        public async Task<bool> SaveAsync()
        {
            var saved = await _context.SaveChangesAsync();
            return saved >= 0;
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

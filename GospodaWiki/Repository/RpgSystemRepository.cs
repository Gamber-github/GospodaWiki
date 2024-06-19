using GospodaWiki.Data;
using GospodaWiki.Dto.Character;
using GospodaWiki.Dto.RpgSystem;
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
            var rpgSystems = _context.RpgSystems.Where(r => r.isPublished).ToList();
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

        public ICollection<GetRpgSystemsDto> GetUnpublishedRpgSystems()
        {
            var rpgSystems = _context.RpgSystems.ToList();
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
        public GetRpgSystemDetailsDto GetRpgSystem(int rpgSystemId)
        {
            var rpgSystemContext = _context.RpgSystems.FirstOrDefault(c => c.RpgSystemId == rpgSystemId && c.isPublished);
            if(rpgSystemContext == null)
            {
                return null;
            }
            var story = _context.Stories.FirstOrDefault(s => s.RpgSystemId == rpgSystemContext.RpgSystemId);
            var tags = _context.Tags.Where(t => t.RpgSystems.Any(r => r.RpgSystemId == rpgSystemContext.RpgSystemId)).Select(t => new TagDetailsDto { Name = t.Name, TagId = t.TagId });
            var characters = _context.Characters.Where(c => c.RpgSystemId == rpgSystemContext.RpgSystemId).Select(c => new CharactersDto { CharacterId = c.CharacterId, FullName = c.FirstName + " " + c.LastName });
            var series = _context.Series.Where(s => s.RpgSystemId == rpgSystemContext.RpgSystemId).Select(s => s.Name).ToList();

            var rpgSystemDetailsDto = new GetRpgSystemDetailsDto
            {
                RpgSystemId = rpgSystemContext.RpgSystemId,
                Name = rpgSystemContext.Name,
                Description = rpgSystemContext.Description,
                StoryName = story?.Name,
                ImagePath = rpgSystemContext.ImagePath,
                Characters = characters.ToList(),
                Series = series.ToList(),
                Tags = tags.ToList(),
                isPublished = rpgSystemContext.isPublished
            };

            return rpgSystemDetailsDto;
        }
        public GetRpgSystemDetailsDto GetUnpublishedRpgSystem(int rpgSystemId)
        {
            var rpgSystemContext = _context.RpgSystems.FirstOrDefault(c => c.RpgSystemId == rpgSystemId );
            var story = _context.Stories.FirstOrDefault(s => s.RpgSystemId == rpgSystemContext.RpgSystemId);
            var tags = _context.Tags.Where(t => t.RpgSystems.Any(r => r.RpgSystemId == rpgSystemContext.RpgSystemId)).Select(t => new TagDetailsDto { Name = t.Name, TagId = t.TagId });
            var characters = _context.Characters.Where(c => c.RpgSystemId == rpgSystemContext.RpgSystemId).Select(c => new CharactersDto { CharacterId = c.CharacterId , FullName = c.FirstName +" " + c.LastName});
            var series = _context.Series.Where(s => s.RpgSystemId == rpgSystemContext.RpgSystemId).Select(s => s.Name).ToList();

            var rpgSystemDetailsDto = new GetRpgSystemDetailsDto
            {
                RpgSystemId = rpgSystemContext.RpgSystemId,
                Name = rpgSystemContext.Name,
                Description = rpgSystemContext.Description,
                StoryName = story?.Name,
                ImagePath = rpgSystemContext.ImagePath,
                Characters = characters.ToList(),
                Series = series.ToList(),
                Tags = tags.ToList(),
                isPublished = rpgSystemContext.isPublished
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
        public bool UpdateRpgSystem(PutRpgSystemDto rpgSystemToUpdate, int rpgSystemId)
        {
            if (rpgSystemToUpdate == null)
            {
                throw new ArgumentNullException(nameof(rpgSystemToUpdate));
            }

            var rpgSystemContext = _context.RpgSystems
                .Include(t => t.Tags)
                .Include(s => s.Series)
                .Include(c => c.Characters)
                .FirstOrDefault(c => c.RpgSystemId == rpgSystemId);

            if (rpgSystemContext == null)
            {
                throw new ArgumentNullException(nameof(rpgSystemToUpdate));
            }

            rpgSystemContext.Tags.Clear();

            var story =  _context.Stories.FirstOrDefault(s => s.RpgSystemId == rpgSystemContext.RpgSystemId);

            var tags = _context.Tags.Where(t => rpgSystemToUpdate.TagsId.Contains(t.TagId)).ToList();
            foreach ( var tag in tags ) {
                rpgSystemContext.Tags.Add(tag);
            }
            var series = _context.Series.Where(s => rpgSystemToUpdate.SeriesId.Contains(s.SeriesId)).ToList();
            foreach (var serie in series)
            {
                rpgSystemContext.Series.Add(serie);
            }
            var characters =  _context.Characters.Where(c => rpgSystemToUpdate.CharactersId.Contains(c.CharacterId)).ToList();
            foreach (var character in characters)
            {
                rpgSystemContext.Characters.Add(character);
            }

            rpgSystemContext.Name = rpgSystemToUpdate.Name;
            rpgSystemContext.Description = rpgSystemToUpdate.Description;
            rpgSystemContext.ImagePath = rpgSystemToUpdate.ImagePath;
            rpgSystemContext.Story = story;

            _context.RpgSystems.Update(rpgSystemContext);
            return Save();
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
        public bool PublishRpgSystem (int rpgSystemId)
        {
            var rpgSystem =  _context.RpgSystems
                .Include(t => t.Tags)
                .Include(s => s.Series)
                .Include(c => c.Characters)
                .FirstOrDefault(r => r.RpgSystemId == rpgSystemId);

            if (rpgSystem == null)
            {
                throw new ArgumentNullException(nameof(rpgSystem));
            }

            rpgSystem.isPublished = true;
            return Save();
        }
    }
}

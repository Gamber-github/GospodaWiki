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
            var rpgSystem = _context.RpgSystems.FirstOrDefault(c => c.RpgSystemId == id);
            var story = _context.Stories.FirstOrDefault(s => s.RpgSystemId == rpgSystem.RpgSystemId);
            var tags = _context.Tags.Where(t => t.RpgSystems.Any(r => r.RpgSystemId == rpgSystem.RpgSystemId)).Select(t => t.Name).ToList();
            var characters = _context.Characters.Where(c => c.RpgSystemId == rpgSystem.RpgSystemId).Select(c => c.FirstName + " " + c.LastName).ToList();
            var series = _context.Series.Where(s => s.RpgSystemId == rpgSystem.RpgSystemId).Select(s => s.Name).ToList();
            
            var rpgSystemDetailsDto = new RpgSystemDetailsDto
            {
                RpgSystemId = rpgSystem.RpgSystemId,
                Name = rpgSystem.Name,
                Description = rpgSystem.Description,
                StoryName = story?.Name,
                ImagePath = rpgSystem.ImagePath,
                Characters = characters.ToArray(),
                Series = series.ToArray(),
                Tags = tags.ToArray()
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
        public bool CreateRpgSystem(RpgSystem rpgSystem)
        {
            if (rpgSystem == null)
            {
                throw new ArgumentNullException(nameof(rpgSystem));
            }

            _context.RpgSystems.Add(rpgSystem);
            return Save();
        }
        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved >= 0;
        }
        public bool UpdateRpgSystem(PutRpgSystemDto rpgSystem)
        {
            if (rpgSystem == null)
            {
                throw new ArgumentNullException(nameof(rpgSystem));
            }

            var rpgSystemContext = _context.RpgSystems.FirstOrDefault(c => c.RpgSystemId == rpgSystem.RpgSystemId);
            
            if (rpgSystemContext == null)
            {
                throw new ArgumentNullException(nameof(rpgSystem));
            }

            rpgSystemContext.RpgSystemId = rpgSystem.RpgSystemId;
            rpgSystemContext.Name = rpgSystem.Name;
            rpgSystemContext.Description = rpgSystem.Description;
            rpgSystemContext.ImagePath = rpgSystem.ImagePath;
            rpgSystemContext.StoryName = rpgSystem.StoryName;

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

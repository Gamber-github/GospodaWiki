using GospodaWiki.Data;
using GospodaWiki.Dto.Character;
using GospodaWiki.Dto.Series;
using GospodaWiki.Interfaces;
using GospodaWiki.Models;

namespace GospodaWiki.Repository
{
    public class SeriesRepository : ISeriesInterface
    {
        private readonly DataContext _context;

        public SeriesRepository(DataContext context)
        {
            _context = context;
        }

        public ICollection<SeriesDto> GetSeries()
        {
            var series = _context.Series.ToList();
            var seriesDto = new List<SeriesDto>();

            foreach (var s in series)
            {
                var characters = _context.Characters.Where(c => c.SeriesId == s.SeriesId);
                var tags = _context.Tags.Where(t => t.Series.Any(s => s.SeriesId == s.SeriesId));
                var rpgSystem = _context.RpgSystems.FirstOrDefault(r => r.RpgSystemId == s.RpgSystemId);
                var players = _context.Players.Where(p => p.Series.Any(s => s.SeriesId == s.SeriesId));

                seriesDto.Add(new SeriesDto
                {
                    SeriesId = s.SeriesId,
                    Name = s.Name,
                    Description = s.Description,
                    Players = players.Select(p => new Dto.PlayerDto
                    {
                        PlayerId = p.PlayerId,
                        FirstName = p.FirstName,
                        LastName = p.LastName
                    }).ToList(),
                    Characters = characters.Select(c => new CharacterDetailsDto
                    {
                        CharacterId = c.CharacterId,
                        FirstName = c.FirstName,
                        LastName = c.LastName,
                    }).ToList(),
                    Tags = tags.Select(t => t.Name).ToList(),
                    RpgSystem = rpgSystem != null? new Dto.RpgSystem.RpgSystemsDto
                    {
                        RpgSystemId = rpgSystem.RpgSystemId,
                        Name = rpgSystem.Name
                    }: new Dto.RpgSystem.RpgSystemsDto(),
                    YoutubePlaylistId = s.YoutubePlaylistId
                });
            }
            return seriesDto;
        }

        public SeriesDetailsDto GetSeriesById(int seriesid)
        {
            var s = _context.Series.FirstOrDefault(s => s.SeriesId == seriesid);
            var characters = _context.Characters.Where(c => c.SeriesId == s.SeriesId);
            var tags = _context.Tags.Where(t => t.Series.Any(s => s.SeriesId == s.SeriesId));
            var rpgSystem = _context.RpgSystems.FirstOrDefault(r => r.RpgSystemId == s.RpgSystemId);
            var players = _context.Players.Where(p => p.Series.Any(s => s.SeriesId == s.SeriesId));

            return new SeriesDetailsDto
            {
                SeriesId = s.SeriesId,
                Name = s.Name,
                Description = s.Description,
                Players = players.Select(p => new Dto.PlayerDto
                {
                    PlayerId = p.PlayerId,
                    FirstName = p.FirstName,
                    LastName = p.LastName
                }).ToList(),
                Characters = characters.Select(c => new CharacterDetailsDto
                {
                    CharacterId = c.CharacterId,
                    FirstName = c.FirstName,
                    LastName = c.LastName,
                }).ToList(),
                Tags = tags.ToList(),
                RpgSystem = rpgSystem != null? new Dto.RpgSystem.RpgSystemsDto
                {
                    RpgSystemId = rpgSystem.RpgSystemId,
                    Name = rpgSystem.Name
                }: new Dto.RpgSystem.RpgSystemsDto(),
                YoutubePlaylistId = s.YoutubePlaylistId
            };
        }

        public bool SeriesExists(int seriesId)
        {
            return _context.Series.Any(s => s.SeriesId == seriesId);
        }

        public bool Save()
        {
            return _context.SaveChanges() >= 0;
        }

        public bool CreateSeries(PostSeriesDto seriesCreate)
        {
            if (seriesCreate == null)
            {
                return false;
            }

            var series = new Series
            {
                Name = seriesCreate.Name,
                Description = seriesCreate.Description,
                RpgSystemId = seriesCreate.RpgSystemId,
                YoutubePlaylistId = seriesCreate.YoutubePlaylistId,
                Tags = _context.Tags.Where(t => seriesCreate.Tags.Contains(t.TagId)).ToList(),
                Players = _context.Players.Where(p => seriesCreate.Players.Contains(p.PlayerId)).ToList(),
                Characters = _context.Characters.Where(c => seriesCreate.Characters.Contains(c.CharacterId)).ToList()
            };

            _context.Series.Add(series);
            return Save();
        }
        public bool UpdateSeries(PatchSeriesDto updatedSeries, int seriesId)
        {
            if(updatedSeries == null)
            {
                throw new ArgumentNullException(nameof(updatedSeries));
            }

            var seriesContext = _context.Series.FirstOrDefault(s => s.SeriesId == seriesId);

            if (seriesContext == null)
            {
                throw new ArgumentNullException(nameof(updatedSeries));
            }

            seriesContext.Name = updatedSeries.Name ?? seriesContext.Name;
            seriesContext.Description = updatedSeries.Description ?? seriesContext.Description;
            seriesContext.RpgSystemId = updatedSeries.RpgSystemId ?? seriesContext.RpgSystemId;
            seriesContext.YoutubePlaylistId = updatedSeries.YoutubePlaylistId ?? seriesContext.YoutubePlaylistId;
            seriesContext.Tags = updatedSeries.Tags != null ? updatedSeries.Tags.Select(t => new Tag { Name = t.Name }).ToList() : new List<Tag>();
            seriesContext.Players = updatedSeries.Players != null ? updatedSeries.Players.Select(p => new Player 
            { 
                FirstName = p.FirstName, 
                LastName = p.LastName, 
                PlayerId = p.PlayerId 
            }).ToList() : new List<Player>();
            seriesContext.Characters = updatedSeries.Characters != null ? updatedSeries.Characters.Select(c => new Character
            {
                FirstName = c.FirstName,
                LastName = c.LastName,
                CharacterId = c.CharacterId
            }).ToList() : new List<Character>();

            _context.Series.Update(seriesContext);
            return Save();
        }
    }
}

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
    }
}

using GospodaWiki.Data;
using GospodaWiki.Dto.Character;
using GospodaWiki.Dto.Series;
using GospodaWiki.Interfaces;
using GospodaWiki.Models;
using Microsoft.EntityFrameworkCore;

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
            var seriesContext = _context.Series.FirstOrDefault(s => s.SeriesId == seriesid);
            var characters = _context.Characters.Where(c => c.SeriesId == seriesContext.SeriesId);
            var tags = _context.Tags.Where(t => t.Series.Any(s => s.SeriesId == seriesContext.SeriesId));
            var rpgSystem = _context.RpgSystems.FirstOrDefault(r => r.RpgSystemId == seriesContext.RpgSystemId);
            var players = _context.Players.Where(p => p.Series.Any(s => s.SeriesId == seriesContext.SeriesId));

            return new SeriesDetailsDto
            {
                SeriesId = seriesContext.SeriesId,
                Name = seriesContext.Name,
                Description = seriesContext.Description,
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
                Tags = tags.Select(t => new Dto.Tag.TagDetailsDto
                {
                    TagId = t.TagId,
                    Name = t.Name
                }).ToList(),
                RpgSystem = rpgSystem != null? new Dto.RpgSystem.RpgSystemsDto
                {
                    RpgSystemId = rpgSystem.RpgSystemId,
                    Name = rpgSystem.Name
                }: new Dto.RpgSystem.RpgSystemsDto(),
                YoutubePlaylistId = seriesContext.YoutubePlaylistId
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

        public async Task<bool> SaveAsync()
        {
            return await _context.SaveChangesAsync() >= 0;
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
        public async Task<bool> UpdateSeries(PatchSeriesDto seriesToUpdate, int seriesId)
        {
            if(seriesToUpdate == null)
            {
                throw new ArgumentNullException(nameof(seriesToUpdate));
            }

            var seriesContext = await _context.Series
                .Include(s => s.Tags)
                .Include(s => s.Players)
                .Include(s => s.Characters)
                .FirstOrDefaultAsync(s => s.SeriesId == seriesId);

            if (seriesContext == null)
            {
                throw new ArgumentNullException(nameof(seriesToUpdate));
            }

            seriesContext.Name = seriesToUpdate.Name ?? seriesContext.Name;
            seriesContext.Description = seriesToUpdate.Description ?? seriesContext.Description;
            seriesContext.RpgSystemId = seriesToUpdate.RpgSystemId ?? seriesContext.RpgSystemId;
            seriesContext.YoutubePlaylistId = seriesToUpdate.YoutubePlaylistId ?? seriesContext.YoutubePlaylistId;

            if (seriesToUpdate.TagsId != null)
            {
                seriesContext.Tags?.Clear();
                if(seriesToUpdate.TagsId.Any())
                {
                    var tags = await _context.Tags
                        .Where(t => seriesToUpdate.TagsId.Contains(t.TagId))
                        .ToListAsync();
                    seriesContext.Tags = tags;
                }
            }

            if(seriesToUpdate.PlayersId != null)
            {
                seriesContext.Players?.Clear();
                if(seriesToUpdate.PlayersId.Any())
                {
                    var players = await _context.Players
                        .Where(p => seriesToUpdate.PlayersId.Contains(p.PlayerId))
                        .ToListAsync();
                    seriesContext.Players = players;
                }
            }

            if(seriesToUpdate.CharactersId != null)
            {
                seriesContext.Characters?.Clear();
                if(seriesToUpdate.CharactersId.Any())
                {
                    var characters = await _context.Characters
                        .Where(c => seriesToUpdate.CharactersId.Contains(c.CharacterId))
                        .ToListAsync();
                    seriesContext.Characters = characters;
                }
            }

            _context.Series.Update(seriesContext);
            return await SaveAsync();
        }
    }
}

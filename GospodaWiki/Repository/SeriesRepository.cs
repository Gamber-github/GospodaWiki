using GospodaWiki.Data;
using GospodaWiki.Dto.Character;
using GospodaWiki.Dto.Player;
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
        public ICollection<GetSeriesDto> GetSeries()
        {
            var series = _context.Series
                .Where(s => s.isPublished)
                .ToList();
            var seriesDto = new List<GetSeriesDto>();

            foreach (var s in series)
            {
                var characters = _context.Characters.Where(c => c.SeriesId == s.SeriesId);
                var tags = _context.Tags.Where(t => t.Series.Any(s => s.SeriesId == s.SeriesId));
                var rpgSystem = _context.RpgSystems.FirstOrDefault(r => r.RpgSystemId == s.RpgSystemId);
                var players = _context.Players.Where(p => p.Series.Any(s => s.SeriesId == s.SeriesId));

                seriesDto.Add(new GetSeriesDto
                {
                    SeriesId = s.SeriesId,
                    Name = s.Name,
                    Description = s.Description,
                    Players = players.Select(p => new GetPlayerDetailsDto
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
                    RpgSystem = rpgSystem != null? new Dto.RpgSystem.GetRpgSystemsDto
                    {
                        RpgSystemId = rpgSystem.RpgSystemId,
                        Name = rpgSystem.Name
                    }: new Dto.RpgSystem.GetRpgSystemsDto(),
                    YoutubePlaylistId = s.YoutubePlaylistId
                });
            }
            return seriesDto;
        }
        public ICollection<GetSeriesDto> GetUnpublishedSeries()
        {
            var series = _context.Series
                .ToList();
            var seriesDto = new List<GetSeriesDto>();

            foreach (var s in series)
            {
                var characters = _context.Characters.Where(c => c.SeriesId == s.SeriesId);
                var tags = _context.Tags.Where(t => t.Series.Any(s => s.SeriesId == s.SeriesId));
                var rpgSystem = _context.RpgSystems.FirstOrDefault(r => r.RpgSystemId == s.RpgSystemId);
                var players = _context.Players.Where(p => p.Series.Any(s => s.SeriesId == s.SeriesId));

                seriesDto.Add(new GetSeriesDto
                {
                    SeriesId = s.SeriesId,
                    Name = s.Name,
                    Description = s.Description,
                    Players = players.Select(p => new GetPlayerDetailsDto
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
                    RpgSystem = rpgSystem != null ? new Dto.RpgSystem.GetRpgSystemsDto
                    {
                        RpgSystemId = rpgSystem.RpgSystemId,
                        Name = rpgSystem.Name
                    } : new Dto.RpgSystem.GetRpgSystemsDto(),
                    YoutubePlaylistId = s.YoutubePlaylistId
                });
            }
            return seriesDto;
        }
        public GetSeriesDetailsDto GetSeriesById(int seriesid)
        {
            var seriesContext = _context.Series
                .Where(s => s.isPublished)
                .FirstOrDefault(s => s.SeriesId == seriesid);
            var characters = _context.Characters.Where(c => c.SeriesId == seriesContext.SeriesId);
            var tags = _context.Tags.Where(t => t.Series.Any(s => s.SeriesId == seriesContext.SeriesId));
            var rpgSystem = _context.RpgSystems.FirstOrDefault(r => r.RpgSystemId == seriesContext.RpgSystemId);
            var players = _context.Players.Where(p => p.Series.Any(s => s.SeriesId == seriesContext.SeriesId));

            return new GetSeriesDetailsDto
            {
                SeriesId = seriesContext.SeriesId,
                Name = seriesContext.Name,
                Description = seriesContext.Description,
                Players = players.Select(p => new GetPlayerDetailsDto
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
                Tags = tags.Select(t => new Dto.Tag.GetTagDetailsDto
                {
                    TagId = t.TagId,
                    Name = t.Name
                }).ToList(),
                RpgSystem = rpgSystem != null? new Dto.RpgSystem.GetRpgSystemsDto
                {
                    RpgSystemId = rpgSystem.RpgSystemId,
                    Name = rpgSystem.Name
                }: new Dto.RpgSystem.GetRpgSystemsDto(),
                YoutubePlaylistId = seriesContext.YoutubePlaylistId
            };
        }
        public GetSeriesDetailsDto GetUnpublishedSeriesById (int seriesId)
        {
            var seriesContext = _context.Series.FirstOrDefault(s => s.SeriesId == seriesId);
            var characters = _context.Characters.Where(c => c.SeriesId == seriesContext.SeriesId);
            var tags = _context.Tags.Where(t => t.Series.Any(s => s.SeriesId == seriesContext.SeriesId));
            var rpgSystem = _context.RpgSystems.FirstOrDefault(r => r.RpgSystemId == seriesContext.RpgSystemId);
            var players = _context.Players.Where(p => p.Series.Any(s => s.SeriesId == seriesContext.SeriesId));

            return new GetSeriesDetailsDto
            {
                SeriesId = seriesContext.SeriesId,
                Name = seriesContext.Name,
                Description = seriesContext.Description,
                Players = players.Select(p => new GetPlayerDetailsDto
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
                Tags = tags.Select(t => new Dto.Tag.GetTagDetailsDto
                {
                    TagId = t.TagId,
                    Name = t.Name
                }).ToList(),
                RpgSystem = rpgSystem != null ? new Dto.RpgSystem.GetRpgSystemsDto
                {
                    RpgSystemId = rpgSystem.RpgSystemId,
                    Name = rpgSystem.Name
                } : new Dto.RpgSystem.GetRpgSystemsDto(),
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
        public bool UpdateSeries(PutSeriesDto seriesToUpdate, int seriesId)
        {
            if(seriesToUpdate == null)
            {
                throw new ArgumentNullException(nameof(seriesToUpdate));
            }

            var seriesContext = _context.Series
                .Include(s => s.Tags)
                .Include(s => s.Players)
                .Include(s => s.Characters)
                .FirstOrDefault(s => s.SeriesId == seriesId);

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
                    var tags = _context.Tags
                        .Where(t => seriesToUpdate.TagsId.Contains(t.TagId))
                        .ToList();
                    seriesContext.Tags = tags;
                }
            }

            if(seriesToUpdate.PlayersId != null)
            {
                seriesContext.Players?.Clear();
                if(seriesToUpdate.PlayersId.Any())
                {
                    var players = _context.Players
                        .Where(p => seriesToUpdate.PlayersId.Contains(p.PlayerId))
                        .ToList();
                    seriesContext.Players = players;
                }
            }

            if(seriesToUpdate.CharactersId != null)
            {
                seriesContext.Characters?.Clear();
                if(seriesToUpdate.CharactersId.Any())
                {
                    var characters = _context.Characters
                        .Where(c => seriesToUpdate.CharactersId.Contains(c.CharacterId))
                        .ToList();
                    seriesContext.Characters = characters;
                }
            }

            _context.Series.Update(seriesContext);
            return Save();
        }
        public bool PublishSeries(int seriesId)
        {
            var seriesContext = _context.Series.FirstOrDefault(s => s.SeriesId == seriesId);
            if (seriesContext == null)
            {
                return false;
            }

            seriesContext.isPublished = true;
            _context.Series.Update(seriesContext);
            return Save();
        }
    }
}

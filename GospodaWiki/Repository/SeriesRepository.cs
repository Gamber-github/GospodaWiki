using GospodaWiki.Data;
using GospodaWiki.Dto.Character;
using GospodaWiki.Dto.Player;
using GospodaWiki.Dto.RpgSystem;
using GospodaWiki.Dto.Series;
using GospodaWiki.Dto.Tag;
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

                var playerList = players.Select(p => new GetPlayerReferenceDto
                {
                    PlayerId = p.PlayerId,
                    FirstName = p.FirstName,
                    LastName = p.LastName
                }).ToList();

                var characterList = characters.Select(c => new GetCharacterReferenceDto
                {
                    CharacterId = c.CharacterId,
                    FirstName = c.FirstName,
                    LastName = c.LastName
                }).ToList();

                var tagList = tags.Select(t => new Dto.Tag.TagReferenceDTO
                {
                    TagId = t.TagId,
                    Name = t.Name
                }).ToList();

                var rpgSystemList = rpgSystem != null ? new Dto.RpgSystem.GetRpgSystemReferenceDto
                {
                    RpgSystemId = rpgSystem.RpgSystemId,
                    Name = rpgSystem.Name
                } : new Dto.RpgSystem.GetRpgSystemReferenceDto();

                seriesDto.Add(new GetSeriesDto
                {
                    SeriesId = s.SeriesId,
                    Name = s.Name,
                    Description = s.Description,
                    Players = playerList,
                    Characters = characterList,
                    Tags = tagList,
                    RpgSystem = rpgSystemList,
                    YoutubePlaylistId = s.YoutubePlaylistId,
                    isPublished = s.isPublished
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

                var playerList = players.Select(p => new GetPlayerReferenceDto
                {
                    PlayerId = p.PlayerId,
                    FirstName = p.FirstName,
                    LastName = p.LastName
                }).ToList();

                var characterList = characters.Select(c => new GetCharacterReferenceDto
                {
                    CharacterId = c.CharacterId,
                    FirstName = c.FirstName,
                    LastName = c.LastName
                }).ToList();

                var tagList = tags.Select(t => new Dto.Tag.TagReferenceDTO
                {
                    TagId = t.TagId,
                    Name = t.Name
                }).ToList();

                var rpgSystemReference = rpgSystem != null ? new Dto.RpgSystem.GetRpgSystemReferenceDto
                {
                    RpgSystemId = rpgSystem.RpgSystemId,
                    Name = rpgSystem.Name
                } : null;

                seriesDto.Add(new GetSeriesDto
                {
                    SeriesId = s.SeriesId,
                    Name = s.Name,
                    Description = s.Description,
                    Players = playerList,
                    Characters = characterList,
                    Tags = tagList,
                    RpgSystem = rpgSystemReference,
                    YoutubePlaylistId = s.YoutubePlaylistId,
                    isPublished = s.isPublished
                });
            }
            return seriesDto;
        }
        public GetSeriesDetailsDto GetSeriesById(int seriesid)
        {
            var seriesContext = _context.Series
                .Where(s => s.isPublished)
                .Include(s => s.Tags)
                .Include(s => s.Players)
                .Include(s => s.Characters)
                .Include(s => s.RpgSystem)
                .Include(s => s.GameMaster)
                .FirstOrDefault(s => s.SeriesId == seriesid);

            var characters = _context.Characters.Where(c => c.SeriesId == seriesContext.SeriesId);
            var tags = _context.Tags.Where(t => t.Series.Any(s => s.SeriesId == seriesContext.SeriesId));
            var rpgSystem = _context.RpgSystems.FirstOrDefault(r => r.RpgSystemId == seriesContext.RpgSystemId);
            var players = _context.Players.Where(p => p.Series.Any(s => s.SeriesId == seriesContext.SeriesId));
            var gameMaster = _context.Players.FirstOrDefault(p => p.PlayerId == seriesContext.GameMasterId);

            return new GetSeriesDetailsDto
            {
                SeriesId = seriesContext.SeriesId,
                Name = seriesContext.Name,
                Description = seriesContext.Description,
                isPublished = seriesContext.isPublished,
                Players = players.Select(p => new GetPlayerReferenceDto
                {
                    PlayerId = p.PlayerId,
                    FirstName = p.FirstName,
                    LastName = p.LastName
                }).ToList(),
                Characters = characters.Select(c => new GetCharacterReferenceDto
                {
                    CharacterId = c.CharacterId,
                    FirstName = c.FirstName,
                    LastName = c.LastName,
                }).ToList(),
                Tags = tags.Select(t => new GetTagDetailsDto
                {
                    TagId = t.TagId,
                    Name = t.Name
                }).ToList(),
                RpgSystem = rpgSystem != null? new GetRpgSystemReferenceDto
                {
                    RpgSystemId = rpgSystem.RpgSystemId,
                    Name = rpgSystem.Name
                }: null,
                GameMaster = gameMaster != null ? new GetPlayerReferenceDto
                {
                    PlayerId = gameMaster.PlayerId,
                    FirstName = gameMaster.FirstName,
                    LastName = gameMaster.LastName
                }: null,
                YoutubePlaylistId = seriesContext.YoutubePlaylistId
            };
        }
        public GetSeriesDetailsDto GetUnpublishedSeriesById (int seriesId)
        {
            var seriesContext = _context.Series
                .Include(s => s.Tags)
                .Include(s => s.Players)
                .Include(s => s.Characters)
                .Include(s => s.RpgSystem)
                .Include(s => s.GameMaster)
                .FirstOrDefault(s => s.SeriesId == seriesId);

            var characters = _context.Characters.Where(c => c.SeriesId == seriesContext.SeriesId);
            var tags = _context.Tags.Where(t => t.Series.Any(s => s.SeriesId == seriesContext.SeriesId));
            var rpgSystem = _context.RpgSystems.FirstOrDefault(r => r.RpgSystemId == seriesContext.RpgSystemId);
            var players = _context.Players.Where(p => p.Series.Any(s => s.SeriesId == seriesContext.SeriesId));
            var gameMaster = _context.Players.FirstOrDefault(p => p.PlayerId == seriesContext.GameMasterId);

            var seriesDetailsDto = new GetSeriesDetailsDto
            {
                SeriesId = seriesContext.SeriesId,
                Name = seriesContext.Name,
                Description = seriesContext.Description,
                isPublished = seriesContext.isPublished,
                Players = players.Select(p => new GetPlayerReferenceDto
                {
                    PlayerId = p.PlayerId,
                    FirstName = p.FirstName,
                    LastName = p.LastName
                }).ToList(),
                Characters = characters.Select(c => new GetCharacterReferenceDto
                {
                    CharacterId = c.CharacterId,
                    FirstName = c.FirstName,
                    LastName = c.LastName,
                }).ToList(),
                Tags = tags.Select(t => new GetTagDetailsDto
                {
                    TagId = t.TagId,
                    Name = t.Name
                }).ToList(),
                RpgSystem = rpgSystem != null ? new GetRpgSystemReferenceDto
                {
                    RpgSystemId = rpgSystem.RpgSystemId,
                    Name = rpgSystem.Name
                } : null,
                GameMaster = gameMaster != null ? new GetPlayerReferenceDto
                {
                    PlayerId = gameMaster.PlayerId,
                    FirstName = gameMaster.FirstName,
                    LastName = gameMaster.LastName
                } : null,
                YoutubePlaylistId = seriesContext.YoutubePlaylistId
            };

            return seriesDetailsDto;
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
                YoutubePlaylistId = seriesCreate.YoutubePlaylistId,
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
            seriesContext.Description = seriesToUpdate.Description;
            seriesContext.RpgSystemId = seriesToUpdate.RpgSystemId;
            seriesContext.YoutubePlaylistId = seriesToUpdate.YoutubePlaylistId ?? seriesContext.YoutubePlaylistId;
            seriesContext.GameMasterId = seriesToUpdate.GameMasterId;

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
            var seriesContext = _context.Series
                .AsTracking()
                .FirstOrDefault(s => s.SeriesId == seriesId);
           
            if (seriesContext == null)
            {
                return false;
            }

            seriesContext.isPublished = !seriesContext.isPublished;
            _context.Series.Update(seriesContext);
            return Save();
        }

        public bool DeleteSeries(int seriesId)
        {
            var seriesContext = _context.Series.FirstOrDefault(s => s.SeriesId == seriesId);
            if (seriesContext == null)
            {
                return false;
            }

            _context.Series.Remove(seriesContext);
            return Save();       
        }
    }
}

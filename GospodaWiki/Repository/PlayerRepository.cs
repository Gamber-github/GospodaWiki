using GospodaWiki.Data;
using GospodaWiki.Dto.Player;
using GospodaWiki.Interfaces;
using GospodaWiki.Models;
using Microsoft.EntityFrameworkCore;

namespace GospodaWiki.Repository
{
    public class PlayerRepository: IPlayerInterface
    {
        private readonly DataContext _context;

        public PlayerRepository(DataContext context)
        {
            _context = context;
        }

        public ICollection<GetPlayersDto> GetPlayers()
        {
            var Players = _context.Players
                .Where(p => p.isPublished)
                .ToList();

            var PlayersDto = Players.Select(player => new GetPlayersDto
            {
                PlayerId = player.PlayerId,
                FirstName = player.FirstName,
                LastName = player.LastName,
                isPublished = player.isPublished
            }).ToList();

            return PlayersDto;
        }
        public ICollection<GetPlayersDto> GetUnpublishedPlayers()
        {
            var Players = _context.Players
                .ToList();

            var PlayersDto = Players.Select(player => new GetPlayersDto
            {
                PlayerId = player.PlayerId,
                FirstName = player.FirstName,
                LastName = player.LastName,
                isPublished = player.isPublished
            }).ToList();

            return PlayersDto;
        }
        public GetPlayerDetailsDto GetPlayer(int id)
        {
            var player = _context.Players
                .Where(p => p.isPublished)
                .Include(p => p.Series)
                .FirstOrDefault(p => p.PlayerId == id);

            if (player == null)
                {
                    return null;
                }

            var series = _context.Series.Where(s => s.Players.Any(p => p.PlayerId == player.PlayerId)).Select(s => s.Name);

            var PlayerDto = new GetPlayerDetailsDto
                {
                    PlayerId = player.PlayerId,
                    FirstName = player.FirstName,
                    LastName = player.LastName,
                    Series = series.ToList(),
                    isPublished = player.isPublished
                };

            return PlayerDto;

        }
        public bool PlayerExists(int playerId)
        {
            return _context.Players.Any(p => p.PlayerId == playerId);
        }
        public bool CreatePlayer(PostPlayerDto playerToCreate)
        {
            if (playerToCreate == null)
            {
                throw new ArgumentNullException(nameof(playerToCreate));
            }

            var player = new Player
            {
                FirstName = playerToCreate.FirstName,
                LastName = playerToCreate.LastName,
                About = playerToCreate.About,
                Image = playerToCreate.Image,
                Age = playerToCreate.Age
            };

            _context.Players.Add(player);
            return Save();
        }
        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved >= 0;
        }
        public GetPlayerDetailsDto GetUnpublishedPlayer(int playerId)
        {
            var player = _context.Players
                .Include(p => p.Series)
                .FirstOrDefault(p => p.PlayerId == playerId);

            if (player == null)
            {
                return null;
            }

            var series = _context.Series.Where(s => s.Players.Any(p => p.PlayerId == player.PlayerId)).Select(s => s.Name);

            var PlayerDto = new GetPlayerDetailsDto
            {
                PlayerId = player.PlayerId,
                FirstName = player.FirstName,
                LastName = player.LastName,
                About = player.About,
                Image = player.Image,
                Age = player.Age,
                Series = series.ToList(),
                isPublished = player.isPublished
            };

            return PlayerDto;
        }
        public bool UpdatePlayer(PutPlayerDto player, int playerId)
        {
            if (player == null) 
            {
                throw new ArgumentNullException(nameof(player));
            }

            var playerContext = _context.Players
                .Include(p => p.Series)
                .FirstOrDefault(p => p.PlayerId == playerId);

            if (playerContext == null) 
            {
                return false;
            }

            playerContext.Series.Clear();

            var series = _context.Series.Where(s => player.SeriesId.Contains(s.SeriesId)).ToList();

            foreach (var serie in series)
            {
                playerContext.Series.Add(serie);
            }

            playerContext.FirstName = player.FirstName;
            playerContext.LastName = player.LastName;
            playerContext.Image = player.Image;
            playerContext.Age = player.Age;
            playerContext.About = player.About;

            _context.Players.Update(playerContext);
            return Save();
        }
        public bool PublishPlayer(int playerId)
        {
            var player = _context.Players.FirstOrDefault(p => p.PlayerId == playerId);

            if (player == null)
            {
                return false;
            }

            player.isPublished = true;
            _context.Players.Update(player);
            return Save();
        }
    }
}

using GospodaWiki.Data;
using GospodaWiki.Interfaces;
using GospodaWiki.Models;

namespace GospodaWiki.Repository
{
    public class PlayerRepository: IPlayerInterface
    {
        private readonly DataContext _context;

        public PlayerRepository(DataContext context)
        {
            _context = context;
        }

        public ICollection<Player> GetPlayers()
        {
            return _context.Players.OrderBy(p => p.Id).ToList();
        }

        public Player GetPlayer(int id)
        {
            return _context.Players.FirstOrDefault(p => p.Id == id);
        }
        public Player GetPlayer(string name)
        {
            return _context.Players.FirstOrDefault(p => $"{p.FirstName} {p.LastName}" == name);
        }

        public bool PlayerExists(int playerId)
        {
            return _context.Players.Any(p => p.Id == playerId);
        }

        public bool CreatePlayer(Player player)
        {
            if (player == null)
            {
                throw new ArgumentNullException(nameof(player));
            }

            _context.Players.Add(player);
            return Save();
        }
        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved >= 0;
        }
    }
}

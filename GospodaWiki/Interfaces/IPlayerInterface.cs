using GospodaWiki.Models;

namespace GospodaWiki.Interfaces
{
    public interface IPlayerInterface
    {
        public ICollection<Player> GetPlayers();
        public Player GetPlayer(int id);
        public Player GetPlayer(string name);
        public bool PlayerExists(int playerId);
        public bool CreatePlayer(Player player);
        public bool Save();
    }
}

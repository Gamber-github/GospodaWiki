using GospodaWiki.Dto.Player;
using GospodaWiki.Models;

namespace GospodaWiki.Interfaces
{
    public interface IPlayerInterface
    {
        public ICollection<GetPlayersDto> GetPlayers();
        public ICollection<GetPlayersDto> GetUnpublishedPlayers();
        public GetPlayerDetailsDto GetPlayer(int id);
        public GetPlayerDetailsDto GetUnpublishedPlayer(int playerId);
        public bool PlayerExists(int playerId);
        public bool CreatePlayer(PostPlayerDto player);
        public bool UpdatePlayer(PutPlayerDto player, int playerId);
        public bool PublishPlayer(int playerId);
        public bool Save();
    }
}

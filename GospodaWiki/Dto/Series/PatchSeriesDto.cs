using GospodaWiki.Models;

namespace GospodaWiki.Dto.Series
{
    public class PatchSeriesDto
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public ICollection<int>? TagsId { get; set; }
        public ICollection<int>? PlayersId { get; set; }
        public ICollection<int>? CharactersId { get; set; }
        public int? RpgSystemId { get; set; }
        public string? YoutubePlaylistId { get; set; }
    }
}

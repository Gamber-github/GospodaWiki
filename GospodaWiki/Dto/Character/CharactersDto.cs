using GospodaWiki.Models;

namespace GospodaWiki.Dto.Character
{
    public class CharactersDto
    {
        public int CharacterId { get; set; }
        public string FullName { get; set;}
        public string SeriesName { get; set; }
        public string RpgSystemName { get; set; }
    }
}

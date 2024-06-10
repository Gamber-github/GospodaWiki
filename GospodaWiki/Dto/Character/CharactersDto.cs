using GospodaWiki.Models;

namespace GospodaWiki.Dto.Character
{
    public class CharactersDto
    {
        public int CharacterId { get; set; }
        public string FullName { get; set;}
        public GospodaWiki.Models.Series Series { get; set; }
        public GospodaWiki.Models.RpgSystem RpgSystem { get; set; }
    }
}

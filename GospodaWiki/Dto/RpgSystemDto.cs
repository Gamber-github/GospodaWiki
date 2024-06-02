using GospodaWiki.Models;

namespace GospodaWiki.Dto
{
    public class RpgSystemDto
    {
        public int RpgSystemId { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
    }
}

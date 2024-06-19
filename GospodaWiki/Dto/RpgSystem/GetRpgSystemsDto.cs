using GospodaWiki.Models;

namespace GospodaWiki.Dto.RpgSystem
{
    public class GetRpgSystemsDto
    {
        public int RpgSystemId { get; set; }
        public string Name { get; set; } = null!;
        public bool isPublished { get; set; }
    }
}

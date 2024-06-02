using GospodaWiki.Dto;

namespace GospodaWiki.Models
{
    public class Country
    {
        public int CountryId { get; set; }
        public string Name { get; set; } = null!;
        public ICollection<Character>? Characters { get; set; }
    }
}

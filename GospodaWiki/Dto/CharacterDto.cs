using GospodaWiki.Models;

namespace GospodaWiki.Dto
{
    public class CharacterDto
    {
        public int CharacterId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public int? Age { get; set; }
        public DateTime? Birthday { get; set; }
        public string? Biography { get; set; }
        public string? City { get; set; }
        public virtual CountryDto? Country { get; set; }
        public virtual RpgSystemDto? RpgSystem { get; set; }
        public virtual ICollection<AbilityDto>? Abilities { get; set; }
    }
}

namespace GospodaWiki.Models
{
    public class Character
    {
        public int CharacterId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public int? Age { get; set; }
        public DateTime? Birthday { get; set; }
        public string? Biography { get; set; }
        public string? City { get; set; }
        public int? CountryId { get; set; }
        public virtual Country? Country { get; set; }
        public virtual ICollection<Tag>? Tags { get; set; }
        public int? RpgSystemId { get; set; }
        public virtual RpgSystem? RpgSystem { get; set; }
        public virtual ICollection<Ability>? Abilities { get; set; }
        public virtual ICollection<Equipment>? Equipments { get; set; }
    }
}

namespace GospodaWiki.Models
{
    public class Character
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public int Age { get; set; }
        public DateTime Birthday { get; set; }
        public string Biography { get; set; } = null!;
        public City City { get; set; } = null!;
        public Country Country { get; set; } = null!;
        public ICollection<Tag> Tags { get; set; } = [];
        public RpgSystem RpgSystem { get; set; } = null!;
        public ICollection<CharacterAbility> CharacterAbilities { get; set; }
        public ICollection<CharacterEquipment> CharacterEquipments { get; set; }
    }
}

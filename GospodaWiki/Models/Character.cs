namespace GospodaWiki.Models
{
    public class Character
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int Age { get; set; }

        public DateTime Birthday { get; set; }

        public string Biography { get; set; }

        public string Image { get; set; }

        public City City { get; set; }

        public Country Country { get; set; }

        public ICollection<Tag> Tags { get; set; }

        public ICollection<CharacterAbility> CharacterAbilities { get; set; }

        public ICollection<CharacterEquipment> CharacterEquipment { get; set; }

        public ICollection<RpgSystemCharacter> RpgSystemCharacters { get; set; }
    }
}

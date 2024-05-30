namespace GospodaWiki.Models
{
    public class RpgSystem
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public ICollection<RpgSystemCharacter> RpgSystemCharacters { get; set; }

        public ICollection<Tag> Tags { get; set; }
    }
}

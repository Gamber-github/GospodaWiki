namespace GospodaWiki.Models
{
    public class RpgSystemCharacter
    {
        public int RpgSystemId { get; set; }
        public int CharcterId { get; set; }
        public RpgSystem RpgSystem { get; set; }
        public Character Character { get; set; }
    }
}

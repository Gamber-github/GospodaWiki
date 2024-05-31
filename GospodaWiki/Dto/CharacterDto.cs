using GospodaWiki.Models;

namespace GospodaWiki.Dto
{
    public class CharacterDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public int Age { get; set; }
        public DateTime Birthday { get; set; }
        public string Biography { get; set; } = null!;
        public string City { get; set; } = null!;
        public string Country { get; set; } = null!;
        public ICollection<Tag> Tags { get; set; } = [];
        public RpgSystem RpgSystem { get; set; } = null!;
    }
}

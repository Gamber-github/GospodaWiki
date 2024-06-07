using GospodaWiki.Models;

namespace GospodaWiki.Dto
{
    public class PlayerDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public int Age { get; set; }
        public string About { get; set; } = null!;
        public string Image { get; set; } = null!;
    }
}

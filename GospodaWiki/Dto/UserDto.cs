using System.ComponentModel.DataAnnotations;

namespace GospodaWiki.Dto
{
    public class UserDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
    }
}

using System.ComponentModel.DataAnnotations;

namespace GospodaWiki.Models
{
    public class User
    {
        public int Id { get; set; }
        [Required]
        public string FirstName { get; set; } = null!;
        [Required]
        public string LastName { get; set; } = null!;
        [Required]
        public bool IsAdmin { get; set; }
    }
}

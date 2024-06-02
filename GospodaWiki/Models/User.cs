using System.ComponentModel.DataAnnotations;

namespace GospodaWiki.Models
{
    public class User
    {
        public int UserId { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Email { get; set; }
        public bool IsAdmin { get; set; }
    }
}

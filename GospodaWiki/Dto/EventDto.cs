using GospodaWiki.Models;
using System.ComponentModel.DataAnnotations;

namespace GospodaWiki.Dto
{
    internal class EventDto
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        [Required]
        public string Date { get; set; } = null!;
        [Required]
        public Location Location { get; set; } = null!;
        public ICollection<Tag> Tags { get; set; } = [];
    }
}
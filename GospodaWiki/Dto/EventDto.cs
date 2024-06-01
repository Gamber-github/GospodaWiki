using GospodaWiki.Models;
using System.ComponentModel.DataAnnotations;

namespace GospodaWiki.Dto
{
    public class EventDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public DateTime Date { get; set; }
    }
}
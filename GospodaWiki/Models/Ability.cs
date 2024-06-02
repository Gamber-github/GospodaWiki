namespace GospodaWiki.Models
{
    public class Ability
    {
        public int AbilityId { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? Type { get; set; }
        public ICollection<Character>? Characters { get; set; }
    }
}

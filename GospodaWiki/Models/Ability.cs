namespace GospodaWiki.Models
{
    public class Ability
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string Type { get; set; } = null!;
        public ICollection<CharacterAbility> CharacterAbilities { get; set; } = [];
    }
}

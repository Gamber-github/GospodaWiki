namespace GospodaWiki.Models
{
    public class Ability
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public CharacterAbility CharacterAbility { get; set; }
    }
}

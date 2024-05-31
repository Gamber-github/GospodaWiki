namespace GospodaWiki.Models
{
    public class Equipment
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string Type { get; set; } = null!;
        public string Place { get; set; } = null!;
        public string Tags { get; set; } = null!;
        public ICollection<CharacterEquipment> CharacterEquipments { get; set; } = null!;
    }
}

namespace GospodaWiki.Models
{
    public class Equipment
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public string Place { get; set; }
        public string Tags { get; set; }
        public ICollection<CharacterEquipment> CharacterEquipments { get; set; }
    }
}

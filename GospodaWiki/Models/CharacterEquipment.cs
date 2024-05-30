namespace GospodaWiki.Models
{
    public class CharacterEquipment
    {
        public int CharacterId { get; set; }
        public int EquipmentId { get; set; }
        public Character Character { get; set; }
        public Equipment Equipment { get; set; }
    }
}

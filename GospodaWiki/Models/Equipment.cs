namespace GospodaWiki.Models
{
    public class Equipment
    {
        public int EquipmentId { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? Type { get; set; }
        public string? Place { get; set; }
        public ICollection<Character>? Characters { get; set; }
        public ICollection<Tag>? Tags { get; set; }
    }
}

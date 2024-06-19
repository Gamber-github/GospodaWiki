namespace GospodaWiki.Dto.Character
{
    public class CharacterDetailsDto
    {
        public int CharacterId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? ImagePath { get; set; }
        public int? Age { get; set; }
        public string? Description { get; set; }
        public string Series { get; set; }
        public string? RpgSystem { get; set; }
        public bool isPublished { get; set; }
        public ICollection<string>? Tags { get; set; }
        public ICollection<string>? Items { get; set; }
    }
}

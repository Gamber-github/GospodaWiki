using GospodaWiki.Dto.Items;
using GospodaWiki.Dto.RpgSystem;
using GospodaWiki.Dto.Series;
using GospodaWiki.Dto.Tag;

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
        public GetSeriesReferenceDto? Series { get; set; }
        public GetRpgSystemReferenceDto? RpgSystem { get; set; }
        public bool isPublished { get; set; }
        public ICollection<TagReferenceDTO>? Tags { get; set; }
        public ICollection<ItemsReferenceDto>? Items { get; set; }
    }
}

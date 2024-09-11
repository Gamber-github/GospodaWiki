using GospodaWiki.Dto.Player;

namespace GospodaWiki.Dto.Character
{
    public class CharacterDTOPagedListDto
    {
        public IEnumerable<GetCharactersDto> Items { get; set; }
        public int totalItemCount { get; set; }
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
    }
}

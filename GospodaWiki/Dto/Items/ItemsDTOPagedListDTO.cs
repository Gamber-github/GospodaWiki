using GospodaWiki.Dto.Character;

namespace GospodaWiki.Dto.Items
{
    public class ItemsDTOPagedListDTO
    {
        public IEnumerable<GetItemsDto> Items { get; set; }
        public int totalItemCount { get; set; }
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
    }
}

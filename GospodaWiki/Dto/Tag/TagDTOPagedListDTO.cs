using GospodaWiki.Dto.Series;

namespace GospodaWiki.Dto.Tag
{
    public class TagDTOPagedListDTO
    {
        public IEnumerable<GetTagDetailsDto> Items { get; set; }
        public int totalItemCount { get; set; }
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
    }
}

using GospodaWiki.Dto.Series;

namespace GospodaWiki.Dto.Event
{
    public class EventDTOPagedListDTO
    {
            public IEnumerable<GetEventsDto> Items { get; set; }
            public int totalItemCount { get; set; }
            public int PageSize { get; set; }
            public int PageNumber { get; set; }   
     }
}

using GospodaWiki.Dto.Event;
using GospodaWiki.Models;

namespace GospodaWiki.Interfaces
{
    public interface IEventInterface
    {
        ICollection<EventsDto> GetEvents();
        EventDetailsDto GetEvent(int eventId);
        bool EventExists(int eventId);
        bool CreateEvent(PostEventDto @event);
        bool UpdateEvent(PatchEventDto @event, int @eventId);
        bool Save();
    }
}

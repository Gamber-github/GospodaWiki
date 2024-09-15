using GospodaWiki.Dto.Event;

namespace GospodaWiki.Interfaces
{
    public interface IEventInterface
    {
        ICollection<GetEventsDto> GetEvents();
        ICollection<GetEventsDto> GetUnpublishedEvents();
        EventDetailsDto GetEvent(int eventId);
        EventDetailsDto GetUnpublishedEvent(int eventId);
        bool EventExists(int eventId);
        bool CreateEvent(PostEventDto @event);
        bool UpdateEvent(PutEventDto @event, int @eventId);
        bool Save();
        bool PublishEvent(int eventId);
        bool DeleteEvent(int eventId);
    }
}

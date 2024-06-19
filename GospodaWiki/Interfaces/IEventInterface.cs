using GospodaWiki.Dto.Event;
using GospodaWiki.Models;

namespace GospodaWiki.Interfaces
{
    public interface IEventInterface
    {
        ICollection<EventsDto> GetEvents();
        ICollection<EventsDto> GetUnpublishedEvents();
        EventDetailsDto GetEvent(int eventId);
        EventDetailsDto GetUnpublishedEvent(int eventId);
        bool EventExists(int eventId);
        bool CreateEvent(PostEventDto @event);
        bool UpdateEvent(PutEventDto @event, int @eventId);
        bool Save();
        Task<bool> SaveAsync();
        Task<bool> PublishEvent(int eventId);
    }
}

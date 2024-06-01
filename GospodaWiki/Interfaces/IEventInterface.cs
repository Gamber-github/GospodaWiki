using GospodaWiki.Models;

namespace GospodaWiki.Interfaces
{
    public interface IEventInterface
    {
        ICollection<Event> GetEvents();
        Event GetEvent(int eventId);
        Event GetEvent(string name);
        bool EventExists(int eventId);
        bool CreateEvent(Event @event);
        bool Save();
    }
}

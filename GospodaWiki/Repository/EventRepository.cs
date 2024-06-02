using GospodaWiki.Data;
using GospodaWiki.Interfaces;
using GospodaWiki.Models;

namespace GospodaWiki.Repository
{
    public class EventRepository : IEventInterface
    {
        private readonly DataContext _context;
        public EventRepository(DataContext context)
        {
            _context = context;
        }

        public ICollection<Event> GetEvents()
        {
            return _context.Events.OrderBy(p => p.EventId).ToList();
        }

        public Event GetEvent(int eventId)
        {
            return _context.Events.FirstOrDefault(p => p.EventId == eventId);
        }

        public Event GetEvent(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentException("Name cannot be empty or null.");
            }

            return _context.Events.FirstOrDefault(e => e.Name == name);
        }

        public bool EventExists(int eventId)
        {
            return _context.Events.Any(p => p.EventId == eventId);
        }

        public bool CreateEvent(Event @event)
        {
            if (@event == null)
            {
                throw new ArgumentNullException(nameof(@event));
            }

            _context.Events.Add(@event);
            return Save();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved >= 0;
        }

        public bool UpdateEvent(Event @event)
        {
            if (@event == null)
            {
                throw new ArgumentNullException(nameof(@event));
            }

            _context.Events.Update(@event);
            return Save();
        }

        public bool DeleteEvent(Event @event)
        {
            if (@event == null)
            {
                throw new ArgumentNullException(nameof(@event));
            }

            _context.Events.Remove(@event);
            return Save();
        }
    }
}

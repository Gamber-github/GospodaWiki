using GospodaWiki.Data;
using GospodaWiki.Dto.Event;
using GospodaWiki.Interfaces;
using GospodaWiki.Models;
using Microsoft.EntityFrameworkCore;

namespace GospodaWiki.Repository
{
    public class EventRepository : IEventInterface
    {
        private readonly DataContext _context;
        public EventRepository(DataContext context)
        {
            _context = context;
        }

        public ICollection<EventsDto> GetEvents()
        {
            var events = _context.Events.ToList();
            var eventsDto = new List<EventsDto>();

            foreach (var @event in events)
            {
                eventsDto.Add(new EventsDto
                {
                    EventId = @event.EventId,
                    Name = @event.Name,
                    Description = @event.Description,
                    EventUrl = @event.EventUrl,
                    ImagePath = @event.ImagePath,
                    Date = @event.Date
                });
            }

            return eventsDto;
        }

        public EventDetailsDto GetEvent(int eventId)
        {
            var @eventContext = _context.Events.FirstOrDefault(p => p.EventId == eventId);

            if (@eventContext == null)
            {
                throw new ArgumentNullException(nameof(@eventContext));
            }

            var location = _context.Locations.FirstOrDefault(l => l.LocationId == @eventContext.LocationId);
            var tags = _context.Tags.Where(t => t.Events.Any(e => e.EventId == @eventContext.EventId)).Select(t => t.Name).ToList();

            var @eventDto = new EventDetailsDto
            {
                EventId = @eventContext.EventId,
                Name = @eventContext.Name,
                Description = @eventContext.Description,
                EventUrl = @eventContext.EventUrl,
                ImagePath = @eventContext.ImagePath,
                Date = @eventContext.Date,
                Location = location != null ? new Location
                {
                    LocationId = location.LocationId,
                    Name = location.Name,
                    Address = location.Address,
                    City = location.City,
                    LocationURL = location.LocationURL
                } : new Location(),
                Tags = tags.ToArray()
            };

            return @eventDto;
        }
        public bool EventExists(int eventId)
        {
            return _context.Events.Any(p => p.EventId == eventId);
        }

        public bool CreateEvent(PostEventDto @eventCreate)
        {
            if (@eventCreate == null)
            {
                throw new ArgumentNullException(nameof(@eventCreate));
            }

            var @event = new Event
            {
                Name = @eventCreate.Name,
                Description = @eventCreate.Description,
                EventUrl = @eventCreate.EventUrl,
                ImagePath = @eventCreate.ImagePath,
                Date = @eventCreate.Date,
                LocationId = @eventCreate.LocationId,
                Tags = _context.Tags.Where(t => @eventCreate.TagIds.Contains(t.TagId)).ToList()
            };

            _context.Events.Add(@event);
            return Save();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved >= 0;
        }
        public async Task<bool> SaveAsync()
        {
            var saved = await _context.SaveChangesAsync();
            return saved >= 0;
        }

        public async Task<bool> UpdateEvent(PatchEventDto eventToUpdate, int @eventId)
        {
            if (eventToUpdate == null)
            {
                throw new ArgumentNullException(nameof(eventToUpdate));
            }

            var @eventContext = _context.Events
                .Include(e => e.Tags)
                .FirstOrDefault(p => p.EventId == @eventId);

            if (@eventContext == null)
            {
                throw new ArgumentNullException(nameof(@eventContext));
            }

            @eventContext.Name = eventToUpdate.Name ?? @eventContext.Name;
            @eventContext.Description = eventToUpdate.Description ?? @eventContext.Description;
            @eventContext.EventUrl = eventToUpdate.EventUrl ?? @eventContext.EventUrl;
            @eventContext.ImagePath = eventToUpdate.ImagePath ?? @eventContext.ImagePath;
            @eventContext.Date = eventToUpdate.Date ?? @eventContext.Date;
            @eventContext.LocationId = eventToUpdate.LocationId ?? @eventContext.LocationId;

            if (eventToUpdate.TagIds != null)
            {
                @eventContext.Tags.Clear();
                var tags = await _context.Tags
                    .Where(t => eventToUpdate.TagIds.Contains(t.TagId))
                    .ToListAsync();
                @eventContext.Tags = tags;
            }

            _context.Events.Update(@eventContext);
            return await SaveAsync();
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

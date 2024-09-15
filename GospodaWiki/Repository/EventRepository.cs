using Ganss.Xss;
using GospodaWiki.Data;
using GospodaWiki.Dto.Event;
using GospodaWiki.Dto.Location;
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

        public ICollection<GetEventsDto> GetEvents()
        {
            var events = _context.Events.Where(e => e.isPublished == true).ToList();
            var eventsDto = new List<GetEventsDto>();

            foreach (var @event in events)
            {
                eventsDto.Add(new GetEventsDto
                {
                    EventId = @event.EventId,
                    Name = @event.Name,
                    Description = @event.Description,
                    EventUrl = @event.EventUrl,
                    ImagePath = @event.ImagePath,
                    Date = @event.Date,
                    isPublished = @event.isPublished
                });
            }

            return eventsDto;
        }

        public ICollection<GetEventsDto> GetUnpublishedEvents()
        {
            var events = _context.Events.ToList();
            var eventsDto = new List<GetEventsDto>();

            foreach (var @event in events)
            {
                eventsDto.Add(new GetEventsDto
                {
                    EventId = @event.EventId,
                    Name = @event.Name,
                    Description = @event.Description,
                    EventUrl = @event.EventUrl,
                    ImagePath = @event.ImagePath,
                    Date = @event.Date,
                    isPublished = @event.isPublished

                });
            }

            return eventsDto;
        }

        public EventDetailsDto GetEvent(int eventId)
        {
            var @eventContext = _context.Events
                .Include(e => e.Tags)
                .Include(e => e.Location)
                .FirstOrDefault(p => p.EventId == eventId && p.isPublished);

            if (@eventContext == null)
            {
                return null;
            }

            var location = _context.Locations.FirstOrDefault(l => l.LocationId == @eventContext.LocationId);
            var tags = _context.Tags.Where(e => e.isPublished && e.Events.Any(e => e.EventId == eventContext.EventId));


            var @eventDto = new EventDetailsDto
            {
                EventId = @eventContext.EventId,
                Name = @eventContext.Name,
                Description = @eventContext.Description,
                EventUrl = @eventContext.EventUrl,
                ImagePath = @eventContext.ImagePath,
                Date = @eventContext.Date,
                Location = location != null ? new GetLocationReferenceDTO
                {
                    LocationId = location.LocationId,
                    Name = location.Name,
       
                } : new GetLocationReferenceDTO(),
                Tags = tags.Select( t=> new Dto.Tag.TagReferenceDTO
                {
                    TagId = t.TagId,
                    Name = t.Name
                }).ToList(),
                isPublished = @eventContext.isPublished
            };

            return @eventDto;
        }

        public EventDetailsDto GetUnpublishedEvent(int eventId)
        {
            var @eventContext = _context.Events
                .Include(e => e.Tags)
                .Include(e => e.Location)
                .FirstOrDefault(p => p.EventId == eventId);

            if (@eventContext == null)
            {
                return null;
            }

            var location = _context.Locations.FirstOrDefault(l => l.LocationId == @eventContext.LocationId);
            var tags = _context.Tags.Where(e => e.Events.Any(e => e.EventId == eventContext.EventId));

            var @eventDto = new EventDetailsDto
            {
                EventId = @eventContext.EventId,
                Name = @eventContext.Name,
                Description = @eventContext.Description,
                EventUrl = @eventContext.EventUrl,
                ImagePath = @eventContext.ImagePath,
                Date = @eventContext.Date,
                Location = location != null ? new GetLocationReferenceDTO
                {
                    LocationId = location.LocationId,
                    Name = location.Name,

                } : new GetLocationReferenceDTO(),
                Tags = tags.Select(t => new Dto.Tag.TagReferenceDTO
                {
                    TagId = t.TagId,
                    Name = t.Name
                }).ToList(),
                isPublished = @eventContext.isPublished

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
            };

            _context.Events.Add(@event);
            return Save();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved >= 0;
        }

        public bool UpdateEvent(PutEventDto @eventToUpdate, int @eventId)
        {
            var sanitizer = new HtmlSanitizer();
            if (@eventToUpdate == null)
            {
                throw new ArgumentNullException(nameof(@eventToUpdate));
            }

            var @eventContext = _context.Events
                .Include(e => e.Tags)
                .Include(e => e.Location)
                .FirstOrDefault(p => p.EventId == @eventId);

            if (@eventContext == null)
            {
                throw new ArgumentNullException(nameof(@eventContext));
            }

            if (@eventToUpdate.TagIds != null)
            {
                @eventContext.Tags?.Clear();
                if (@eventToUpdate.TagIds.Any())
                {
                    var tags = _context.Tags
                        .Where(t => @eventToUpdate.TagIds.Contains(t.TagId))
                        .ToList();
                    @eventContext.Tags = tags;
                }
            }

            @eventContext.EventId = @eventId;
            @eventContext.Name = sanitizer.Sanitize(@eventToUpdate.Name);
            @eventContext.Description = sanitizer.Sanitize(@eventToUpdate.Description);
            @eventContext.EventUrl = sanitizer.Sanitize(@eventToUpdate.EventUrl);
            @eventContext.ImagePath = @eventToUpdate.ImagePath ?? @eventContext.ImagePath;
            @eventContext.Date = DateTime.Parse(@eventToUpdate.Date);
            @eventContext.LocationId = @eventToUpdate.LocationId ?? eventContext.LocationId;

            _context.Events.Update(@eventContext);
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

        public bool PublishEvent(int eventId)
        {
            var @eventContext = _context.Events.FirstOrDefault(p => p.EventId == eventId);

            if (@eventContext == null)
            {
                throw new ArgumentNullException(nameof(@eventContext));
            }

            @eventContext.isPublished = !eventContext.isPublished;
            _context.Events.Update(@eventContext);
            return Save();
        }

        public bool DeleteEvent(int eventId)
        {
            var @eventContext = _context.Events.FirstOrDefault(p => p.EventId == eventId);

            if (@eventContext == null)
            {
                throw new ArgumentNullException(nameof(@eventContext));
            }

            _context.Events.Remove(@eventContext);
            return Save();
        }
    }
}

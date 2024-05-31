using AutoMapper;
using GospodaWiki.Dto;
using GospodaWiki.Interfaces;
using GospodaWiki.Models;
using Microsoft.AspNetCore.Mvc;

namespace GospodaWiki.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventController : Controller
    {
        private readonly IEventInterface _eventRepository;
        private readonly IMapper _mapper;
        public EventController(IEventInterface eventRepository, IMapper mapper)
        {
            _eventRepository = eventRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Event>))]
        public IActionResult GetEvents()
        {
            var events = _mapper.Map<List<EventDto>>(_eventRepository.GetEvents());
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(events);
        }

        [HttpGet("{eventId}")]
        [ProducesResponseType(200, Type = typeof(Event))]
        [ProducesResponseType(400)]
        public IActionResult GetEvent(int eventId)
        {
           if(!_eventRepository.EventExists(eventId))
            {
                return NotFound();
            }

            var events = _mapper.Map<List<EventDto>>(_eventRepository.GetEvent(eventId));

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(events);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateEvent([FromBody] Event eventCreate)
        {
            if (eventCreate == null)
            {
                return BadRequest(ModelState);
            }

            var eventMap = _mapper.Map<Event>(eventCreate);
            if(!_eventRepository.CreateEvent(eventMap))
            {
                ModelState.AddModelError("", $"Something went wrong saving the event {eventMap.Name}");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created");

        }
    }
}

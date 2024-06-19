using AutoMapper;
using GospodaWiki.Dto.Event;
using GospodaWiki.Interfaces;
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
        [ProducesResponseType(200, Type = typeof(IEnumerable<EventsDto>))]
        public IActionResult GetUnpublishedEvents()
        {
            var events = _mapper.Map<List<EventsDto>>(_eventRepository.GetUnpublishedEvents());
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(events);
        }

        [HttpGet("{eventId}")]
        [ProducesResponseType(200, Type = typeof(EventDetailsDto))]
        [ProducesResponseType(400)]
        public IActionResult GetUnpublishedEvent(int eventId)
        {
           if(!_eventRepository.EventExists(eventId))
            {
                return NotFound();
            }

            var events = _mapper.Map<EventDetailsDto>(_eventRepository.GetUnpublishedEvent(eventId));

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(events);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateEvent([FromBody] PostEventDto eventCreate)
        {
            if (eventCreate == null)
            {
                return BadRequest(ModelState);
            }

            var eventMap = _mapper.Map<PostEventDto>(eventCreate);
            if(!_eventRepository.CreateEvent(eventMap))
            {
                ModelState.AddModelError("", $"Something went wrong saving the event {eventMap.Name}");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created");

        }

        [HttpPut("{eventId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(500)]
        [ProducesResponseType(404)]
        public IActionResult UpdateEvent(int eventId, [FromBody] PutEventDto eventUpdate)
        {
            if (eventUpdate == null)
            {
                return BadRequest(ModelState);
            }

            if (eventId == null)
            {
                return BadRequest(ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!_eventRepository.EventExists(eventId))
            {
                return NotFound();
            }

            var eventMap = _mapper.Map<PutEventDto>(eventUpdate);

            if (!_eventRepository.UpdateEvent(eventMap, eventId))
            {
                ModelState.AddModelError("", $"Something went wrong updating the event {eventMap.Name}");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully updated");
        }

        [HttpPatch("{eventId}/publish")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> PublishEvent([FromRoute] int eventId)
        {
            if (!_eventRepository.EventExists(eventId))
            {
                ModelState.AddModelError("", $"Something went wrong.");
                return NotFound(ModelState);
            }

            if (!await _eventRepository.PublishEvent(eventId))
            {
                ModelState.AddModelError("", $"Something went wrong publishing the event with id {eventId}");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully published");
        }
    }
}

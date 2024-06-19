using AutoMapper;
using GospodaWiki.Dto.Character;
using GospodaWiki.Dto.Event;
using GospodaWiki.Dto.RpgSystem;
using GospodaWiki.Dto.Series;
using GospodaWiki.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GospodaWiki.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PublishedController : Controller
    {
        private readonly ICharacterInterface _characterRepository;
        private readonly IEventInterface _eventRepository;
        private readonly IRpgSystemInterface _rpgSystemRepository;
        private readonly ISeriesInterface _seriesRepository;
        private readonly IMapper _mapper;

        public PublishedController(
            ICharacterInterface characterRepository, 
            IEventInterface eventRepository, 
            IRpgSystemInterface rpgSystemRepository,
            ISeriesInterface seriesRepository,
            IMapper mapper)
        {
            _characterRepository = characterRepository;
            _eventRepository = eventRepository;
            _rpgSystemRepository = rpgSystemRepository;
            _seriesRepository = seriesRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("/Characters")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<CharactersDto>))]
        public IActionResult GetChatacters()
        {
            var characters = _mapper.Map<List<CharactersDto>>(_characterRepository.GetCharacters());
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(characters);
        }

        [HttpGet("/Character/{characterId}")]
        [ProducesResponseType(200, Type = typeof(CharacterDetailsDto))]
        [ProducesResponseType(400)]
        public IActionResult GetCharacter(int characterId)
        {
            if (!_characterRepository.CharacterExists(characterId))
            {
                return NotFound();
            }

            var character = _mapper.Map<CharacterDetailsDto>(_characterRepository.GetCharacter(characterId));

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if(character == null)
            {
                return NotFound();
            }

            return Ok(character);
        }

        [HttpGet("/Events")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<EventsDto>))]
        [ProducesResponseType(400)]
        public IActionResult GetEvents()
        {      
            var events = _mapper.Map<List<EventsDto>>(_eventRepository.GetEvents());
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(events);
        }

        [HttpGet("/Event/{eventId}")]
        [ProducesResponseType(200, Type = typeof(EventDetailsDto))]
        [ProducesResponseType(400)]
        public IActionResult GetEvent(int eventId)
        {
            if (!_eventRepository.EventExists(eventId))
            {
                return NotFound();
            }

            var events = _mapper.Map<EventDetailsDto>(_eventRepository.GetEvent(eventId));

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(events);
        }

        [HttpGet("/RpgSystems")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<GetRpgSystemsDto>))]
        [ProducesResponseType(400)]
        public IActionResult GetRpgSystems()
        {
            var rpgSystems = _mapper.Map<List<GetRpgSystemsDto>>(_rpgSystemRepository.GetRpgSystems());
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(rpgSystems);
        }

        [HttpGet("/RpgSystem/{rpgSystemId}")]
        [ProducesResponseType(200, Type = typeof(GetRpgSystemDetailsDto))]
        [ProducesResponseType(400)]
        public IActionResult GetRpgSystem(int rpgSystemId)
        {
            if (!_rpgSystemRepository.RpgSystemExists(rpgSystemId))
            {
                return NotFound();
            }

            var rpgSystem = _mapper.Map<GetRpgSystemDetailsDto>(_rpgSystemRepository.GetRpgSystem(rpgSystemId));

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if(rpgSystem == null)
            {
                return NotFound();
            }

            return Ok(rpgSystem);
        }

        [HttpGet("/Series")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<GetSeriesDto>))]
        [ProducesResponseType(400)]
        public IActionResult GetSeries()
        {
            var series = _mapper.Map<List<GetSeriesDto>>(_seriesRepository.GetSeries());
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(series);
        }

        [HttpGet("/Series/{seriesId}")]
        [ProducesResponseType(200, Type = typeof(GetSeriesDetailsDto))]
        [ProducesResponseType(400)]
        public IActionResult GetSeries(int seriesId)
        {
            if (!_seriesRepository.SeriesExists(seriesId))
            {
                return NotFound();
            }

            var series = _mapper.Map<GetSeriesDetailsDto>(_seriesRepository.GetSeriesById(seriesId));

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if(series == null)
            {
                return NotFound();
            }

            return Ok(series);
        }
    }
}

using AutoMapper;
using GospodaWiki.Dto.Character;
using GospodaWiki.Dto.Event;
using GospodaWiki.Dto.Items;
using GospodaWiki.Dto.RpgSystem;
using GospodaWiki.Dto.Series;
using GospodaWiki.Dto.Tag;
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
        private readonly ITagInterface _tagRepository;
        private readonly IItemInterface _itemRepository;
        private readonly IMapper _mapper;

        public PublishedController(
            ICharacterInterface characterRepository, 
            IEventInterface eventRepository, 
            IRpgSystemInterface rpgSystemRepository,
            ISeriesInterface seriesRepository,
            ITagInterface tagRepository,
            IItemInterface itemRepository,
            IMapper mapper
            )

        {
            _characterRepository = characterRepository;
            _eventRepository = eventRepository;
            _rpgSystemRepository = rpgSystemRepository;
            _seriesRepository = seriesRepository;
            _tagRepository = tagRepository;
            _itemRepository = itemRepository;
            _mapper = mapper;

        }

        [HttpGet]
        [Route("/Characters")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<CharactersDto>))]
        public IActionResult GetChatacters(int pageNumber = 1, int pageSize = 10)
        {
            var characters = _mapper.Map<List<CharactersDto>>(_characterRepository.GetCharacters());
            var pagedCharacters = characters.Skip((pageNumber - 1) * pageSize).Take(pageSize);
            var mappedCharacters = _mapper.Map<List<CharactersDto>>(pagedCharacters);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(mappedCharacters);
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
        public IActionResult GetEvents(int pageNumber = 1, int pageSize = 10)
        {      
            var events = _mapper.Map<List<EventsDto>>(_eventRepository.GetEvents());
            var pagedEvents = events.Skip((pageNumber - 1) * pageSize).Take(pageSize);
            var mappedEvents = _mapper.Map<List<EventsDto>>(pagedEvents);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(mappedEvents);
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
        public IActionResult GetRpgSystems(int pageNumber = 1, int pageSize = 10)
        {
            var rpgSystems = _mapper.Map<List<GetRpgSystemsDto>>(_rpgSystemRepository.GetRpgSystems());
            var pagedRpgSystems = rpgSystems.Skip((pageNumber - 1) * pageSize).Take(pageSize);
            var mappedRpgSystems = _mapper.Map<List<GetTagDetailsDto>>(pagedRpgSystems);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(mappedRpgSystems);
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
        public IActionResult GetSeries(int pageNumber = 1, int pageSize = 10)
        {
            var series = _mapper.Map<List<GetSeriesDto>>(_seriesRepository.GetSeries());
            var pagedSeries = series.Skip((pageNumber - 1) * pageSize).Take(pageSize);
            var mappedSeries = _mapper.Map<List<GetTagDetailsDto>>(pagedSeries);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(mappedSeries);
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

        [HttpGet("/Tags")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<GetTagDetailsDto>))]
        [ProducesResponseType(400)]
        public IActionResult GetTags(int pageNumber = 1, int pageSize = 10)
        {
            var tags = _mapper.Map<List<GetTagDetailsDto>>(_tagRepository.GetTags());
            var pagedTags = tags.Skip((pageNumber - 1) * pageSize).Take(pageSize);
            var mappedTags = _mapper.Map<List<GetTagDetailsDto>>(pagedTags);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(mappedTags);
        }

        [HttpGet("/Items")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<GetItemsDto>))]
        [ProducesResponseType(400)]
        public IActionResult GetItems(int pageNumber = 1, int pageSize = 10)
        {
            var items = _mapper.Map<List<GetItemsDto>>(_itemRepository.GetItems());
            var pagedItems = items.Skip((pageNumber - 1) * pageSize).Take(pageSize);
            var mappedItems = _mapper.Map<List<GetItemsDto>>(pagedItems);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(mappedItems);
        }
        [HttpGet("/Item/{itemId}")]
        [ProducesResponseType(200, Type = typeof(GetItemDetailsDto))]
        [ProducesResponseType(400)]
        public IActionResult GetItem(int itemId)
        {
            if (!_itemRepository.ItemExists(itemId))
            {
                return NotFound();
            }

            var item = _mapper.Map<GetItemDetailsDto>(_itemRepository.GetItem(itemId));

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if(item == null)
            {
                return NotFound();
            }

            return Ok(item);
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using GospodaWiki.Interfaces;
using GospodaWiki.Models;
using AutoMapper;
using GospodaWiki.Dto;

namespace GospodaWiki.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CharacterController : Controller
    {
        private readonly ICharacterInterface _characterRepository;
        private readonly IMapper _mapper;
        public CharacterController(ICharacterInterface characterRepository, IMapper mapper)
        {
            _characterRepository = characterRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Character>))]
        public IActionResult GetChatacters()
        {             
            var characters = _mapper.Map<List<CharacterDto>>(_characterRepository.GetCharacters());
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(characters);
        }

        [HttpGet("{characterId}")]
        [ProducesResponseType(200, Type = typeof(Character))]
        [ProducesResponseType(400)]
        public IActionResult GetCharacter(int characterId)
        {
            if (!_characterRepository.CharacterExists(characterId))
            {
                return NotFound();
            }

            var character = _mapper.Map<List<CharacterDto>>(_characterRepository.GetCharacter(characterId));

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(character);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateCharacter([FromBody] Character characterCreate)
        {
            if (characterCreate == null)
            {
                return BadRequest(ModelState);
            }

            var characterMap = _mapper.Map<Character>(characterCreate);

            if (!_characterRepository.CreateCharacter(characterMap))
            {
                ModelState.AddModelError("", $"Something went wrong saving the character {characterCreate.FirstName} {characterCreate.LastName}");
                return StatusCode(500, ModelState);
            }

            return Ok("Succesfully Created");
        }
    }
}
 
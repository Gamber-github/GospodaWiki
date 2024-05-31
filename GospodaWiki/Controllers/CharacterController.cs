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
        public IActionResult getCharacter(int characterId)
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
    }
}
 
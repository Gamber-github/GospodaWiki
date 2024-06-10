using Microsoft.AspNetCore.Mvc;
using GospodaWiki.Interfaces;
using GospodaWiki.Models;
using AutoMapper;
using GospodaWiki.Dto.Character;

namespace GospodaWiki.Controllers
{
    [Route("v1/[controller]")]
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

        [HttpGet("{characterId}")]
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

            return Ok(character);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateCharacter([FromBody] PostCharacterDto characterCreate)
        {
            if (characterCreate == null)
            {
                return BadRequest(ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var character = _characterRepository.GetCharacters().FirstOrDefault(c => c.FullName.Trim().ToUpper() == characterCreate.FullName.Trim().ToUpper());

            if (character != null)
            {
                ModelState.AddModelError("", $"Character {characterCreate.FirstName} {characterCreate.LastName} already exists");
                return StatusCode(422,ModelState);
            }

            var characterMap = _mapper.Map<PostCharacterDto>(characterCreate);

            if (!_characterRepository.CreateCharacter(characterMap))
            {
                ModelState.AddModelError("", $"Something went wrong saving the character {characterCreate.FirstName} {characterCreate.LastName}");
                return StatusCode(500, ModelState);
            }

            return Ok("Succesfully Created");
        }

        [HttpPatch("{characterId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]

        public IActionResult UpdateCharacter([FromRoute] int characterId,[FromBody] PatchCharacterDto characterUpdate)
        {
            if (characterUpdate == null || ModelState.IsValid == false)
            {
                return BadRequest(ModelState);
            }

            if (!_characterRepository.CharacterExists(characterId))
            {
                ModelState.AddModelError("", $"Character {characterUpdate.FirstName} {characterUpdate.LastName} was not found");
                return NotFound(ModelState);
            }

            var characterMap = _mapper.Map<PatchCharacterDto>(characterUpdate);

            if (!_characterRepository.UpdateCharacter(characterMap, characterId))
            {
                ModelState.AddModelError("", $"Something went wrong updating the character {characterUpdate.FirstName} {characterUpdate.LastName}");
                return StatusCode(500, ModelState);
            }

            return Ok("Succesfully Updated");
        }
    }
}
 
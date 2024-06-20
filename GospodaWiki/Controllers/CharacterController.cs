using Microsoft.AspNetCore.Mvc;
using GospodaWiki.Interfaces;
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
        public IActionResult GetUnpublishedCharacters()
        {
            var characters = _mapper.Map<List<CharactersDto>>(_characterRepository.GetUnpublishedCharacters());
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(characters);
        }

        [HttpGet("{characterId}")]
        [ProducesResponseType(200, Type = typeof(CharacterDetailsDto))]
        [ProducesResponseType(400)]

        public IActionResult GetUnpublishedCharacter(int characterId)
        {
            if (!_characterRepository.CharacterExists(characterId))
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var character = _mapper.Map<CharacterDetailsDto>(_characterRepository.GetUnpublishedCharacter(characterId));

            if (character == null)
            {
                return NotFound();
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
                return StatusCode(422, ModelState);
            }

            var characterMap = _mapper.Map<PostCharacterDto>(characterCreate);

            if (!_characterRepository.CreateCharacter(characterMap))
            {
                ModelState.AddModelError("", $"Something went wrong saving the character {characterCreate.FirstName} {characterCreate.LastName}");
                return StatusCode(500, ModelState);
            }

            return Ok("Succesfully Created");
        }

        [HttpPut("{characterId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]

        public IActionResult UpdateCharacter([FromRoute] int characterId,[FromBody] PutCharacterDto updatedCharacter)
        {
            if (updatedCharacter == null )
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!_characterRepository.CharacterExists(characterId))
            {
                return NotFound();
            }

            var characterMap = _mapper.Map<PutCharacterDto>(updatedCharacter);

            if (! _characterRepository.UpdateCharacter(characterMap, characterId))
            {
                ModelState.AddModelError("", $"Something went wrong updating the character {updatedCharacter.FirstName} {updatedCharacter.LastName}");
                return StatusCode(500, ModelState);
            }

            return Ok("Succesfully Updated");
        }

        [HttpPatch("{characterId}/publish")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult PublishCharacter([FromRoute] int characterId)
        {
            if (!_characterRepository.CharacterExists(characterId))
            {
                ModelState.AddModelError("", $"Something went wrong.");
                return NotFound(ModelState);
            }

            if (!_characterRepository.PublishCharacter(characterId))
            {
                ModelState.AddModelError("", $"Something went wrong publishing the character.");
                return StatusCode(500, ModelState);
            }

            return Ok("Succesfully Published");
        }
    }
}
 
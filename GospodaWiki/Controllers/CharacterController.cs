using Microsoft.AspNetCore.Mvc;
using GospodaWiki.Interfaces;
using AutoMapper;
using GospodaWiki.Dto.Character;
using Microsoft.AspNetCore.Authorization;

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
        [Authorize]
        [ProducesResponseType(200, Type = typeof(IEnumerable<GetCharactersDto>))]
        public IActionResult GetUnpublishedCharacters(int pageNumber = 1, int pageSize = 10)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var characters = _mapper.Map<List<GetCharactersDto>>(_characterRepository.GetUnpublishedCharacters());
            var pagedCharacters = characters.Skip((pageNumber - 1) * pageSize).Take(pageSize);
            var mappedCharacters = _mapper.Map<List<GetCharactersDto>>(pagedCharacters);

            var response = new CharacterDTOPagedListDto
            {
                Items = (IEnumerable<GetCharactersDto>)mappedCharacters,
                totalItemCount = characters.Count,
                PageSize = pageSize,
                PageNumber = pageNumber
            };

            return Ok(response);    
        }

        [HttpGet("{characterId}")]
        [Authorize]
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
        [Authorize]
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
        [Authorize]
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
        [Authorize]
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

        [HttpDelete("{characterId}")]
        [Authorize]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]

        public IActionResult DeleteCharacter([FromRoute] int characterId)
        {
            if (!_characterRepository.CharacterExists(characterId))
            {
                return NotFound();
            }

            if (!_characterRepository.DeleteCharacter(characterId))
            {
                ModelState.AddModelError("", $"Something went wrong deleting the character.");
                return StatusCode(500, ModelState);
            }

            return Ok("Succesfully Deleted");
        }
    }
}
 
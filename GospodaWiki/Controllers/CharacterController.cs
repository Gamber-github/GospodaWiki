using Microsoft.AspNetCore.Mvc;
using GospodaWiki.Interfaces;
using GospodaWiki.Models;
using AutoMapper;
using GospodaWiki.Dto;
using GospodaWiki.Helper;
using Azure;
using System.Text.Encodings.Web;
using System.Text.Json;

namespace GospodaWiki.Controllers
{
    [Route("v1")]
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

        [HttpGet("Characters")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Character>))]
        public IActionResult GetChatacters()
        {             
            var characters = _mapper.Map<List<Character>>(_characterRepository.GetCharacters());
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var json = SerializeObject(characters);

            return Ok(json);
        }

        [HttpGet("Character/{characterId}")]
        [ProducesResponseType(200, Type = typeof(Character))]
        [ProducesResponseType(400)]
        public IActionResult GetCharacter(int characterId)
        {
            if (!_characterRepository.CharacterExists(characterId))
            {
                return NotFound();
            }

            var character = _mapper.Map<Character>(_characterRepository.GetCharacter(characterId));

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var json = SerializeObject(character);

            return Ok(json);
        }

        private object SerializeObject(object obj)
        {
            var json = JsonSerializer.Serialize(obj, new JsonSerializerOptions
            {
                IgnoreNullValues = true,
                WriteIndented = true,
                Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
            });

            return json;
        }

        [HttpPost("Character")]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public IActionResult CreateCharacter([FromBody] CharacterDto characterCreate)
        {
            if (characterCreate == null)
            {
                return BadRequest(ModelState);
            }

            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var character = _characterRepository.GetCharacters()
                .Where(c => c.FirstName.Trim().ToLower() == characterCreate.FirstName.Trim().ToLower() && c.LastName.Trim().ToLower() == characterCreate.LastName.Trim().ToLower())
                .FirstOrDefault();

            if (character != null)
            {
                ModelState.AddModelError("", $"Character {characterCreate.FirstName} {characterCreate.LastName} already exists.");
                return StatusCode(422, ModelState);
            }

            var characterMap = _mapper.Map<Character>(characterCreate);

            if (!_characterRepository.CreateCharacter(characterMap))
            {
                ModelState.AddModelError("", $"Something went wrong saving the character {characterCreate.FirstName} {characterCreate.LastName}");
                return StatusCode(500, ModelState);
            }

            return Ok("Succesfully Created");
        }

        [HttpPut("Character/{characterId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public IActionResult UpdateCharacter(int characterId, [FromBody] CharacterDto characterUpdate)
        {
            if (characterUpdate == null)
            {
                return BadRequest(ModelState);
            }

            if (characterId != characterUpdate.CharacterId)
            {
                return BadRequest(ModelState);
            }

            if (!_characterRepository.CharacterExists(characterId))
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var characterMap = _mapper.Map<Character>(characterUpdate);

            if (!_characterRepository.UpdateCharacter(characterMap))
            {
                ModelState.AddModelError("", $"Something went wrong updating the character {characterUpdate.FirstName} {characterUpdate.LastName}");
                return StatusCode(500, ModelState);
            }

            return Ok("Succesfully Updated");
        }
    }
}
 
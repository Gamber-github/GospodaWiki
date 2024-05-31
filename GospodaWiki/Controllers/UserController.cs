using AutoMapper;
using GospodaWiki.Interfaces;
using GospodaWiki.Models;
using Microsoft.AspNetCore.Mvc;
using GospodaWiki.Dto;

namespace GospodaWiki.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly IUserInterface _userRepository;
        private readonly IMapper _mapper;
        public UserController(IUserInterface userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        [HttpGet("{userId}")]
        [ProducesResponseType(200, Type = typeof(User))]
        [ProducesResponseType(400)]
        public IActionResult getUser(int userId)
        {
            if (!_userRepository.UserExists(userId))
            {
                return BadRequest();
            }

            var user = _mapper.Map<List<UserDto>>(_userRepository.GetUser(userId));
            
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(user);
        }
    }
}

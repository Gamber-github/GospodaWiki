using GospodaWiki.Dto.Account;
using GospodaWiki.Interfaces;
using GospodaWiki.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace GospodaWiki.Controllers
{
    [Route("v1/account")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly ITokenService _tokenService;
        private readonly SignInManager<AppUser> _signInManager;
        public AccountController(UserManager<AppUser> userManager, ITokenService tokenService, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _tokenService = tokenService;
            _signInManager = signInManager;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var appuser = new AppUser
                {
                    UserName = registerDto.UserName,
                    Email = registerDto.Email
                };

                var createdUser = await _userManager.CreateAsync(appuser, registerDto.Password);

                if(createdUser.Succeeded)
                {
                 var roleResult = await _userManager.AddToRoleAsync(appuser, "User");
                 
                    if(roleResult.Succeeded)
                    {
                        return Ok(
                            new NewUserDto
                            {
                                UserName = appuser.UserName,
                                Email = appuser.Email,
                                Token = _tokenService.CreateToken(appuser)
                            });
                    }
                    else
                    {
                        return BadRequest("Failed to add user to role");
                    }
                }
                else
                {
                    return StatusCode(500, createdUser.Errors);
                }


            } catch (Exception e)
            {
                return StatusCode(500,e.Message);
            }


        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.UserName == loginDto.UserName);

            if(user == null)
            {
                return Unauthorized("Invalid username or password");
            }

            var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);

            if(!result.Succeeded) {
                return Unauthorized("Invalid username or password");
            }

            return Ok(new NewUserDto
            {
                UserName = user.UserName,
                Email = user.Email,
                Token = _tokenService.CreateToken(user)
            });
            
        }

        [HttpGet("authorize")]
        [Authorize]
        public async Task<IActionResult> GetAuthenticatedUser()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userName = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.GivenName).Value;

            if (userName == null)
            {
                return Unauthorized();
            }

            var user =  _userManager.Users.FirstOrDefault(x => x.UserName == userName);


            if (user == null)
            {
                return Unauthorized();
            }

            return Ok(new NewUserDto
            {
                UserName = user.UserName ,
                Email = user.Email,
            });
        }

    }

}

using Application.Dtos.AppUserDtos;
using Application.Services.Interfaces.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;

namespace Online_Electronic_Store.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {

        #region Fields
        private readonly IUserService _userService;
        #endregion

        #region Constructor
        public AuthenticationController(IUserService userService) { _userService = userService; }
        #endregion

        #region EndPoints
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto dto)
        {
            var res = await _userService.RegisterAsync(dto);
            if (!res.Success) return BadRequest(res);
            return Ok(res);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {
            var res = await _userService.LoginAsync(dto);
            if (!res.Success) return BadRequest(res);
            return Ok(res);
        }

        [HttpGet("test")]
        [Authorize]
        public IActionResult Test() => Ok(new { Message = "You are authenticated", User = User.Identity!.Name });

        [HttpGet("claims")]
        [Authorize]
        public IActionResult GetClaims()
        {
            var claims = User.Claims.Select(c => new { c.Type, c.Value }).ToList();
            return Ok(claims);
        }

        [HttpGet("me")]
        [Authorize]
        public IActionResult Me()
        {
            var email = User.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Sub)?.Value;
            return Ok(new { Email = email });
        }
        #endregion

    }
}



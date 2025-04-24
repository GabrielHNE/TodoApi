using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Versioning;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Todo.Application.DTOs;
using Todo.Application.Interfaces;
using Todo.Domain.Interfaces.Services;

namespace Todo.WebAPI.Controllers
{
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IJwtService _jwtService;
        
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterAsync([FromBody] RegisterDTO reg)
        {
            var token = await _authService.RegisterAsync(reg);
            return Ok(new { token });
        }

        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync ([FromBody] LoginDTO login)
        {
            var token = await _authService.LoginAsync(login);
            return Ok(new { token });
        }

        [HttpGet("/oauth/google/login")]
        [AllowAnonymous]
        public IActionResult GoogleLogin()
        {
            
            var props = new AuthenticationProperties
            {
                RedirectUri = Url.Action(nameof(GoogleCallback))
            };
            
            return Challenge(props, "Google");
        }

        [HttpGet("/oauth/google/callback")]
        [AllowAnonymous]
        public async Task<IActionResult> GoogleCallback()
        {
            var result = await HttpContext.AuthenticateAsync("Google");

            if (!result.Succeeded)
            {
                return Unauthorized("Google authentication failed.");
            }

            var email = result.Principal.Claims.FirstOrDefault(c => c.Type.Contains("emailaddress"))?.Value;
            var name = result.Principal.Claims.FirstOrDefault(c => c.Type.Contains("name"))?.Value;
            var id = result.Principal.Claims.FirstOrDefault(c => c.Type.Contains("nameidentifier"))?.Value;


            var token = _jwtService.GenerateToken("blabla", "blabla");
            
            return Ok(new { token });
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Versioning;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Todo.Application.DTOs;
using Todo.Application.Interfaces;

namespace Todo.WebAPI.Controllers
{
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        
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
    }
}
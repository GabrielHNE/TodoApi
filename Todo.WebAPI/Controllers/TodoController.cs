using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Mime;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Todo.Application.Mappers;
using Todo.Application.Utilities;
using Todo.Domain.DTOs;
using Todo.Domain.Entities;
using Todo.Domain.Exceptions;
using Todo.Domain.Interfaces.Repositories;
using Todo.Domain.Interfaces.Services;

namespace Todo.WebAPI.Controllers
{
    [Authorize]
    [Route("todos")]
    [ApiController]
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    public class TodoController : Controller
    {
        private readonly ITodoItemService _todoService;
        private readonly IUserService _userService;


        public TodoController(ITodoItemService todoService, IUserService userService)
        {
            _todoService = todoService;
            _userService = userService;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] TodoDTO todo)
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var user = await _userService.GetByEmailAsync(email);

            if(user == null)
                return Unauthorized();

            var tEntity = todo.ToModel();
            tEntity.IdUser = user.Id;

            var t = await _todoService.CreateAsync(tEntity);
            
            return Ok(t.ToDTO());
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> Update([FromBody] TodoDTO todo, [FromRoute] long id)
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var user = await _userService.GetByEmailAsync(email);

            var t = await _todoService.UpdateAsync(id, todo, user.Id);
            
            return Ok(t.ToDTO());
        }

        [HttpGet]
        public async Task<IActionResult> All([FromQuery] int page = 1, [FromQuery] int limit = 100)
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var user = await _userService.GetByEmailAsync(email);

            var todos = await _todoService.AllTodosAsync(user.Id);
            var pagedList = PagedList<TodoDTO>.ToPagedList(todos.Select(c => c.ToDTO()), page, limit);

            return Ok(pagedList);            
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete([FromRoute] long id)
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var user = await _userService.GetByEmailAsync(email);

            var t = await _todoService.DeleteAsync(id, user.Id);
            
            return Ok(t.ToDTO());
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Todo.Domain.DTOs;
using Todo.Domain.Entities;
using Todo.Domain.Exceptions;
using Todo.Domain.Interfaces.Repositories;
using Todo.Domain.Interfaces.Services;

namespace Todo.Application.Services
{
    public class TodoItemService : ITodoItemService
    {
        private readonly ITodoItemRepository _todoItemRepository;

        public TodoItemService(ITodoItemRepository todoItemRepository)
        {
            _todoItemRepository = todoItemRepository;
        }

        public async ValueTask<IEnumerable<TodoItem>> AllTodosAsync(long idUser)
            => await _todoItemRepository.GetAllByUserIdAsync(idUser);
        
        public async ValueTask<TodoItem> GetByIdAsync(long id)
            => await _todoItemRepository.GetByIdAsync(id);

        public async ValueTask<TodoItem> CreateAsync(TodoItem item)
            => await _todoItemRepository.SaveAsync(item);

        public async ValueTask<TodoItem> UpdateAsync(long idTodo, TodoDTO itemDTO, long idUser)    
        {
            var obj = await _todoItemRepository.GetByIdAsync(idTodo);

            if(obj == null)
                throw new NotFoundException("Not Found");
            
            if(CheckTodoBelongsToUser(obj, idUser))
                throw new UnauthorizedException("Not authorized");

            obj.Title = itemDTO.Title;
            obj.Description = itemDTO.Description;

            await _todoItemRepository.SaveAsync(obj);

            return obj;
        } 

        public async Task DeleteAsync(long idTodo, long idUser)
        {
            var todo = await _todoItemRepository.GetByIdAsync(idTodo);

            if(todo == null)
                throw new NotFoundException("NotFound");
            
            if(CheckTodoBelongsToUser(todo, idUser))
                throw new UnauthorizedException("Not authorized");

            await _todoItemRepository.DeleteAsync(idTodo);
        }

        private bool CheckTodoBelongsToUser(TodoItem todo, long userId)
            => todo.IdUser == userId;
    }
}
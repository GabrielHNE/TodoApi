using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Todo.Domain.DTOs;
using Todo.Domain.Entities;
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
            => await _todoItemRepository.CreateAsync(item);

        public async ValueTask<TodoItem> UpdateAsync(long id, TodoDTO itemDTO)
        {
            var obj = await _todoItemRepository.GetByIdAsync(id);

            if(obj == null)
                throw new Exception("NotFound");
            
            obj.Title = itemDTO.Title;
            obj.Description = itemDTO.Description;

            await _todoItemRepository.UpdateAsync(obj);

            return obj;
        } 

        public async ValueTask<TodoItem> DeleteAsync(long id)
        {
            var todo = await _todoItemRepository.GetByIdAsync(id);

            if(todo == null)
                throw new Exception("NotFound");
            
            return await _todoItemRepository.DeleteAsync(todo);
        }
    }
}
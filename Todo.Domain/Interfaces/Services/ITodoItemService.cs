using Todo.Domain.DTOs;
using Todo.Domain.Entities;

namespace Todo.Domain.Interfaces.Services
{
    public interface ITodoItemService
    {
        ValueTask<IEnumerable<TodoItem>> AllTodosAsync(long idUser);
        ValueTask<TodoItem> GetByIdAsync(long id);
        ValueTask<TodoItem> CreateAsync(TodoItem item);
        ValueTask<TodoItem> UpdateAsync(long idTodo, TodoDTO itemDTO, long idUser);
        ValueTask<TodoItem> DeleteAsync(long idTodo, long idUser);
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Todo.Domain.Entities;

namespace Todo.Domain.Interfaces.Repositories
{
    public interface ITodoItemRepository
    {
        ValueTask<IEnumerable<TodoItem>> GetAllByUserIdAsync(long userId);
        ValueTask<TodoItem> GetByIdAsync(long id);
        ValueTask<TodoItem> CreateAsync(TodoItem todoItem);
        ValueTask<TodoItem> UpdateAsync(TodoItem todoItem);
        ValueTask<TodoItem> DeleteAsync(TodoItem todoItem);
    }
}
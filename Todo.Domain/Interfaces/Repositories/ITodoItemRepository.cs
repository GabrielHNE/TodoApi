using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Todo.Domain.Entities;

namespace Todo.Domain.Interfaces.Repositories
{
    public interface ITodoItemRepository : IRepository<TodoItem>
    {
        ValueTask<IEnumerable<TodoItem>> GetAllByUserIdAsync(long userId);
    }
}
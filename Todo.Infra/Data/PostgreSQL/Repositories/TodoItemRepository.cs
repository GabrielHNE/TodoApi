using System.Data.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Todo.Domain.Entities;
using Todo.Domain.Interfaces.Repositories;
using Todo.Infra.Data.PostgresSQL.Context;

namespace Todo.Infra.Data.PostgreSQL.Repositories
{
    public class TodoItemRepository : ITodoItemRepository
    {
        ApplicationDbContext _context;
        public TodoItemRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async ValueTask<TodoItem> CreateAsync(TodoItem todoItem)
        {
            _context.Add(todoItem);
            await _context.SaveChangesAsync();
            return todoItem;
        }

        public async ValueTask<TodoItem> DeleteAsync(TodoItem todoItem)
        {
            _context.Remove(todoItem);
            await  _context.SaveChangesAsync();

            return todoItem;
        }

        public async ValueTask<IEnumerable<TodoItem>> GetAllByUserIdAsync(long userId)
            => await _context.TodosItem.Where(c => c.IdUser == userId).ToListAsync();

        public async ValueTask<TodoItem> GetByIdAsync(long id)
            => await _context.TodosItem.SingleOrDefaultAsync(x => x.Id == id);

        public async ValueTask<TodoItem> UpdateAsync(TodoItem todoItem)
        {
            _context.Update(todoItem);
            await _context.SaveChangesAsync();

            return todoItem;
        }
    }
}
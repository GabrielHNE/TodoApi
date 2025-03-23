using System.Data.Common;
using System.Security.Cryptography.X509Certificates;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Todo.Domain.Entities;
using Todo.Domain.Interfaces.Repositories;
using Todo.Infra.Data.PostgresSQL.Context;

namespace Todo.Infra.Data.PostgreSQL.Repositories
{
    public class TodoItemRepository : RepositoryBase<TodoItem>, ITodoItemRepository
    {
        public TodoItemRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async ValueTask<IEnumerable<TodoItem>> GetAllByUserIdAsync(long userId)
            => await this.DbSet.Include(c => c.User).Where(c => userId == c.User.Id).ToListAsync();
    }
}
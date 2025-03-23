using Microsoft.EntityFrameworkCore;
using Todo.Domain.Interfaces.Repositories;
using Todo.Domain.Models;
using Todo.Infra.Data.PostgresSQL.Context;

namespace Todo.Infra.Data.PostgreSQL.Repositories
{
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        
        public UserRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async ValueTask<User> GetByEmailAsync(string email)
            => await this.DbSet.FirstAsync(x => x.Email.ToLower().Equals(email.ToLower(), StringComparison.OrdinalIgnoreCase));
            // => await _context.Users.FirstAsync(u => u.Email.ToLower().Equals(email.ToLower()));
            

        public async ValueTask<bool> ExistsByEmailAsync(string email)
            => await this.DbSet.AnyAsync(x => x.Email.ToLower().Equals(email.ToLower(), StringComparison.OrdinalIgnoreCase));
            // => await _context.Users.AnyAsync(u => u.Email.ToLower().Equals(email.ToLower()));

    }
}
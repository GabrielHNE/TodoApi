using Microsoft.EntityFrameworkCore;
using Todo.Domain.Interfaces.Repositories;
using Todo.Domain.Models;
using Todo.Infra.Data.PostgresSQL.Context;

namespace Todo.Infra.Data.PostgreSQL.Repositories
{
    public class UserRepository : IUserRepository
    {
        ApplicationDbContext _context;
        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async ValueTask<User> CreateAsync(User user)
        {
            _context.Add(user);
            await _context.SaveChangesAsync();
            
            return user;
        }

        public async ValueTask<User> DeleteAsync(User user)
        {
            _context.Remove(user);
            await  _context.SaveChangesAsync();

            return user;
        }

        public async ValueTask<User> GetByEmailAsync(string email)
            => await _context.Users.FirstAsync(u => u.Email.ToLower().Equals(email.ToLower()));

        public async ValueTask<bool> ExistsByEmailAsync(string email)
            => await _context.Users.AnyAsync(u => u.Email.ToLower().Equals(email.ToLower()));

        public async ValueTask<User> GetByIdAsync(int id)
            => await _context.Users.SingleOrDefaultAsync(u => u.Id == id);

        public async ValueTask<User> GetTodosAsync(int id)
            => await _context.Users.Include(c => c.Todos).SingleOrDefaultAsync(u => u.Id == id);

        public async ValueTask<User> UpdateAsync(User user)
        {
            _context.Update(user);
            await _context.SaveChangesAsync();

            return user;
        }
    }
}
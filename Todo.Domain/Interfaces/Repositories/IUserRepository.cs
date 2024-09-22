using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Todo.Domain.Models;

namespace Todo.Domain.Interfaces.Repositories;
public interface IUserRepository
{
    ValueTask<User> GetByIdAsync(int id);
    ValueTask<User> GetTodosAsync(int id);
    ValueTask<User> GetByEmailAsync(string email);
    ValueTask<bool> ExistsByEmailAsync(string Email);
    ValueTask<User> CreateAsync(User user);
    ValueTask<User> UpdateAsync(User user);
    ValueTask<User> DeleteAsync(User user);
}
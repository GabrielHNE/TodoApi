using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Todo.Domain.Models;

namespace Todo.Domain.Interfaces.Repositories;
public interface IUserRepository : IRepository<User>
{
    ValueTask<User> GetByEmailAsync(string email);
    ValueTask<bool> ExistsByEmailAsync(string Email);
}
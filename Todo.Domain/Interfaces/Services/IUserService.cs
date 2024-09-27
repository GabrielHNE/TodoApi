using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Todo.Domain.Models;

namespace Todo.Domain.Interfaces.Services
{
    public interface IUserService
    {
        ValueTask<User> GetByEmailAsync(string email);
    }
}
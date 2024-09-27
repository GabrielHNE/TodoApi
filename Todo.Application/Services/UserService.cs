using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Todo.Domain.Interfaces.Repositories;
using Todo.Domain.Interfaces.Services;
using Todo.Domain.Models;

namespace Todo.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async ValueTask<User> GetByEmailAsync(string email)
            => await _userRepository.GetByEmailAsync(email);
    }
}
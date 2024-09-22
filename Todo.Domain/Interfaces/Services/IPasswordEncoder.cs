using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Todo.Domain.Interfaces.Services
{
    public interface IPasswordEncoder
    {
        string HashPassword(string password);
        bool Verify(string password, string encodedPassword);
        Task<string> HashPasswordAsync(string password);
        Task<bool> VerifyAsync(string password, string encodedPassword);
    }
}
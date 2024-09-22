using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Todo.Domain.Interfaces.Services;

namespace Todo.Infra.Security
{
    public class PasswordEncoder : IPasswordEncoder
    {
        public string HashPassword(string password)
            => BCrypt.Net.BCrypt.HashPassword(password);

        public async Task<string> HashPasswordAsync(string password)
            => await Task.FromResult(this.HashPassword(password));

        public bool Verify(string password, string encodedPassword)
            => BCrypt.Net.BCrypt.Verify(password, encodedPassword);

        public async Task<bool> VerifyAsync(string password, string encodedPassword)
            => await Task.FromResult(this.Verify(password, encodedPassword));
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Todo.Domain.Interfaces.Services
{
    public interface IJwtService
    {
        public string GenerateToken(string username, string email);
    }
}
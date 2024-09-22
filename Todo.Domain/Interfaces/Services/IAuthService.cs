using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Todo.Application;
using Todo.Application.DTOs;
using Todo.Domain.Models;

namespace Todo.Application.Interfaces
{
    public interface IAuthService
    {
        ValueTask<string> RegisterAsync (RegisterDTO user);
        ValueTask<string> LoginAsync (LoginDTO login);
    }
}
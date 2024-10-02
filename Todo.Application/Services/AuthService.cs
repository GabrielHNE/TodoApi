using Todo.Application.DTOs;
using Todo.Application.Interfaces;
using Todo.Domain.Exceptions;
using Todo.Domain.Interfaces.Repositories;
using Todo.Domain.Interfaces.Services;
using Todo.Domain.Models;

namespace Todo.Application.Services
{
    public class AuthService : IAuthService
    {
        private IUserRepository _userRepository;
        private IJwtService _jwtService;
        private IPasswordEncoder _passwordEncoder;

        public AuthService(IUserRepository userRepository, IJwtService jwtService, IPasswordEncoder passwordEncoder)
        {
            _userRepository = userRepository;
            _jwtService = jwtService;
            _passwordEncoder = passwordEncoder;
        }
        
        public async ValueTask<string> RegisterAsync(RegisterDTO user)
        {
            if(await _userRepository.ExistsByEmailAsync(user.Email))
            {
                throw new UnauthorizedException("Email is already in use");   
            }
            
            User u = new User { Name = user.Name, Email = user.Email, Password = await _passwordEncoder.HashPasswordAsync(user.Password) };
            
            await _userRepository.CreateAsync(u);

            return _jwtService.GenerateToken(u.Name, u.Email);
        }
        
        public async ValueTask<string> LoginAsync(LoginDTO login){
            var user = await _userRepository.GetByEmailAsync(login.Email);

            if(user == null || !_passwordEncoder.Verify(login.Password, user.Password))
                throw new UnauthorizedException("Unauthorized");
            
            return _jwtService.GenerateToken(user.Name, user.Email);
        }

    }
}
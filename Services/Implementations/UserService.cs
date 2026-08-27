using API_REST.DTOs;
using API_REST.Models;
using API_REST.Repositories.Interfaces;
using API_REST.Services.Interfaces;
using System.Xml.Linq;

namespace API_REST.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        // On injecte le repository ici
        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<IEnumerable<UserReadDto>> GetAllUsersAsync()
        {
            var users = await _userRepository.GetAllAsync();
            return users.Select(u => new UserReadDto
            {
                Id = u.Id,
                Name = u.Name,
                FirstName = u.FirstName,
                Email = u.Email,
                Role = u.Role
            });
        }

        public async Task<UserReadDto?> GetUserByIdAsync(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null) return null;

            return new UserReadDto
            {
                Id = user.Id,
                Name = user.Name,
                FirstName = user.FirstName,
                Email = user.Email,
                Role = user.Role
            };
        }

        public async Task<UserReadDto> CreateUserAsync(UserCreateDto userCreateDto)
        {
            var user = new User
            {
                Name = userCreateDto.Name,
                FirstName = userCreateDto.FirstName,
                Email = userCreateDto.Email,
                Role = userCreateDto.Role
            };

            await _userRepository.AddAsync(user);
            await _userRepository.SaveChangesAsync();

            return new UserReadDto
            {
                Id = user.Id,
                Name = user.Name,
                FirstName = user.FirstName,
                Email = user.Email,
                Role = user.Role
            };
        }

        public async Task<bool> UpdateUserAsync(int id, UserCreateDto userCreateDto)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null) return false;

            user.Name = userCreateDto.Name;
            user.FirstName = userCreateDto.FirstName;
            user.Email = userCreateDto.Email;
            user.Role = userCreateDto.Role;

            _userRepository.Update(user);
            return await _userRepository.SaveChangesAsync();
        }

        public async Task<bool> DeleteUserAsync(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null) return false;

            _userRepository.Delete(user);
            return await _userRepository.SaveChangesAsync();
        }
    }
}
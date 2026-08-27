using API_REST.DTOs;

namespace API_REST.Services.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<UserReadDto>> GetAllUsersAsync();
        Task<UserReadDto?> GetUserByIdAsync(int id);
        Task<UserReadDto> CreateUserAsync(UserCreateDto userCreateDto);
        Task<bool> UpdateUserAsync(int id, UserCreateDto userCreateDto);
        Task<bool> DeleteUserAsync(int id);
    }
}

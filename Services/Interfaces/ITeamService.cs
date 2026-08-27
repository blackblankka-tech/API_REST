using API_REST.DTOs;

namespace API_REST.Services.Interfaces
{
    public interface ITeamService
    {
        Task<IEnumerable<TeamReadDto>> GetAllTeamsAsync();
        Task<TeamReadDto?> GetTeamByIdAsync(int id);
        Task<TeamReadDto> CreateTeamAsync(TeamCreateDto teamCreateDto);
        Task<bool> UpdateTeamAsync(int id, TeamCreateDto teamUpdateDto);
        Task<bool> DeleteTeamAsync(int id);
    }
}

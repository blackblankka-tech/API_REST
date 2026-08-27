using API_REST.DTOs;

namespace API_REST.Services.Interfaces
{
    public interface IProjectService
    {
        Task<IEnumerable<ProjectReadDto>> GetAllProjectsAsync();
        Task<ProjectReadDto?> GetProjectByIdAsync(int id);
        Task<ProjectReadDto?> CreateProjectAsync(ProjectCreateDto projectCreateDto); // Peut renvoyer null si la Team n'existe pas
        Task<bool> UpdateProjectAsync(int id, ProjectCreateDto projectUpdateDto);
        Task<bool> DeleteProjectAsync(int id);
    }
}

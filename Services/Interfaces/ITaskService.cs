using API_REST.DTOs;

namespace API_REST.Services.Interfaces
{
    public interface ITaskService
    {
        Task<IEnumerable<TaskReadDto>> GetAllTasksAsync();
        Task<TaskReadDto?> GetTaskByIdAsync(int id);
        Task<TaskReadDto?> CreateTaskAsync(TaskCreateDto taskCreateDto); // Renvoie null si Projet ou User introuvable
        Task<bool> UpdateTaskAsync(int id, TaskCreateDto taskUpdateDto);
        Task<bool> DeleteTaskAsync(int id);
    }
}

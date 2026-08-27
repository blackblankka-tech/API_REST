using API_REST.Data;
using API_REST.DTOs;
using API_REST.Models;
using API_REST.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace API_REST.Services.Implementations
{
    public class TaskService : ITaskService
    {
        private readonly AppDbContext _context;

        public TaskService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TaskReadDto>> GetAllTasksAsync()
        {
            return await _context.TaskItems
                .Select(t => new TaskReadDto
                {
                    Id = t.Id,
                    Title = t.Title,
                    Description = t.Description,
                    Status = t.Status,
                    Priority = t.Priority,
                    DueDate = t.DueDate,
                    ProjectId = t.ProjectId,
                    UserId = t.UserId
                }).ToListAsync();
        }

        public async Task<TaskReadDto?> GetTaskByIdAsync(int id)
        {
            var task = await _context.TaskItems.FindAsync(id);
            if (task == null) return null;

            return new TaskReadDto
            {
                Id = task.Id,
                Title = task.Title,
                Description = task.Description,
                Status = task.Status,
                Priority = task.Priority,
                DueDate = task.DueDate,
                ProjectId = task.ProjectId,
                UserId = task.UserId
            };
        }

        public async Task<TaskReadDto?> CreateTaskAsync(TaskCreateDto taskCreateDto)
        {
            // Sécurité L3 : On s'assure que le projet ET l'utilisateur existent avant d'insérer
            var projectExists = await _context.Projects.AnyAsync(p => p.Id == taskCreateDto.ProjectId);
            var userExists = await _context.Users.AnyAsync(u => u.Id == taskCreateDto.UserId);

            if (!projectExists || !userExists) return null;

            var task = new TaskItem
            {
                Title = taskCreateDto.Title,
                Description = taskCreateDto.Description,
                Status = taskCreateDto.Status,
                Priority = taskCreateDto.Priority,
                DueDate = taskCreateDto.DueDate.ToUniversalTime(), // Obligatoire pour Postgres timestamptz
                ProjectId = taskCreateDto.ProjectId,
                UserId = taskCreateDto.UserId
            };

            _context.TaskItems.Add(task);
            await _context.SaveChangesAsync();

            return new TaskReadDto
            {
                Id = task.Id,
                Title = task.Title,
                Description = task.Description,
                Status = task.Status,
                Priority = task.Priority,
                DueDate = task.DueDate,
                ProjectId = task.ProjectId,
                UserId = task.UserId
            };
        }

        public async Task<bool> UpdateTaskAsync(int id, TaskCreateDto taskUpdateDto)
        {
            var task = await _context.TaskItems.FindAsync(id);
            if (task == null) return false;

            // Vérification des nouvelles clés étrangères lors de la modification
            var projectExists = await _context.Projects.AnyAsync(p => p.Id == taskUpdateDto.ProjectId);
            var userExists = await _context.Users.AnyAsync(u => u.Id == taskUpdateDto.UserId);

            if (!projectExists || !userExists) return false;

            task.Title = taskUpdateDto.Title;
            task.Description = taskUpdateDto.Description;
            task.Status = taskUpdateDto.Status;
            task.Priority = taskUpdateDto.Priority;
            task.DueDate = taskUpdateDto.DueDate.ToUniversalTime();
            task.ProjectId = taskUpdateDto.ProjectId;
            task.UserId = taskUpdateDto.UserId;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteTaskAsync(int id)
        {
            var task = await _context.TaskItems.FindAsync(id);
            if (task == null) return false;

            _context.TaskItems.Remove(task);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
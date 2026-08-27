using API_REST.Data;
using API_REST.DTOs;
using API_REST.Models;
using API_REST.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace API_REST.Services.Implementations
{
    public class ProjectService : IProjectService
    {
        private readonly AppDbContext _context;

        public ProjectService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ProjectReadDto>> GetAllProjectsAsync()
        {
            return await _context.Projects
                .Select(p => new ProjectReadDto
                {
                    Id = p.Id,
                    Name = p.Name,
                    Description = p.Description,
                    StartDate = p.StartDate,
                    EndDate = p.EndDate,
                    Status = p.Status,
                    TeamId = p.TeamId
                }).ToListAsync();
        }

        public async Task<ProjectReadDto?> GetProjectByIdAsync(int id)
        {
            var project = await _context.Projects.FindAsync(id);
            if (project == null) return null;

            return new ProjectReadDto
            {
                Id = project.Id,
                Name = project.Name,
                Description = project.Description,
                StartDate = project.StartDate,
                EndDate = project.EndDate,
                Status = project.Status,
                TeamId = project.TeamId
            };
        }

        public async Task<ProjectReadDto?> CreateProjectAsync(ProjectCreateDto projectCreateDto)
        {
            // Sécurité L3 : On vérifie si la Team existe vraiment en BDD avant de créer le projet
            var teamExists = await _context.Teams.AnyAsync(t => t.Id == projectCreateDto.TeamId);
            if (!teamExists) return null;

            var project = new Project
            {
                Name = projectCreateDto.Name,
                Description = projectCreateDto.Description,
                // .ToUniversalTime() est OBLIGATOIRE pour PostgreSQL avec le type 'timestamp with time zone'
                StartDate = projectCreateDto.StartDate.ToUniversalTime(),
                EndDate = projectCreateDto.EndDate.ToUniversalTime(),
                Status = projectCreateDto.Status,
                TeamId = projectCreateDto.TeamId
            };

            _context.Projects.Add(project);
            await _context.SaveChangesAsync();

            return new ProjectReadDto
            {
                Id = project.Id,
                Name = project.Name,
                Description = project.Description,
                StartDate = project.StartDate,
                EndDate = project.EndDate,
                Status = project.Status,
                TeamId = project.TeamId
            };
        }

        public async Task<bool> UpdateProjectAsync(int id, ProjectCreateDto projectUpdateDto)
        {
            var project = await _context.Projects.FindAsync(id);
            if (project == null) return false;

            // On vérifie aussi si la nouvelle Team existe lors de la modification
            var teamExists = await _context.Teams.AnyAsync(t => t.Id == projectUpdateDto.TeamId);
            if (!teamExists) return false;

            project.Name = projectUpdateDto.Name;
            project.Description = projectUpdateDto.Description;
            project.StartDate = projectUpdateDto.StartDate.ToUniversalTime();
            project.EndDate = projectUpdateDto.EndDate.ToUniversalTime();
            project.Status = projectUpdateDto.Status;
            project.TeamId = projectUpdateDto.TeamId;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteProjectAsync(int id)
        {
            var project = await _context.Projects.FindAsync(id);
            if (project == null) return false;

            _context.Projects.Remove(project);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
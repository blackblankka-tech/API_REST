using API_REST.DTOs;
using API_REST.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API_REST.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProjectsController : ControllerBase
    {
        private readonly IProjectService _projectService;

        public ProjectsController(IProjectService projectService)
        {
            _projectService = projectService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProjectReadDto>>> GetAllProjects()
        {
            var projects = await _projectService.GetAllProjectsAsync();
            return Ok(projects);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProjectReadDto>> GetProjectById(int id)
        {
            var project = await _projectService.GetProjectByIdAsync(id);
            if (project == null) return NotFound(new { message = $"Projet avec l'ID {id} introuvable." });
            return Ok(project);
        }

        [HttpPost]
        public async Task<ActionResult<ProjectReadDto>> CreateProject(ProjectCreateDto projectCreateDto)
        {
            var createdProject = await _projectService.CreateProjectAsync(projectCreateDto);

            if (createdProject == null)
            {
                return BadRequest(new { message = $"Impossible de créer le projet. L'équipe avec l'ID {projectCreateDto.TeamId} n'existe pas." });
            }

            return CreatedAtAction(nameof(GetProjectById), new { id = createdProject.Id }, createdProject);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProject(int id, ProjectCreateDto projectUpdateDto)
        {
            var success = await _projectService.UpdateProjectAsync(id, projectUpdateDto);
            if (!success) return BadRequest(new { message = $"Mise à jour impossible. Vérifiez l'ID du projet ou l'existence de la Team {projectUpdateDto.TeamId}." });
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProject(int id)
        {
            var success = await _projectService.DeleteProjectAsync(id);
            if (!success) return NotFound(new { message = $"Projet avec l'ID {id} introuvable." });
            return NoContent();
        }
    }
}
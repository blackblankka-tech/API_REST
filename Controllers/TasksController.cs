using API_REST.DTOs;
using API_REST.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API_REST.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TasksController : ControllerBase
    {
        private readonly ITaskService _taskService;

        public TasksController(ITaskService taskService)
        {
            _taskService = taskService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TaskReadDto>>> GetAllTasks()
        {
            var tasks = await _taskService.GetAllTasksAsync();
            return Ok(tasks);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TaskReadDto>> GetTaskById(int id)
        {
            var task = await _taskService.GetTaskByIdAsync(id);
            if (task == null) return NotFound(new { message = $"Tâche avec l'ID {id} introuvable." });
            return Ok(task);
        }

        [HttpPost]
        public async Task<ActionResult<TaskReadDto>> CreateTask(TaskCreateDto taskCreateDto)
        {
            var createdTask = await _taskService.CreateTaskAsync(taskCreateDto);

            if (createdTask == null)
            {
                return BadRequest(new { message = "Impossible de créer la tâche. Vérifiez que le ProjectId et le UserId existent bien dans la base." });
            }

            return CreatedAtAction(nameof(GetTaskById), new { id = createdTask.Id }, createdTask);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTask(int id, TaskCreateDto taskUpdateDto)
        {
            var success = await _taskService.UpdateTaskAsync(id, taskUpdateDto);
            if (!success)
            {
                return BadRequest(new { message = "Modification impossible. Vérifiez l'ID de la tâche ou la validité des ID Projet/Utilisateur." });
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTask(int id)
        {
            var success = await _taskService.DeleteTaskAsync(id);
            if (!success) return NotFound(new { message = $"Tâche avec l'ID {id} introuvable." });
            return NoContent();
        }
    }
}
using API_REST.DTOs;
using API_REST.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API_REST.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TeamsController : ControllerBase
    {
        private readonly ITeamService _teamService;

        public TeamsController(ITeamService teamService)
        {
            _teamService = teamService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TeamReadDto>>> GetAllTeams()
        {
            var teams = await _teamService.GetAllTeamsAsync();
            return Ok(teams);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TeamReadDto>> GetTeamById(int id)
        {
            var team = await _teamService.GetTeamByIdAsync(id);
            if (team == null) return NotFound(new { message = $"Équipe avec l'ID {id} introuvable." });
            return Ok(team);
        }

        [HttpPost]
        public async Task<ActionResult<TeamReadDto>> CreateTeam(TeamCreateDto teamCreateDto)
        {
            var createdTeam = await _teamService.CreateTeamAsync(teamCreateDto);
            return CreatedAtAction(nameof(GetTeamById), new { id = createdTeam.Id }, createdTeam);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTeam(int id, TeamCreateDto teamUpdateDto)
        {
            var success = await _teamService.UpdateTeamAsync(id, teamUpdateDto);
            if (!success) return NotFound(new { message = $"Équipe avec l'ID {id} introuvable." });
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTeam(int id)
        {
            var success = await _teamService.DeleteTeamAsync(id);
            if (!success) return NotFound(new { message = $"Équipe avec l'ID {id} introuvable." });
            return NoContent();
        }
    }
}
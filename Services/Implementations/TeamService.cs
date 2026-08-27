using API_REST.Data;
using API_REST.DTOs;
using API_REST.Models;
using API_REST.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace API_REST.Services.Implementations
{
    public class TeamService : ITeamService
    {
        private readonly AppDbContext _context;

        public TeamService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TeamReadDto>> GetAllTeamsAsync()
        {
            return await _context.Teams
                .Select(t => new TeamReadDto
                {
                    Id = t.Id,
                    Name = t.Name,
                    Description = t.Description
                }).ToListAsync();
        }

        public async Task<TeamReadDto?> GetTeamByIdAsync(int id)
        {
            var team = await _context.Teams.FindAsync(id);
            if (team == null) return null;

            return new TeamReadDto
            {
                Id = team.Id,
                Name = team.Name,
                Description = team.Description
            };
        }

        public async Task<TeamReadDto> CreateTeamAsync(TeamCreateDto teamCreateDto)
        {
            var team = new Team
            {
                Name = teamCreateDto.Name,
                Description = teamCreateDto.Description
            };

            _context.Teams.Add(team);
            await _context.SaveChangesAsync();

            return new TeamReadDto
            {
                Id = team.Id,
                Name = team.Name,
                Description = team.Description
            };
        }

        public async Task<bool> UpdateTeamAsync(int id, TeamCreateDto teamUpdateDto)
        {
            var team = await _context.Teams.FindAsync(id);
            if (team == null) return false;

            team.Name = teamUpdateDto.Name;
            team.Description = teamUpdateDto.Description;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteTeamAsync(int id)
        {
            var team = await _context.Teams.FindAsync(id);
            if (team == null) return false;

            _context.Teams.Remove(team);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
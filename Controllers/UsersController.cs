using API_REST.DTOs;
using API_REST.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API_REST.Controllers
{
    // L'URL sera : api/users
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        // On injecte le Service ici
        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        // GET: api/users (Récupérer tous les utilisateurs)
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserReadDto>>> GetUsers()
        {
            var users = await _userService.GetAllUsersAsync();
            return Ok(users);
        }

        // GET: api/users/5 (Récupérer un utilisateur précis)
        [HttpGet("{id}")]
        public async Task<ActionResult<UserReadDto>> GetUser(int id)
        {
            var user = await _userService.GetUserByIdAsync(id);
            if (user == null) return NotFound(); // Renvoie une erreur 404 si non trouvé

            return Ok(user);
        }

        // POST: api/users (Créer un nouvel utilisateur)
        [HttpPost]
        public async Task<ActionResult<UserReadDto>> CreateUser(UserCreateDto userCreateDto)
        {
            var createdUser = await _userService.CreateUserAsync(userCreateDto);

            // Renvoie un code 201 Created avec l'URL pour accéder au nouvel utilisateur
            return CreatedAtAction(nameof(GetUser), new { id = createdUser.Id }, createdUser);
        }

        // PUT: api/users/5 (Modifier un utilisateur)
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, UserCreateDto userCreateDto)
        {
            var result = await _userService.UpdateUserAsync(id, userCreateDto);
            if (!result) return NotFound();

            return NoContent(); // Code 204 : Succès, pas de contenu à renvoyer
        }

        // DELETE: api/users/5 (Supprimer un utilisateur)
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var result = await _userService.DeleteUserAsync(id);
            if (!result) return NotFound();

            return NoContent();
        }
    }
}
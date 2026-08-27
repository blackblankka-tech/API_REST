using API_REST.Data;
using API_REST.Models;
using API_REST.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace API_REST.Repositories.Implementations
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;

        // On injecte le DbContext ici pour pouvoir interagir avec PostgreSQL
        public UserRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<User?> GetByIdAsync(int id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task AddAsync(User user)
        {
            await _context.Users.AddAsync(user);
        }

        public void Update(User user)
        {
            // EF Core suit déjà l'entité, mais ça force le statut à "Modified"
            _context.Users.Update(user);
        }

        public void Delete(User user)
        {
            _context.Users.Remove(user);
        }

        public async Task<bool> SaveChangesAsync()
        {
            // Sauvegarde réellement les changements dans PostgreSQL
            return (await _context.SaveChangesAsync()) >= 0;
        }
    }
}
using API_REST.Models;
using Microsoft.EntityFrameworkCore;


namespace API_REST.Data
{
    public class AppDbContext : DbContext
    {
            public AppDbContext(DbContextOptions<AppDbContext> options)
                : base(options)
            {
            }

            public DbSet<User> Users { get; set; }

            public DbSet<Team> Teams { get; set; }

            public DbSet<Project> Projects { get; set; }

            public DbSet<TaskItem> TaskItems { get; set; }

            public DbSet<TaskDependency> TaskDependencies { get; set; }
     }
    
}

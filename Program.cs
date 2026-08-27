using API_REST.Data;
using API_REST.Repositories.Implementations;
using API_REST.Repositories.Interfaces;
using API_REST.Services.Implementations;
using API_REST.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// 1. Configuration de la base de données PostgreSQL
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// 2. Enregistrement des Repositories (Accès aux données)
builder.Services.AddScoped<IUserRepository, UserRepository>();

// 3. Enregistrement des Services (Logique métier)
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IProjectService, ProjectService>();
builder.Services.AddScoped<ITeamService, TeamService>();
builder.Services.AddScoped<ITaskService, TaskService>();
// 4. Configuration des Contrôleurs et de Swagger
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(); // Génère la documentation de l'API

var app = builder.Build();

// 5. Configuration du pipeline HTTP
if (app.Environment.IsDevelopment())
{
    // C'est ceci qui corrige ton erreur 404 !
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
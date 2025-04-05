using DotNetEnv;
using EvolveDb;
using Microsoft.EntityFrameworkCore;
using MySqlConnector;
using RestWithASPNETUdemy.Business;
using RestWithASPNETUdemy.Business.Implementations;
using RestWithASPNETUdemy.model.Context;
using RestWithASPNETUdemy.Repository.Generic;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

Env.Load();

var connectionString = $"Server={Environment.GetEnvironmentVariable("DB_HOST")};" +
                       $"Database={Environment.GetEnvironmentVariable("DB_NAME")};" +
                       $"Uid={Environment.GetEnvironmentVariable("DB_USER")};" +
                       $"Pwd={Environment.GetEnvironmentVariable("DB_PASS")};";

builder.Services.AddDbContext<MySQLContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

if (builder.Environment.IsDevelopment())
{
    MigrateDatabase(connectionString);
}

builder.Services.AddApiVersioning();

builder.Services.AddScoped<IPersonService, PersonServiceImplementation>();
builder.Services.AddScoped<IBooksService, BookServiceImplementation>();
builder.Services.AddScoped(typeof(IRepository<>), typeof(GenericRepository<>));

builder.Services.AddControllers();

builder.Services.AddScoped<IPersonService, PersonServiceImplementation>();

var app = builder.Build();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

void MigrateDatabase(string connectionString)
{
    try
    {
        var evolveConnection = new MySqlConnection(connectionString);
        var evolve = new Evolve(evolveConnection, Log.Information)
        {
            Locations = new List<string> { "db/migrations", "db/dataset" },
            IsEraseDisabled = true,
        };
        evolve.Migrate();
    }
    catch (Exception ex)
    {
        Log.Error("Database migrations failed", ex);
        throw;
    }
}
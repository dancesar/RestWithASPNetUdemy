using DotNetEnv;
using Microsoft.EntityFrameworkCore;
using RestWithASPNETUdemy.Business;
using RestWithASPNETUdemy.Business.Implementations;
using RestWithASPNETUdemy.model.Context;

var builder = WebApplication.CreateBuilder(args);

Env.Load();

var connectionString = $"Server={Environment.GetEnvironmentVariable("DB_HOST")};" +
                       $"Database={Environment.GetEnvironmentVariable("DB_NAME")};" +
                       $"Uid={Environment.GetEnvironmentVariable("DB_USER")};" +
                       $"Pwd={Environment.GetEnvironmentVariable("DB_PASS")};";

builder.Services.AddDbContext<MySQLContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

builder.Services.AddApiVersioning();

builder.Services.AddScoped<IPersonBusiness, PersonBusinessImplementation>();

builder.Services.AddControllers();

builder.Services.AddScoped<IPersonBusiness, PersonBusinessImplementation>();

var app = builder.Build();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
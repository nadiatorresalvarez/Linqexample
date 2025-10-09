using Lab8_NadiaTorres.Interfaces;
using Lab8_NadiaTorres.Models;
using Microsoft.EntityFrameworkCore;
using Lab8_NadiaTorres.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Registrar DbContext (antes de Build)
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<dbContextnLab8>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

// Registrar UnitOfWork en DI (usa los repositorios internos)
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
        c.RoutePrefix = string.Empty; // Swagger en la raiz
    });
}

app.UseHttpsRedirection();
app.UseCors();

app.MapControllers();

app.Run();

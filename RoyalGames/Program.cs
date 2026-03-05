using Microsoft.EntityFrameworkCore;
using RoyalGames.Applications.Services;
using RoyalGames.Contexts;
using RoyalGames.Interfaces;
using RoyalGames.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Chamar nossa conexao com o banco aqui na program
builder.Services.AddDbContext<Royal_GamesContext>(options => options.UseSqlServer
    (builder.Configuration.GetConnectionString("Default")));

builder.Services.AddScoped<IClassificacaoIndicativaRepository, ClassificacaoIndicativaRepository>();
builder.Services.AddScoped<ClassificacaoIndicativaService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

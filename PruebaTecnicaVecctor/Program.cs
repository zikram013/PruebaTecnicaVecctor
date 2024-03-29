using PruebaTecnicaVecctor.Servicio;
using PruebaTecnicaVecctor.Servicio.Interfaz;
using PruebaTecnicaVecctor.ContextoBBDD;
using Microsoft.EntityFrameworkCore;
using PruebaTecnicaVecctor.Repository.Interfaz;
using PruebaTecnicaVecctor.Entidad;
using PruebaTecnicaVecctor.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<ApplicationContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("ConexionSql")));

builder.Services.AddScoped<INasaService, NasaService>();
builder.Services.AddScoped<INasaApiService, NasaApiService>();
builder.Services.AddScoped<INasaAsteroidsRepository, NasaAsteroidsRepository>();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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

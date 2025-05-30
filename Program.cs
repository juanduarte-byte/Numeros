using Microsoft.EntityFrameworkCore;
using ParImparAPI.Domain.Data;
using ParImparAPI.Domain.Entities;

var builder = WebApplication.CreateBuilder(args);

// Agregar servicios
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(); // Swagger para documentar la API

// Configuración de CORS (si es necesario)
builder.Services.AddCors(options =>
{
    options.AddPolicy(
        "AllowAll",
        policy =>
        {
            policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
        }
    );
});

// Configuración de EF Core con SQLite
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlite("Data Source=Numeros.db"));

// Crear la aplicación
var app = builder.Build();

// Usar Swagger en el entorno de desarrollo
if (app.Environment.IsDevelopment())
{
    app.UseSwagger(); // Habilitar Swagger
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "ParImparAPI v1");
        c.RoutePrefix = "swagger"; // Esto asegura que Swagger se abra en /swagger
    });
}

// Configurar middleware
app.UseHttpsRedirection(); // Redirigir a HTTPS
app.UseAuthorization(); // Autorización (para manejar seguridad si la implementas)

// Usar CORS (si es necesario)
app.UseCors("AllowAll");

// Mapear controladores
app.MapControllers();

// Ejecutar la aplicación
app.Run();

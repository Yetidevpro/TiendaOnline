using Microsoft.EntityFrameworkCore;
using TiendaOnline.Application.Services;
using TiendaOnline.Infrastructure.Data;

var builder = WebApplication.CreateBuilder(args);

// Configuración de CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend",
        policy =>
        {
            policy.WithOrigins("http://127.0.0.1:5500") // URL de tu frontend
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});

// Agregar servicios a la colección.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configuración de Entity Framework Core con SQL Server
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("TiendaOnlineDB")));

// Inyección de dependencias
builder.Services.AddScoped<IProductoService, ProductoService>();
builder.Services.AddScoped<IColorService, ColorService>();
builder.Services.AddScoped<ITallaService, TallaService>();

var app = builder.Build();

// Configuración del pipeline de solicitudes HTTP.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Aplicar CORS antes de la autorización y redirección HTTPS
app.UseCors("AllowFrontend");

// Si necesitas evitar redirección HTTPS en desarrollo (opcional)
if (!app.Environment.IsDevelopment())
{
    app.UseHttpsRedirection();
}

app.UseAuthorization();
app.MapControllers();

app.Run();
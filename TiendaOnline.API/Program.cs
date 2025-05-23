using Microsoft.EntityFrameworkCore;
using TiendaOnline.Application.Services;
using TiendaOnline.Infrastructure.Data;

var builder = WebApplication.CreateBuilder(args);

/*// Configuración de CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend",
        policy =>
        {
            policy.WithOrigins("http://127.0.0.1:5500") // URL de tu frontend usado para pruebas locales
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});*/

// Agregar servicios a la colección.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configuración de Entity Framework Core para diferenciar entre entornos de produción y pruebas.
var connectionString = builder.Configuration.GetConnectionString("TiendaOnlineDB");

if (builder.Environment.IsDevelopment() || builder.Environment.IsStaging())
{
    // En produción 
    builder.Services.AddDbContext<ApplicationDbContext>(options =>
        options.UseSqlServer(connectionString));
}
else
{
    // Para desarrollo 
    builder.Services.AddDbContext<ApplicationDbContext>(options =>
        options.UseInMemoryDatabase("TestDatabase"));
}

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

if (!app.Environment.IsDevelopment())
{
    app.UseHttpsRedirection();
}

app.UseAuthorization();
app.MapControllers();

app.Run();
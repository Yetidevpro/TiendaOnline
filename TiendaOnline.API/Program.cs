using Microsoft.EntityFrameworkCore;
using TiendaOnline.Application.Services;
using TiendaOnline.Infrastructure.Data;

var builder = WebApplication.CreateBuilder(args);

// Configuraci�n de CORS
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

// Agregar servicios a la colecci�n.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configuraci�n de Entity Framework Core (diferenciar entre producci�n y pruebas)
var connectionString = builder.Configuration.GetConnectionString("TiendaOnlineDB");

if (builder.Environment.IsDevelopment() || builder.Environment.IsStaging())
{
    // En desarrollo o staging usar SQL Server
    builder.Services.AddDbContext<ApplicationDbContext>(options =>
        options.UseSqlServer(connectionString));
}
else
{
    // En pruebas usar base de datos en memoria
    builder.Services.AddDbContext<ApplicationDbContext>(options =>
        options.UseInMemoryDatabase("TestDatabase"));
}

// Inyecci�n de dependencias
builder.Services.AddScoped<IProductoService, ProductoService>();
builder.Services.AddScoped<IColorService, ColorService>();
builder.Services.AddScoped<ITallaService, TallaService>();

var app = builder.Build();

// Configuraci�n del pipeline de solicitudes HTTP.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Aplicar CORS antes de la autorizaci�n y redirecci�n HTTPS
app.UseCors("AllowFrontend");

// Si necesitas evitar redirecci�n HTTPS en desarrollo (opcional)
if (!app.Environment.IsDevelopment())
{
    app.UseHttpsRedirection();
}

app.UseAuthorization();
app.MapControllers();

app.Run();
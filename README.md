# 🧵 Tienda Online API

Esta es una API REST desarrollada con **.NET 8** que gestiona una tienda de ropa online. Permite crear, actualizar, listar y eliminar productos, junto con sus colores y tallas asociadas.

## 📋 Enunciado de la prueba técnica

### Tienda de ropa online

Se quiere crear una página web con las siguientes necesidades:
- Crear productos.
- Editar productos.
- Eliminar productos.
- Listar productos.
		
**Producto:**
- Talla
- Color
- Precio
- Descripción

### Realizar una Api Rest para cubrir las necesidades de la página

**Funcionalidades:**
- La api debe realizar las operaciones requeridas por el punto 1 

**Requerimientos:**
- Api Rest
- .Net Core >= 2.0 o .Net Framework >= 4.6.1
- Sql server 2012 o superior	
- Aplicar buenas prácticas (Separar en capas, inyección de dependencias, Solid, Validaciones, etc.)
- Realizar Test Unitarios

*Todo lo adicional que se te ocurra será bienvenido :)*

## 📋 Índice

- [Características principales](#-características-principales)
- [Tecnologías utilizadas](#-tecnologías-utilizadas)
- [Estructura del proyecto](#-estructura-del-proyecto)
- [Modelo de datos y Entity Framework Core](#-modelo-de-datos-y-entity-framework-core)
- [Instalación y ejecución](#-cómo-ejecutar-la-api)
- [Funcionalidades de la API](#-funcionalidades-de-la-api)
- [Testing](#-testing)
- [Documentación y CORS](#-swagger--cors)
- [Seguridad](#-seguridad-en-configuración)
- [Actualizaciones futuras](#-actualizaciones-futuras)
- [Autor](#-autor)

## 📦 Características principales

- CRUD de productos, tallas y colores.
- API RESTful siguiendo buenas prácticas.
- Inyección de dependencias y separación por capas (Domain, Application, Infrastructure, Api).
- Testing unitario con **xUnit** y base de datos en memoria.
- Documentación interactiva con **Swagger**.
- Soporte para **CORS** para pruebas básicas desde otras aplicaciones frontend.

## 📚 Tecnologías utilizadas

- .NET 8
- Entity Framework Core 9 (InMemory y SQL Server)
- xUnit
- Swagger / Swashbuckle
- Moq
- CORS habilitado para pruebas
- Buenas prácticas: SOLID, DTOs, pruebas unitarias, separación por capas, inyección de dependencias

## 🧱 Estructura del proyecto

- **TiendaOnline.Domain**: Entidades y contratos.
- **TiendaOnline.Application**: DTOs, servicios e interfaces.
- **TiendaOnline.Infrastructure**: Implementación de EF Core, repositorios y configuración de base de datos.
- **TiendaOnline.Api**: Capa de presentación (controladores REST), configuración Swagger, CORS e inyección de dependencias.
- **TiendaOnline.Testing**: Pruebas unitarias usando EF Core InMemory y xUnit.

## 🗄️ Modelo de datos y Entity Framework Core

La API utiliza Entity Framework Core como ORM (Object-Relational Mapper) para interactuar con la base de datos. A continuación se detalla la estructura de datos y las relaciones entre entidades:

### Diagrama de relaciones
![image](https://github.com/user-attachments/assets/2a340208-361d-4100-bc83-64df1652ab22)

### Características de la implementación

- **Relaciones muchos a muchos**: Un producto puede tener múltiples colores y tallas, y viceversa.
- **Tablas intermedias**: ProductoColor y ProductoTalla para manejar las relaciones.
- **Configuración Fluent API**: Uso de OnModelCreating para definir relaciones complejas.
- **Claves compuestas**: En las tablas intermedias para evitar duplicados.
- **Eliminación en cascada**: Al eliminar un producto, se eliminan sus relaciones con colores y tallas.
- **Precisión decimal**: Configuración específica para el campo Precio (18,2).


## 🚀 ¿Cómo ejecutar la API?

### 1. Clonar el repositorio

```bash
git clone https://github.com/tuusuario/TiendaOnline.git
cd tienda-online-api
```

### 2. Configurar cadena de conexión

La cadena de conexión está ubicada en el archivo TiendaOnline.Api/appsettings.json:

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=localhost;Database=TiendaOnlineDb;Trusted_Connection=True;TrustServerCertificate=True"
}
```

⚠️ **Nota**: Esta configuración es solo para esta prueba. En proyectos reales la ConecctionString iria en GitHub Secrets o Azure Key Vault. Ya que appsetting.json siempre esta en el ropositorio y cualquier persona que tuviera acceso al repositorio podria ver la conexión  con la base de datos.

### 3. Ejecutar el proyecto

```bash
dotnet run --project TiendaOnline.Api
```

La API estará disponible por defecto en https://localhost:<puerto>.

### 4. Acceder a Swagger

```
https://localhost:<puerto>/swagger
```

## ✅ Funcionalidades de la API

### Endpoints principales

- **GET /api/productos** → Listar productos.
- **POST /api/productos** → Crear un producto.
- **PUT /api/productos/{id}** → Actualizar un producto.
- **DELETE /api/productos/{id}** → Eliminar un producto.

También se exponen endpoints similares para tallas (/api/tallas) y colores (/api/colores).

Puedes probar todos los endpoints directamente desde Swagger o usando herramientas como Postman.

## 🧪 Testing

Se incluyen pruebas unitarias para los servicios de negocio (ProductoService, ColorService, TallaService) utilizando:

- xUnit como framework de testing.
- EF Core InMemory para simular el contexto de base de datos.
- Inyección de dependencias en los tests para asegurar pruebas aisladas y realistas.

### Ejecutar los tests
```
bash o desde powershell
dotnet test
```
Cada prueba realiza operaciones de creación, obtención, actualización y eliminación, validando el correcto funcionamiento de los servicios.

## 🧩 Swagger & CORS

- **Swagger**: Integrado desde Program.cs, permitiendo pruebas interactivas desde el navegador.
- **CORS**: Configurado para permitir solicitudes desde cualquier origen, usado para realizar una prueva pequeña con un frontend local

```csharp
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
    });
});
```
## 📚 Tecnologías utilizadas

- .NET 8
- Entity Framework Core 9 (InMemory y SQL Server)
- xUnit
- Swagger / Swashbuckle
- Moq
- CORS habilitado para pruebas
- Buenas prácticas: SOLID, DTOs, pruebas unitarias, separación por capas, inyección de dependencias


## 📝 Actualizaciones futuras

Las siguientes mejoras están planeadas para próximas versiones:

- **Autenticación y autorización**:
  - Implementación de JWT para acceso seguro a la API
  - Roles de usuario (Admin, Cliente, Vendedor)

- **Monitoreo y diagnóstico**:
  - Logging estructurado

- **Infraestructura**:
  - Implementación de CI/CD con GitHub Actions o Azure

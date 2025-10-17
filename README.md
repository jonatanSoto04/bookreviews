# 📚 BookReviews - Aplicación de Reseñas de Libros

Una aplicación web completa para descubrir, explorar y reseñar libros. Desarrollada con .NET 8 en el backend y Angular 17 en el frontend.

## 🚀 Características

### Backend (.NET 8)
- ✅ API RESTful con ASP.NET Core
- ✅ Autenticación JWT
- ✅ Entity Framework Core con SQL Server
- ✅ Patrón Repository
- ✅ DTOs y AutoMapper
- ✅ Validaciones de modelos
- ✅ Swagger/OpenAPI documentation
- ✅ CORS configurado

### Frontend (Angular 17)
- ✅ Interfaz responsive y moderna
- ✅ Autenticación y autorización
- ✅ Búsqueda y filtrado de libros
- ✅ Sistema de reseñas con calificación
- ✅ Gestión de estado con servicios
- ✅ Interceptores HTTP
- ✅ Guards de rutas

## 🛠️ Tecnologías Utilizadas

### Backend
- .NET 8
- ASP.NET Core Web API
- Entity Framework Core
- SQL Server
- JWT Authentication
- Swagger/OpenAPI
- AutoMapper

### Frontend
- Angular 17
- TypeScript
- RxJS
- Font Awesome
- CSS3 con Grid/Flexbox

## 📋 Prerrequisitos

- .NET 8 SDK
- Node.js 18+ y npm
- MySQL Server 8.0+
- Angular CLI 17+

## 🚀 Ejecución Local

### 1. Clonar el Repositorio
```bash
git clone <url-del-repositorio>
cd book-reviews-app

Migraciones y Base de Datos

    cd BookReviewsAPI

    # Restaurar paquetes NuGet
    dotnet restore

    # Ejecutar migraciones
    dotnet ef migrations add InitialCreate
    dotnet ef database update

    # Ejecutar la aplicación
    dotnet run



Configuración del Frontend

cd BookReviewsAPI

# Restaurar paquetes NuGet
dotnet restore

# Ejecutar migraciones
dotnet ef migrations add InitialCreate
dotnet ef database update

# Ejecutar la aplicación
dotnet run

#### Base de Datos MySQL
1. Asegúrate de tener MySQL Server 8.0+ ejecutándose
2. Crea una base de datos llamada `BookReviewsDB`
3. Actualiza la cadena de conexión en `appsettings.json`:
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=BookReviewsDB;Uid=root;Pwd=tu_password;"
  }
}

🔄 Comandos Actualizados para MySQL

# Instalar el paquete de MySQL
dotnet add package Pomelo.EntityFrameworkCore.MySql

# Restaurar paquetes
dotnet restore

# Crear migración
dotnet ef migrations add InitialCreateMySql

# Aplicar migración
dotnet ef database update

# Ejecutar
dotnet run
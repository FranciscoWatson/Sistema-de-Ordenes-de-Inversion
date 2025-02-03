# Sistema-de-Ordenes-de-Inversion
# Sistema de Órdenes de Inversión (SOI)

Este proyecto es una **API REST** para gestionar órdenes de inversión. Implementa arquitectura **CQRS** con **MediatR**, **FluentValidation** para validaciones, **JWT** para autenticación y **Entity Framework Core** para persistencia.

---

## Tabla de Contenido

1. [Arquitectura](#arquitectura)  
2. [Proyectos y Estructura](#proyectos-y-estructura)  
3. [Tecnologías Utilizadas](#tecnologías-utilizadas)  
4. [Instalación y Ejecución](#instalación-y-ejecución)  
   - [Requisitos Previos](#requisitos-previos)  
   - [Ejecución con Docker](#ejecución-con-docker)  
   - [Uso sin Docker](#uso-sin-docker)  
5. [Configuraciones Principales](#configuraciones-principales)  
   - [Variables de Entorno](#variables-de-entorno)  
   - [JWT](#jwt)  
   - [Entity Framework](#entity-framework)  
6. [Endpoints Principales](#endpoints-principales)  
   - [Autenticación](#autenticación)  
   - [Ordenes](#ordenes)  
7. [CQRS y MediatR](#cqrs-y-mediatr)  
8. [Validaciones con FluentValidation](#validaciones-con-fluentvalidation)  
9. [Licencia](#licencia)

---

## Arquitectura

El sistema está dividido en **cuatro capas** principales, siguiendo una arquitectura limpia y organizada:

1. **SOI.API**: Capa de presentación (controllers con endpoints).  
2. **SOI.Application**: Contiene la lógica de aplicación (Commands, Queries, Handlers, DTOs, Validaciones, etc.). Implementa **CQRS** con **MediatR**.  
3. **SOI.Domain**: Entidades de dominio (Entities, Servicios de dominio, Interfaces de negocio).  
4. **SOI.Infrastructure**: Implementaciones concretas de persistencia (Entity Framework Core), autenticación, repositorios y configuraciones de base de datos.

Además, se usa **MediatR** para desacoplar la lógica de dominio y la de infraestructura.

---

## Proyectos y Estructura

El repositorio contiene estos proyectos:

- **SOI.API**  
  - `Program.cs` y `appsettings.json`: configuración de la aplicación.  
  - Controllers (por ejemplo, `AuthenticationController`, `OrdenController`) que exponen los endpoints REST.  
  - `dockerfile`: descripción para crear la imagen Docker.

- **SOI.Application**  
  - `Commands`, `Queries`: objetos que representan operaciones (CrearOrden, ObtenerOrdenes, etc.).  
  - `Handlers`: clases que gestionan la lógica de cada Command/Query usando **MediatR**.  
  - `DTOs`: objetos de transferencia de datos para requests/responses.  
  - `Validators`: validaciones **FluentValidation** para cada operación.  
  - `ApplicationConfiguration.cs`: inicializa servicios de aplicación.

- **SOI.Domain**  
  - `Entities`: clases que representan el modelo de dominio (Cuenta, Orden, Activo, etc.).  
  - `Authentication`: interfaces y servicios de autenticación.  
  - `Services`: lógica de dominio (por ejemplo, cálculos de comisiones, etc.).  

- **SOI.Infrastructure**  
  - `Persistence`: incluye el `SoiDbContext` (clase de EF Core), configuraciones (clases `*Configuration.cs`) y `Migrations`.  
  - `Repositories`: implementaciones concretas de repositorios para acceder a la base de datos.  
  - `InfrastructureLayerConfiguration.cs`: inicializa servicios de infraestructura.

---

## Tecnologías Utilizadas

- **.NET 8** (ASP.NET Core)  
- **Entity Framework Core**  
- **SQL Server** (ejemplo, remoto o local)  
- **Docker**  
- **MediatR**  
- **FluentValidation**  
- **AutoMapper**  
- **JWT** (JSON Web Token) para autenticación  
- **Swagger** para documentación de endpoints

---

## Instalación y Ejecución

### Requisitos Previos

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet)  
- [Docker](https://docs.docker.com/get-docker/) instalado y corriendo, si deseas usar contenedores.  
- SQL Server local o externo, configurado para permitir conexiones remotas (opcional si quieres hacer pruebas con BD real).

### Ejecución con Docker

1. Clona este repositorio.
2. Edita el archivo `docker-compose.yml` si necesitas cambiar las variables de entorno:
   ```yaml
   environment:
     ConnectionStrings__SqlServerConnection: "Server=host.docker.internal,1433;Database=SoiDB;User Id=sa;Password=sa;TrustServerCertificate=True;"
     JwtAuthentication__SecretKey: "47DEQpj8HBSa+/TImW+5JCeuQeRkm5NMpJWZG3hSuFU="
     JwtAuthentication__Issuer: "http://localhost:5297"
     JwtAuthentication__Audience: "soiapi"
     JwtAuthentication__TokenExpiryInMinutes: "60"
3. Ejecutar:
docker-compose up --build en la terminal para ejecutar en docker.

---

## Configuraciones Principales

### Variables de Entorno

La API está configurada para recibir variables de entorno que pueden personalizar la conexión a la base de datos, la autenticación JWT y otros parámetros. Estas variables pueden definirse en el archivo `docker-compose.yml` o establecerse manualmente en el entorno.

#### Configuración en Docker Compose:
```yaml
environment:
  ConnectionStrings__SqlServerConnection: "Server=host.docker.internal,1433;Database=SoiDB;User Id=sa;Password=sa;TrustServerCertificate=True;"
  ASPNETCORE_ENVIRONMENT: "Development"
  JwtAuthentication__SecretKey: "47DEQpj8HBSa+/TImW+5JCeuQeRkm5NMpJWZG3hSuFU="
  JwtAuthentication__Issuer: "http://localhost:5297"
  JwtAuthentication__Audience: "soiapi"
  JwtAuthentication__TokenExpiryInMinutes: "60"

```

### Variables de Entorno

La API está configurada para recibir variables de entorno que pueden personalizar la conexión a la base de datos, la autenticación JWT y otros parámetros. Estas variables pueden definirse en el archivo `docker-compose.yml` o establecerse manualmente en el entorno.

#### Configuración en Docker Compose:
```yaml
environment:
  ConnectionStrings__SqlServerConnection: "Server=host.docker.internal,1433;Database=SoiDB;User Id=sa;Password=sa;TrustServerCertificate=True;"
  ASPNETCORE_ENVIRONMENT: "Development"
  JwtAuthentication__SecretKey: "47DEQpj8HBSa+/TImW+5JCeuQeRkm5NMpJWZG3hSuFU="
  JwtAuthentication__Issuer: "http://localhost:5297"
  JwtAuthentication__Audience: "soiapi"
  JwtAuthentication__TokenExpiryInMinutes: "60"
```

### JWT (Autenticación por Tokens)

La API usa JWT (JSON Web Token) para la autenticación de usuarios. Cuando un usuario inicia sesión, recibe un token JWT que debe enviarse en las peticiones protegidas en el header Authorization.

Ejemplo de Token JWT:

```json
{
  "Authorization": "Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9..."
}
```

Para generar los tokens, se usa la clave secreta definida en `JwtAuthentication__SecretKey`.

### Entity Framework

Se usa Entity Framework Core como ORM para manejar la base de datos SQL Server.

#### Migraciones:

Para aplicar migraciones y generar la base de datos, ejecuta:

```sh
dotnet ef database update
```

Si necesitas crear una nueva migración:

```sh
dotnet ef migrations add NombreMigracion
```

Las migraciones se encuentran en `SOI.Infrastructure/Persistence/Migrations`.

## Endpoints Principales

### Autenticación

**POST /authenticate**

**Descripción:** Inicia sesión y devuelve un token JWT.

#### Request Body:
```json
{
  "nombre": "usuario",
  "password": "secreto"
}
```

#### Response:
```json
{
  "token": "eyJhbGciOiJIUzI1NiIs..."
}
```

### Órdenes de Inversión

> **Nota:** Todos estos endpoints requieren autenticación con un token JWT.

**POST /api/ordenes**

**Descripción:** Crea una nueva orden.

#### Request Body:
```json
{
  "cuentaId": 1,
  "activoId": 10,
  "cantidad": 100,
  "precio": 250.5,
  "operacion": "C"
}
```

#### Response:
```json
{
  "ordenId": 5,
  "cuentaId": 1,
  "activoId": 10,
  "cantidad": 100,
  "precio": 250.5,
  "montoTotal": 25050
}
```

**GET /api/ordenes**

**Descripción:** Lista todas las órdenes de la cuenta autenticada.

**GET /api/ordenes/{id}**

**Descripción:** Obtiene los detalles de una orden específica.

**PUT /api/ordenes/{id}**

**Descripción:** Actualiza una orden.

#### Request Body:
```json
{
  "cantidad": 200,
  "precio": 260.0,
  "operacion": "V"
}
```

**DELETE /api/ordenes/{id}**

**Descripción:** Elimina una orden.

## CQRS y MediatR

El proyecto implementa el patrón CQRS (Command Query Responsibility Segregation) con MediatR:

- **Commands:** Representan operaciones que modifican el estado del sistema (`CrearOrdenCommand`, `ActualizarOrdenCommand`).
- **Queries:** Representan operaciones que leen datos sin modificar (`ObtenerOrdenesQuery`).

Cada **Command** o **Query** tiene un **Handler** que contiene la lógica específica.

## Validaciones con FluentValidation

Todas las validaciones de datos en la API se manejan con FluentValidation.

Ejemplo de validación en `CrearOrdenCommandValidator`:

```csharp
public class CrearOrdenCommandValidator : AbstractValidator<CrearOrdenCommand>
{
    public CrearOrdenCommandValidator()
    {
        RuleFor(x => x.Cantidad).GreaterThan(0);
        RuleFor(x => x.Precio).GreaterThan(0);
    }
}
```

Si una solicitud no cumple las reglas, se devuelve un error **400 Bad Request**.


## Licencia

Este proyecto está bajo una licencia de código abierto. Puedes usarlo y modificarlo libremente.

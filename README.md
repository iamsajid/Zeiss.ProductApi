# Demo Product API

A modern ASP.NET Core REST API for product management, built with vertical slice architecture and best practices for scalability, maintainability, and testability.

## Features
- **Vertical Slice Architecture**: Each feature (CRUD, authentication, etc.) is implemented as a self-contained slice.
- **CQRS with MediatR**: Command and query separation using MediatR for request handling and pipeline behaviors.
- **Entity Framework Core**: PostgreSQL database integration for persistent storage.
- **Distributed Unique ID Generation**: Uses Redis for generating unique product IDs across instances.
- **Caching**: In-memory and Redis caching via decorator pattern for fast product queries.
- **JWT Authentication & Role-Based Authorization**: Secure endpoints with user and admin roles.
- **Swagger UI**: Interactive API documentation with JWT support.
- **Docker Support**: Run the app as a container for easy deployment.
- **Unit Testing**: Comprehensive tests for handlers, validators, and controllers using xUnit, Moq, and FluentAssertions.
- **Global Error Handling**: Custom middleware for consistent error responses.
- **Logging**: Action filter and cache decorator logging for observability.
- **Pagination & Filtering**: Efficient product listing with category filtering and pagination.

## Design Patterns & Practices
- **Decorator Pattern**: Used for caching repository queries.
- **Pipeline Behaviors**: MediatR validation and logging behaviors.
- **Repository Pattern**: Abstracts data access for products.
- **Middleware**: Centralized error handling and logging.
- **Dependency Injection**: All services registered via DI container.
- **Unit of Work**: (Implicit via EF Core DbContext)

## Getting Started
### Prerequisites
- .NET 8 SDK
- Docker (for containerized run)
- PostgreSQL & Redis (local or via Docker Compose)

### Start PostgreSQL & Redis with Docker Compose
1. From the project root, run:
   ```sh
   docker-compose up -d
   ```
2. This will start PostgreSQL and Redis containers as defined in `docker-compose.yml`.
3. Update your `appsettings.json` connection strings if needed to match the Docker Compose setup.

### Local Run
1. Build the project: `dotnet build`
2. Update connection strings in `appsettings.json`. (Optional)
3. Run the app: `dotnet run --project src/ProductApi/ProductApi.csproj`
4. Access Swagger UI: `http://localhost:5000/swagger`

### Docker Run
1. Build the image:
   ```sh
   cd src/ProductApi
   docker build -t productapi .
   ```
2. Run the container:
   ```sh
   docker run -p 8080:8080 productapi
   ```
3. Access Swagger UI: `http://localhost:8080/swagger`

### Authentication
- Use `/api/auth/login` to obtain a JWT token.
- Use the token in the `Authorization: Bearer <token>` header for protected endpoints.

### Testing
- Run unit tests:
  ```sh
  dotnet test tests/ProductApi.UnitTests/ProductApi.UnitTests.csproj
  ```

### API Testing with Postman
- Import the provided Postman collection (`ProductApi.postman_collection.json`) to test endpoints interactively.

## Folder Structure
- `src/ProductApi/Features/` - Vertical slices for each feature (CRUD, Auth, etc.)
- `src/ProductApi/Common/` - Shared helpers, middleware, filters, enums
- `src/ProductApi/Infrastructure/` - Data access, migrations
- `tests/ProductApi.UnitTests/` - Unit tests for handlers, validators, controllers

**Tech Stack:** ASP.NET Core, MediatR, EF Core, PostgreSQL, Redis, Docker, xUnit, Moq, FluentAssertions
---

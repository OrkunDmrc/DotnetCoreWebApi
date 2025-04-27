# DotnetCoreWebApi

A simple .NET 6 Web API project demonstrating CRUD operations, validation, authentication, and unit testing.

## Features

1. CRUD operations for Books, Authors, and Genres with relational data.
2. Fluent Validation for input validation.
3. Simple and easy-to-understand architecture.
4. Operates without a database using in-memory storage.
5. Custom error logging with middleware.
6. Unit tests for CRUD operations.
7. User authentication and token management.

## Technologies Used

- **ASP.NET Core (.NET 6)**: Framework for building the Web API.
- **AutoMapper**: For object-to-object mapping.
- **FluentValidation**: For validating input models.
- **EntityFrameworkCore**: ORM for data access.
- **EntityFrameworkCore.InMemory**: In-memory database for testing and development.
- **Swashbuckle.AspNetCore**: For generating Swagger documentation.
- **Moq**: For mocking dependencies in unit tests.
- **Fluent Assertions**: For expressive unit test assertions.
- **JwtBearer Authentication**: For secure user authentication.

## Getting Started

1. Clone the repository:
   ```bash
   git clone https://github.com/orkundmrc/DotnetCoreWebApi.git
   cd DotnetCoreWebApi

2. Restore dependencies
   ```bash
   dotnet restore

3. Run the application
   ```bash
   dotnet run --project WebApi/WebApi.csproj

4. Access the Swagger UI at

   https://localhost:7285/swagger

5. Running test

   ```bash
   dotnet test

## Project Structure

- **WebAPi**: Contains the main application code.
    - **Controllers/**: API endpoints for Books, Authors, Genres, and Users.
    - **Application/**: Business logic for CRUD operations.
    - **DbOperations/**: Database context and data initialization.
    - **Middlewares/**: Custom middleware for error handling.
    - **Services/**: Logging services.
    - **TokenOperations/**: JWT token generation and validation.

- **Tests/WebApi.UnitTests**: Unit tests for the application.



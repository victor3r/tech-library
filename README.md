# Library API

A simple library management API built with .NET 9, implementing book checkout functionality and user authentication.

## Technologies

- .NET 9
- Entity Framework Core
- JWT Authentication
- BCrypt for password hashing
- FluentValidation
- Swagger/OpenAPI

## Project Structure

The solution is divided into three projects:
- `library.api`: Main API project containing controllers and business logic
- `library.communication`: Data transfer objects (DTOs)
- `library.exception`: Custom exception handling

## Features

### User Management
- User registration with encrypted password
- JWT token authentication

### Book Management
- Book checkout system
- Validation for book availability
- Maximum loan period of 7 days

## API Endpoints

### Users
- `POST /Users` - Register new user
  - Response: 201 Created
  - Returns: User name and access token

### Checkouts
- `POST /Checkouts/{bookId}` - Checkout a book (requires authentication)
  - Response: 204 No Content
  - Validates book availability

## How to Run

1. Clone the repository:
```bash
git clone https://github.com/victor3r/tech-library.git
```

2. Navigate to the project directory:
```bash
cd tech-library
```

3. Build and run the project:
```bash
dotnet run --project library.api
```

4. Access the Swagger documentation:
```
http://localhost:5071/swagger
```

## Architecture

The project follows a Clean Architecture approach with the following components:

- **Controllers**: Handle HTTP requests and responses
- **Use Cases**: Contain business logic for specific operations
- **Domain**: Core business entities
- **Infrastructure**: Database access, security, and external services

The application uses Entity Framework Core for data persistence and JWT tokens for authentication. The business logic is organized into use cases, promoting separation of concerns and maintainability.
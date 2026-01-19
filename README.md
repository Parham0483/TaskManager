# TaskManager - ASP.NET Core Learning Project

A task management REST API built with ASP.NET Core 9.0 and PostgreSQL, created as a learning project to explore .NET Core fundamentals, Entity Framework Core, and modern API development practices.

## 🎯 Project Overview

This project was developed as a hands-on learning exercise while studying C# and ASP.NET Core. It implements a basic task management system with user assignment capabilities, demonstrating core concepts of web API development, database integration, and service-oriented architecture.

## 🛠️ Technologies & Tools

- **Framework**: ASP.NET Core 9.0
- **Database**: PostgreSQL
- **ORM**: Entity Framework Core 9.0.5
- **Database Provider**: Npgsql.EntityFrameworkCore.PostgreSQL 9.0.4
- **API Documentation**: Swagger/OpenAPI (Swashbuckle.AspNetCore 8.1.4)
- **Mapping**: AutoMapper 12.0.1
- **Runtime**: .NET 9.0

## 📁 Project Structure
```
TaskManager/
├── Data/
│   └── AppDbContext.cs          # Database context configuration
├── Models/
│   ├── Tasks.cs                 # Task entity model
│   └── User.cs                  # User entity model
├── Dtos/
│   ├── TasksDtos/
│   │   ├── TasksCreateDto.cs    # DTO for creating tasks
│   │   ├── TasksReadDto.cs      # DTO for reading tasks
│   │   └── TasksUpdateDto.cs    # DTO for updating tasks
│   └── UsersDtos/
│       ├── UsersCreateDto.cs    # DTO for creating users
│       ├── UsersReadDto.cs      # DTO for reading users
│       └── UsersUpdateDto.cs    # DTO for updating users
├── Services/
│   ├── Interfaces/
│   │   ├── ITaskService.cs      # Task service interface
│   │   └── IUserService.cs      # User service interface
│   └── TaskService.cs           # Task service implementation (pending)
└── Program.cs                   # Application entry point
```

## 🔑 Key Features

### Task Management
- Create, read, update, and delete tasks
- Task status tracking (Todo, Doing, Done)
- Assignee management
- Deadline tracking
- Automatic timestamp generation

### User Management
- User CRUD operations
- Task assignment to users
- Track assigned tasks count per user
- Phone number and password management

### API Features
- RESTful API design
- Data Transfer Objects (DTOs) for clean separation
- Service layer architecture
- Swagger UI for API documentation
- PostgreSQL integration with Entity Framework Core

## 📊 Database Schema

### Tasks Table
- `Id` - Primary key
- `Status` - Enum (Todo, Doing, Done)
- `Asignee` - Task assignee name
- `CreatedAt` - Automatic timestamp
- `HandedIn` - Deadline timestamp

### Users Table
- `Id` - Primary key
- `Name` - User's name
- `PhoneNo` - Contact number
- `Password` - User password
- `AssignedTasks` - Collection of assigned tasks

## 🚀 Getting Started

### Prerequisites
- .NET 9.0 SDK
- PostgreSQL database
- Docker (optional, for PostgreSQL container)

### Database Configuration

Update the connection string in `appsettings.json`:
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Port=5432;Database=taskmanagerdb;Username=yourusername;Password=yourpassword;"
  }
}
```

> **Note**: The current configuration uses `ComandeerConnection` instead of `DefaultConnection`. Update `Program.cs` to use the correct connection string name.

### Running the Application

1. **Clone the repository**
```bash
   git clone <repository-url>
   cd TaskManager
```

2. **Restore dependencies**
```bash
   dotnet restore
```

3. **Update database connection**
   - Modify `appsettings.json` with your PostgreSQL credentials
   - Ensure the connection string name matches in `Program.cs`

4. **Run migrations** (when implemented)
```bash
   dotnet ef database update
```

5. **Run the application**
```bash
   dotnet run
```

6. **Access Swagger UI**
   - Navigate to `https://localhost:7282/swagger` or `http://localhost:5207/swagger`

## 📝 Service Layer Implementation

The project includes comprehensive service interfaces with methods for:

### Task Service
- `GetAllTasks()` - Retrieve all tasks
- `GetTaskById(int id)` - Get specific task
- `GetTasksByUserId(int userId)` - Filter by user
- `GetTasksByStatus(TaskStatus status)` - Filter by status
- `GetTasksByUserIdAndStatus()` - Combined filtering
- `GetTasksByDateCreated()` - Filter by creation date
- `GetTasksByDateHandedIn()` - Filter by deadline
- `CreateTask()`, `UpdateTask()`, `DeleteTask()` - CRUD operations

### User Service
- `GetAllUsers()` - Retrieve all users
- `GetUserById(int id)` - Get specific user
- `CreateUser()`, `UpdateUser()`, `DeleteUser()` - CRUD operations

> **Note**: Service implementations are currently scaffolded and return `NotImplementedException`. These are ready to be implemented as learning progresses.

## 🎓 Learning Outcomes

Through building this project, I gained hands-on experience with:

- ASP.NET Core Web API fundamentals
- Entity Framework Core and database relationships
- Repository and Service layer patterns
- Data Transfer Objects (DTOs) pattern
- Dependency Injection in .NET
- RESTful API design principles
- PostgreSQL integration
- Swagger/OpenAPI documentation
- AutoMapper for object-to-object mapping
- C# language features and best practices

## 🔧 Configuration Details

### Database Connection
- Host: `host.docker.internal` (for Docker setup)
- Port: `5432`
- Database: `comandeerdb`
- Uses Npgsql provider for PostgreSQL

### Development Settings
- Environment: Development
- HTTPS Redirection: Enabled
- Swagger UI: Enabled in Development mode
- Launch URLs: 
  - HTTPS: `https://localhost:7282`
  - HTTP: `http://localhost:5207`

## 🚧 Future Improvements

As a learning project, potential enhancements include:

- [ ] Complete service layer implementations
- [ ] Add authentication and authorization (JWT)
- [ ] Implement proper error handling and logging
- [ ] Add unit and integration tests
- [ ] Create repository pattern implementations
- [ ] Add pagination for list endpoints
- [ ] Implement file attachments for tasks
- [ ] Add task categories/tags
- [ ] Create a frontend application

## 📚 Resources Used

- [Microsoft ASP.NET Core Documentation](https://docs.microsoft.com/aspnet/core)
- [Entity Framework Core Documentation](https://docs.microsoft.com/ef/core)
- [PostgreSQL Documentation](https://www.postgresql.org/docs/)
- Various C# and .NET Core tutorials and courses

## 👨‍💻 Author

**Parham**
- Final-year BSc Computer Science student at University of Westminster
- Learning .NET Core development alongside academic studies
- Interested in FinTech and Business Analytics

## 📄 License

This is a personal learning project created for educational purposes.

---

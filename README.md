# Employee Management System

A mini HR system built with ASP.NET Core MVC, Entity Framework Core, and SQL Server.

## Features

- Full CRUD for Employees and Departments
- Search employees by name or department
- Pagination (10 per page)
- Soft delete (deactivate) employees
- Department delete protection (prevents deleting departments with linked employees)
- Clean responsive UI with Bootstrap 5 (Indigo theme)
- Dashboard with summary statistics
- Repository + Service pattern architecture
- Global error handling with custom error pages

## Tech Stack

- ASP.NET Core 10 MVC
- C#
- Entity Framework Core 10
- SQL Server (LocalDB)
- Bootstrap 5
- Bootstrap Icons
- Google Fonts (Inter)

## Prerequisites

- .NET 10 SDK
- SQL Server (LocalDB is sufficient — included with Visual Studio)

## How to Run

1. Clone the repo:
   ```
   git clone https://github.com/ahmed5121shaban/ai_makers_assessment.git
   ```
2. Navigate to the project:
   ```
   cd EmployeeManagement
   ```
3. Apply migrations and seed data:
   ```
   dotnet ef database update --project EmployeeManagement.Infrastructure --startup-project EmployeeManagement.Web
   ```
4. Run the app:
   ```
   dotnet run --project EmployeeManagement.Web
   ```
5. Open `https://localhost:5001` in your browser.

## Database

- Migrations are in `EmployeeManagement.Infrastructure/Data/Migrations/`
- Seed data (5 departments, 10 employees) is applied automatically on first run via `DataSeeder.cs`
- The connection string uses LocalDB — update `appsettings.json` if using a different SQL Server instance

## Project Structure

```
EmployeeManagement/
├── EmployeeManagement.slnx
├── EmployeeManagement.Core/           Domain entities & interfaces
│   ├── Entities/                      Employee.cs, Department.cs
│   └── Interfaces/                    Repository & Service interfaces
├── EmployeeManagement.Infrastructure/ EF Core, repositories & services
│   ├── Data/                          DbContext, migrations, seed data
│   ├── Repositories/                  Data access implementations
│   └── Services/                      Business logic implementations
└── EmployeeManagement.Web/            ASP.NET Core MVC
    ├── Controllers/                   Employees, Departments, Home
    ├── ViewModels/                    Strongly-typed view models
    ├── Views/                         Razor views
    └── wwwroot/                       Static assets (CSS, JS)
```

## Architecture

- **Presentation Layer**: ASP.NET Core MVC (Controllers + Views + ViewModels)
- **Business Layer**: Service classes with business validation (duplicate email, department constraints)
- **Data Layer**: Repository pattern with EF Core and SQL Server
- All layers communicate through interfaces registered via Dependency Injection

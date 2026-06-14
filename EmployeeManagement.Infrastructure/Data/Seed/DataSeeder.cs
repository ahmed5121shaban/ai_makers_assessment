using EmployeeManagement.Core.Entities;

namespace EmployeeManagement.Infrastructure.Data.Seed;

public static class DataSeeder
{
    public static async Task SeedAsync(AppDbContext context)
    {
        if (context.Departments.Any()) return;

        var departments = new List<Department>
        {
            new() { Name = "Engineering", Description = "Software and hardware engineering" },
            new() { Name = "Human Resources", Description = "People operations and hiring" },
            new() { Name = "Sales", Description = "Revenue and client acquisition" },
            new() { Name = "Marketing", Description = "Brand and digital marketing" },
            new() { Name = "Finance", Description = "Accounting and financial planning" }
        };

        context.Departments.AddRange(departments);
        await context.SaveChangesAsync();

        if (context.Employees.Any()) return;

        var employees = new List<Employee>
        {
            new() { FullName = "Ahmed Hassan", Email = "ahmed.hassan@company.com", MobileNumber = "+201001234567", DepartmentId = departments[0].DepartmentId, JobTitle = "Senior Software Engineer", HireDate = new DateTime(2022, 3, 15) },
            new() { FullName = "Sara Khalid", Email = "sara.khalid@company.com", MobileNumber = "+201001234568", DepartmentId = departments[0].DepartmentId, JobTitle = "Backend Developer", HireDate = new DateTime(2023, 1, 10) },
            new() { FullName = "Mohamed Ali", Email = "mohamed.ali@company.com", MobileNumber = "+201001234569", DepartmentId = departments[1].DepartmentId, JobTitle = "HR Manager", HireDate = new DateTime(2021, 6, 1) },
            new() { FullName = "Noor Emad", Email = "noor.emad@company.com", MobileNumber = "+201001234570", DepartmentId = departments[2].DepartmentId, JobTitle = "Sales Executive", HireDate = new DateTime(2023, 9, 20) },
            new() { FullName = "Omar Youssef", Email = "omar.youssef@company.com", MobileNumber = "+201001234571", DepartmentId = departments[3].DepartmentId, JobTitle = "Marketing Specialist", HireDate = new DateTime(2022, 11, 5) },
            new() { FullName = "Lina Samir", Email = "lina.samir@company.com", MobileNumber = "+201001234572", DepartmentId = departments[4].DepartmentId, JobTitle = "Financial Analyst", HireDate = new DateTime(2024, 2, 14) },
            new() { FullName = "Khaled Mostafa", Email = "khaled.mostafa@company.com", MobileNumber = "+201001234573", DepartmentId = departments[0].DepartmentId, JobTitle = "DevOps Engineer", HireDate = new DateTime(2023, 5, 8) },
            new() { FullName = "Dalia Ibrahim", Email = "dalia.ibrahim@company.com", MobileNumber = "+201001234574", DepartmentId = departments[1].DepartmentId, JobTitle = "Recruiter", HireDate = new DateTime(2024, 7, 22) },
            new() { FullName = "Tamer Adel", Email = "tamer.adel@company.com", MobileNumber = "+201001234575", DepartmentId = departments[2].DepartmentId, JobTitle = "Account Manager", HireDate = new DateTime(2022, 4, 18) },
            new() { FullName = "Hana Mahmoud", Email = "hana.mahmoud@company.com", MobileNumber = "+201001234576", DepartmentId = departments[3].DepartmentId, JobTitle = "Content Writer", HireDate = new DateTime(2023, 8, 30), IsActive = false }
        };

        context.Employees.AddRange(employees);
        await context.SaveChangesAsync();
    }
}

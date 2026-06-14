using EmployeeManagement.Core.Entities;

namespace EmployeeManagement.Core.Interfaces;

public interface IEmployeeService
{
    Task<(IEnumerable<Employee> Employees, int TotalCount)> GetEmployeesAsync(string? search = null, int page = 1, int pageSize = 10);
    Task<Employee?> GetByIdAsync(int id);
    Task AddAsync(Employee employee);
    Task UpdateAsync(Employee employee);
    Task DeleteAsync(int id);
    Task<int> GetTotalCountAsync();
    Task<int> GetActiveCountAsync();
}

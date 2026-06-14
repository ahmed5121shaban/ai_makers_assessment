using Microsoft.Extensions.Logging;
using EmployeeManagement.Core.Entities;
using EmployeeManagement.Core.Interfaces;

namespace EmployeeManagement.Infrastructure.Services;

public class EmployeeService : IEmployeeService
{
    private readonly IEmployeeRepository _repository;
    private readonly ILogger<EmployeeService> _logger;

    public EmployeeService(IEmployeeRepository repository, ILogger<EmployeeService> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task<(IEnumerable<Employee> Employees, int TotalCount)> GetEmployeesAsync(string? search = null, int page = 1, int pageSize = 10)
    {
        var employees = await _repository.GetAllAsync(search, page, pageSize);
        var totalCount = await _repository.GetCountAsync(search);
        return (employees, totalCount);
    }

    public async Task<Employee?> GetByIdAsync(int id)
    {
        return await _repository.GetByIdAsync(id);
    }

    public async Task AddAsync(Employee employee)
    {
        if (await _repository.EmailExistsAsync(employee.Email))
            throw new InvalidOperationException($"An employee with email '{employee.Email}' already exists.");

        await _repository.AddAsync(employee);
        await _repository.SaveChangesAsync();
        _logger.LogInformation("Employee '{Name}' created (ID {Id})", employee.FullName, employee.EmployeeId);
    }

    public async Task UpdateAsync(Employee employee)
    {
        if (await _repository.EmailExistsAsync(employee.Email, employee.EmployeeId))
            throw new InvalidOperationException($"Another employee with email '{employee.Email}' already exists.");

        employee.UpdatedAt = DateTime.UtcNow;
        await _repository.UpdateAsync(employee);
        await _repository.SaveChangesAsync();
        _logger.LogInformation("Employee '{Name}' updated (ID {Id})", employee.FullName, employee.EmployeeId);
    }

    public async Task DeleteAsync(int id)
    {
        var employee = await _repository.GetByIdAsync(id)
            ?? throw new KeyNotFoundException($"Employee with ID {id} not found.");

        employee.IsActive = false;
        employee.UpdatedAt = DateTime.UtcNow;
        await _repository.UpdateAsync(employee);
        await _repository.SaveChangesAsync();
        _logger.LogInformation("Employee '{Name}' deactivated (ID {Id})", employee.FullName, id);
    }

    public async Task<int> GetTotalCountAsync()
    {
        return await _repository.GetTotalCountAsync();
    }

    public async Task<int> GetActiveCountAsync()
    {
        return await _repository.GetActiveCountAsync();
    }
}

using Microsoft.EntityFrameworkCore;
using EmployeeManagement.Core.Entities;
using EmployeeManagement.Core.Interfaces;
using EmployeeManagement.Infrastructure.Data;

namespace EmployeeManagement.Infrastructure.Repositories;

public class EmployeeRepository : IEmployeeRepository
{
    private readonly AppDbContext _context;

    public EmployeeRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Employee>> GetAllAsync(string? search = null, int page = 1, int pageSize = 10)
    {
        var query = _context.Employees
            .Include(e => e.Department)
            .Where(e => e.IsActive)
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(search))
        {
            var term = search.Trim().ToLower();
            query = query.Where(e => e.FullName.ToLower().Contains(term) || e.Department.Name.ToLower().Contains(term));
        }

        return await query
            .OrderByDescending(e => e.CreatedAt)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
    }

    public async Task<int> GetCountAsync(string? search = null)
    {
        var query = _context.Employees.Where(e => e.IsActive).AsQueryable();

        if (!string.IsNullOrWhiteSpace(search))
        {
            var term = search.Trim().ToLower();
            query = query.Where(e => e.FullName.ToLower().Contains(term) || e.Department.Name.ToLower().Contains(term));
        }

        return await query.CountAsync();
    }

    public async Task<Employee?> GetByIdAsync(int id)
    {
        return await _context.Employees
            .Include(e => e.Department)
            .FirstOrDefaultAsync(e => e.EmployeeId == id);
    }

    public async Task<bool> EmailExistsAsync(string email, int? excludeId = null)
    {
        return await _context.Employees
            .AnyAsync(e => e.Email == email && (!excludeId.HasValue || e.EmployeeId != excludeId.Value));
    }

    public async Task AddAsync(Employee employee)
    {
        await _context.Employees.AddAsync(employee);
    }

    public Task UpdateAsync(Employee employee)
    {
        _context.Employees.Update(employee);
        return Task.CompletedTask;
    }

    public async Task<int> GetTotalCountAsync()
    {
        return await _context.Employees.CountAsync();
    }

    public async Task<int> GetActiveCountAsync()
    {
        return await _context.Employees.CountAsync(e => e.IsActive);
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}

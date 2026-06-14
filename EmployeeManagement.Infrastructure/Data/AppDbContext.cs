using Microsoft.EntityFrameworkCore;
using EmployeeManagement.Core.Entities;

namespace EmployeeManagement.Infrastructure.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Employee> Employees => Set<Employee>();
    public DbSet<Department> Departments => Set<Department>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Department>(e =>
        {
            e.HasKey(d => d.DepartmentId);
            e.Property(d => d.Name).HasMaxLength(100).IsRequired();
            e.HasIndex(d => d.Name).IsUnique();
            e.Property(d => d.Description).HasMaxLength(255);
            e.Property(d => d.CreatedAt).HasDefaultValueSql("GETUTCDATE()");
        });

        modelBuilder.Entity<Employee>(e =>
        {
            e.HasKey(em => em.EmployeeId);
            e.Property(em => em.FullName).HasMaxLength(150).IsRequired();
            e.Property(em => em.Email).HasMaxLength(150).IsRequired();
            e.HasIndex(em => em.Email).IsUnique();
            e.Property(em => em.MobileNumber).HasMaxLength(20).IsRequired();
            e.Property(em => em.JobTitle).HasMaxLength(100).IsRequired();
            e.Property(em => em.HireDate).IsRequired();
            e.Property(em => em.IsActive).HasDefaultValue(true);
            e.Property(em => em.CreatedAt).HasDefaultValueSql("GETUTCDATE()");

            e.HasOne(em => em.Department)
                .WithMany(d => d.Employees)
                .HasForeignKey(em => em.DepartmentId)
                .OnDelete(DeleteBehavior.Restrict);

            e.HasIndex(em => em.DepartmentId);
            e.HasIndex(em => em.FullName);
        });
    }
}

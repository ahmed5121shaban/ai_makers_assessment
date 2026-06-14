using System.ComponentModel.DataAnnotations;

namespace EmployeeManagement.Web.ViewModels;

public class DepartmentViewModel
{
    public int DepartmentId { get; set; }

    [Required(ErrorMessage = "Department name is required")]
    [StringLength(100, MinimumLength = 2, ErrorMessage = "Name must be between 2 and 100 characters")]
    public string Name { get; set; } = string.Empty;

    [StringLength(255, ErrorMessage = "Description cannot exceed 255 characters")]
    public string? Description { get; set; }

    public int EmployeeCount { get; set; }
}

using System.ComponentModel.DataAnnotations;

namespace EmployeeManagement.Web.ViewModels;

public class EmployeeViewModel
{
    public int EmployeeId { get; set; }

    [Required(ErrorMessage = "Full name is required")]
    [StringLength(150, MinimumLength = 2, ErrorMessage = "Full name must be between 2 and 150 characters")]
    [Display(Name = "Full Name")]
    public string FullName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Enter a valid email address")]
    [StringLength(150)]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "Mobile number is required")]
    [RegularExpression(@"^\+?[0-9]{7,15}$", ErrorMessage = "Enter a valid mobile number (7-15 digits, optional + prefix)")]
    [Display(Name = "Mobile Number")]
    public string MobileNumber { get; set; } = string.Empty;

    [Required(ErrorMessage = "Department is required")]
    [Display(Name = "Department")]
    public int DepartmentId { get; set; }

    [Required(ErrorMessage = "Job title is required")]
    [StringLength(100, ErrorMessage = "Job title cannot exceed 100 characters")]
    [Display(Name = "Job Title")]
    public string JobTitle { get; set; } = string.Empty;

    [Required(ErrorMessage = "Hire date is required")]
    [DataType(DataType.Date)]
    [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
    [Display(Name = "Hire Date")]
    public DateTime HireDate { get; set; } = DateTime.Today;

    public string? DepartmentName { get; set; }
    public bool IsActive { get; set; } = true;
}

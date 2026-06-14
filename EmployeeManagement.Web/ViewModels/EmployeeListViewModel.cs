namespace EmployeeManagement.Web.ViewModels;

public class EmployeeListViewModel
{
    public IEnumerable<EmployeeViewModel> Employees { get; set; } = new List<EmployeeViewModel>();
    public string? Search { get; set; }
    public int Page { get; set; } = 1;
    public int TotalCount { get; set; }
    public int PageSize { get; set; } = 10;
    public int TotalPages => (int)Math.Ceiling((double)TotalCount / PageSize);
}

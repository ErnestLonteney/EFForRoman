namespace DataAccess.Entities;

public class Manager
{
    public Guid Id { get; private set; }

    public string FirstName { get; set; } = string.Empty;

    public string LastName { get; set; } = string.Empty;

    public string? Email { get; set; }

    public string? Phone { get; set; }

    public DateOnly DateOfBirth { get; set; }

    public double Salary { get; set; }

    public DateOnly WorkStart { get; set; }

    public string? Notes { get; set; }

    public List<Order>? MyManagedOrders { get; set; }

    public ICollection<Department> Departments { get; set; } = [];
}

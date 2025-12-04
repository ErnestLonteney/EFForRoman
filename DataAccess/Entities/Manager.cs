namespace DataAccess.Entities;

public class Manager : Person
{  
    public DateOnly DateOfBirth { get; set; }

    public double Salary { get; set; }

    public DateOnly WorkStart { get; set; }

    public string? Notes { get; set; }

    public virtual List<Order>? MyManagedOrders { get; set; }

    public virtual ICollection<Department> Departments { get; set; } = [];
}

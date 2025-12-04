namespace DataAccess.Entities;

// [Table("SuperUsers")]
// [Index(nameof(FirstName), nameof(LastName))]
public class Customer : Person  
{
    public Customer(string firstName, string lastName)
        : base(firstName, lastName) 
    {   
    }

    public Customer()
    {
        
    }

    public float Discount { get; set; }

    public DateOnly DateOfBirth { get; set; }

    public virtual List<Order> MyOrders { get; set; }
}

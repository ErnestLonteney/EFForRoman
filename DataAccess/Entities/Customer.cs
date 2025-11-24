using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Entities;

// [Table("SuperUsers")]
// [Index(nameof(FirstName), nameof(LastName))]
public class Customer
{
    public Customer(string firstName, string lastName)
    {
        FirstName = firstName;
        LastName = lastName;    
    }

    public Customer()
    {
        
    }

    public Guid Id { get; private set; }

    public float Discount { get; set; }

    //  [MaxLength(200)]
    public string FirstName { get; set; } = string.Empty;

    //   [MaxLength(200)]
    public string LastName { get; set; } = string.Empty;

    //  [MaxLength(300)]
    public string? Email { get; set; }

    // [MaxLength(15)]
    public string? Phone { get; set; }

    public DateOnly DateOfBirth { get; set; }


    public Address? Address { get; set; }

    public List<Order> MyOrders { get; set; }
}

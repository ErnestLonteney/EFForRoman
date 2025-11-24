using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Entities;

// [PrimaryKey(nameof(CustomerId))]
public class Address
{
    public Guid CustomerId { get; set; }

    public string Street { get; set; } = string.Empty;

    public string City { get; set; } = string.Empty;

    public string? ZipCode { get; set; }

    public string Country { get; set; } = string.Empty;


 //   [DeleteBehavior(DeleteBehavior.Cascade)]
  //  [ForeignKey(nameof(CustomerId))]
    public Customer Customer { get; set; } = null!;
}

namespace DataAccess.Entities;

// [PrimaryKey(nameof(CustomerId))]
public class Address
{
    public Guid PersonId { get; set; }

    public string Street { get; set; } = string.Empty;

    public string City { get; set; } = string.Empty;

    public string? ZipCode { get; set; }

    public string Country { get; set; } = string.Empty;


 //   [DeleteBehavior(DeleteBehavior.Cascade)]
  //  [ForeignKey(nameof(CustomerId))]
    public virtual Person Person { get; set; } = null!;
}

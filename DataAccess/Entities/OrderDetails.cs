using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Entities;

[PrimaryKey(nameof(OrderId), nameof(ProductCode))]
public class OrderDetails
{
    public Guid OrderId { get; set; } // [order_id]

    public int ProductCode { get; set; }  // [productCode]

    public int Quantity { get; set; } // [quantity]

    // Navigation properties

    [ForeignKey(nameof(OrderId))]
    public Order Order { get; set; } = null!;

  //  [DeleteBehavior(DeleteBehavior.Cascade)]
  //  [ForeignKey(nameof(ProductCode))]
    public Product Product { get; set; } = null!;

}

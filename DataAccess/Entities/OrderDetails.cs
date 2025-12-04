using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Entities;

// [PrimaryKey(nameof(OrderId), nameof(ProductCode))]
public class OrderDetails
{
    public Guid OrderId { get; set; } // [order_id]

    public int ProductCode { get; set; }  // [productCode]

    public int Quantity { get; set; } // [quantity]

    public int ProductId { get; set; }

    // Navigation properties

    // [ForeignKey(nameof(OrderId))]
    public virtual Order Order { get; set; } = null!;

  //  [DeleteBehavior(DeleteBehavior.Cascade)]
  //  [ForeignKey(nameof(ProductCode))]
    public virtual Product Product { get; set; } = null!;

}

namespace DataAccess.Entities;

public class Order
{
    public Guid Id { get; set; } // [id]

    public DateTime Date { get; set; } // [date]

   
    // Navigation properties

    public virtual Customer Customer { get; set; } = null!;

    public virtual Manager? Manager { get; set; }

    public virtual List<OrderDetails> Details { get; set; } = [];
}

using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Entities;

public class Order
{
    public Guid Id { get; set; } // [id]

    public DateTime Date { get; set; } // [date]

    public Customer Customer { get; set; } = null!;

    public Manager? Manager { get; set; }

    public List<OrderDetails> Details { get; set; } = [];
}

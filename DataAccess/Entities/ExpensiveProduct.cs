namespace DataAccess.Entities;

public class ExpensiveProduct
{
    public string Name { get; set; } = string.Empty;

    public decimal? Price { get; set; }

    public decimal? RawPrice { get; set; }
}

namespace DataAccess.Entities
{
    // [PrimaryKey(nameof(Code))]
   // [Index(nameof(Name), IsUnique = false)]
    public class Product
    {
        public Product(string name, decimal price)
        {
            Name = name;
            Price = price;
        }

        public Product()
        {
                
        }

        // [Key]
        public int Code { get; private set; } // [code]

       // [MaxLength(200)]
        public string Name { get; set; } = string.Empty; // [name]

        public string? Description { get; set; } // [description]

       // [Column(TypeName = "money")]
        public decimal? Price { get; set; } // [price]
    }
}

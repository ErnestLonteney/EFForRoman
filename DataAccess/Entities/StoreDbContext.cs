using DataAccess.Configurations;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Entities;

public class StoreDbContext : DbContext
{
    public StoreDbContext()
    {
       // Database.EnsureDeleted();  
        Database.EnsureCreated();   
    }

    public DbSet<Product> Products { get; set; } = null!;

    public DbSet<Customer> Customers { get; set; } = null!;

    public DbSet<Order> Orders { get; set; } = null!;

    public DbSet<OrderDetails> OrderDetails { get; set; } = null!;

    public DbSet<Address> Addresses { get; set; } = null!;

    public DbSet<Department> Departments { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new ProductConfiguration());


        modelBuilder.Entity<Product>().Property<decimal>("SellPrice").HasComputedColumnSql("(Price * 1.2)");

        modelBuilder.Entity<Customer>().ToTable("SuperUsers");

        //modelBuilder.Entity<Customer>()
        //    .Property(c => c.Id)
          //  .ValueGeneratedOnAdd();

        modelBuilder.Entity<Customer>()
            .Property(c => c.FirstName)
            .HasMaxLength(200);

        modelBuilder.Entity<Customer>().Property(c => c.LastName)
            .HasMaxLength(200);

        modelBuilder.Entity<Customer>().Property(c => c.Email)
            .HasMaxLength(300);

        modelBuilder.Entity<Customer>().HasIndex(c => new { c.FirstName, c.LastName });

        modelBuilder.Entity<Order>().Property(o => o.Date)
            .HasDefaultValueSql("GETDATE()");

        // Order -> OrderDetails (1 - many) relationship
        modelBuilder.Entity<Order>()
            .HasMany(o => o.Details)
            .WithOne(od => od.Order)
            .IsRequired(true)
            .OnDelete(DeleteBehavior.Cascade);

        // Product -> OrderDetails  (1 to many) relationship
        modelBuilder.Entity<OrderDetails>() // ProductId NOT NULL
            .HasOne(od => od.Product)
            .WithMany()
            .IsRequired(true)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Order>()
            .HasOne(o => o.Customer)
            .WithMany(c => c.MyOrders)
            .IsRequired(true);

        modelBuilder.Entity<Address>()
            .HasKey(a => a.CustomerId);

        modelBuilder.Entity<Customer>()
             .HasOne(c => c.Address)
             .WithOne(a => a.Customer)
             .IsRequired(false)
             .OnDelete(DeleteBehavior.Cascade);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=localhost;Database=MyStore;Trusted_Connection=True;TrustServerCertificate=true");
    }
}

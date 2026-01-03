using DataAccess.Configurations;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Entities;

public class StoreDbContext : DbContext
{
    public StoreDbContext()
    {
       // Database.EnsureDeleted();
       // Database.EnsureCreated();
        Database.Migrate();   
    }

    public string Role { get; init; }

    public DbSet<Product> Products { get; set; } = null!;

    public DbSet<Customer> Customers { get; set; } = null!;

    public DbSet<Manager> Managers { get; set; }

    public DbSet<Order> Orders { get; set; } = null!;

    public DbSet<OrderDetails> OrderDetails { get; set; } = null!;

    public DbSet<Address> Addresses { get; set; } = null!;

    public DbSet<Department> Departments { get; set; } = null!;

    public DbSet<Person> People { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new ProductConfiguration());

       // modelBuilder.Entity<Customer>().ToTable("SuperUsers");


        //modelBuilder.Entity<Customer>()
        //    .Property(c => c.Id)
        //  .ValueGeneratedOnAdd();

        modelBuilder.Entity<Customer>()
            .Property(c => c.FirstName)
            .HasMaxLength(200);

        modelBuilder.Entity<Person>().HasKey(c => c.Id);

        modelBuilder.Entity<Person>().Property(c => c.LastName)
            .HasMaxLength(200);

        modelBuilder.Entity<Person>().Property(c => c.Email)
            .HasMaxLength(300);

        modelBuilder.Entity<Person>().HasIndex(c => new { c.FirstName, c.LastName });

        modelBuilder.Entity<Order>().Property(o => o.Date)
            .HasDefaultValueSql("GETDATE()");

        modelBuilder.Entity<OrderDetails>()
            .HasKey(od => new { od.OrderId, od.ProductId });

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
            .HasKey(a => a.PersonId);

        modelBuilder.Entity<Person>()
             .HasOne(c => c.Address)
             .WithOne(a => a.Person)
             .IsRequired(false)
             .OnDelete(DeleteBehavior.Cascade);


        modelBuilder.Entity<Person>().HasQueryFilter(p => Role == "Admin" || p.DateOfBirth < new DateOnly(2007, 1, 1));

        modelBuilder.Entity<ExpensiveProduct>().HasNoKey().ToView("V_EXPENSIVE_PRODUCTS");
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder
           // .UseLazyLoadingProxies()
            .UseSqlServer("Server=localhost;Database=MyStore;Trusted_Connection=True;TrustServerCertificate=true");
    }
}

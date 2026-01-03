using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace ChangeTracking
{
    internal class Program
    {
        static void Main()
        {
            using var context = new StoreDbContext();

            var product = new Product
            {
                Name = "New Product",
                Price = 99.99m,
                Description = "This is a new product"
            };


            Console.WriteLine(context.Entry(product).State);

            context.Products.Add(product);

            Console.WriteLine(context.Entry(product).State);

            context.SaveChanges();

            Console.WriteLine(context.Entry(product).State);

            var existingProduct = context.Products.FirstOrDefault(p => p.Name == "New Product");

            Console.WriteLine(context.Entry(existingProduct).State);

            existingProduct.Price = 120.99m;

            Console.WriteLine(context.Entry(existingProduct).State);

            context.SaveChanges();

            Console.WriteLine(context.Entry(existingProduct).State);

            context.Products.Remove(existingProduct);

            Console.WriteLine(context.Entry(existingProduct).State);

            context.SaveChanges();

            Console.WriteLine(context.Entry(existingProduct).State);

            existingProduct.Price = 59.99m;

            Console.WriteLine(context.Entry(existingProduct).State);


            Console.WriteLine(new String('-', 50));

            var result = context.Products.AsNoTracking().ToList();

            foreach (var prod in result)
            {
                prod.Price += 10;

                Console.WriteLine(context.Entry(prod).State);
            }

            foreach (var prod in result)
            {
                if (prod.Price > 100)
                {
                    // context.Attach(prod);
                    //   Console.WriteLine(context.Entry(prod).State);
                    prod.Price += 10;
                    prod.Description = "3424325345";
                    Console.WriteLine(context.Entry(prod).State);

                    // UPDATE PRODUCTS SET PRICE = PRICE + 10 WHERE Id = 12
                    context.Entry(prod).Property(p => p.Price).IsModified = true;
                    Console.WriteLine(context.Entry(prod).State);
                }

                Console.WriteLine(context.Entry(prod).State);
            }

            Console.WriteLine(context.ChangeTracker.DebugView.LongView.ToString());
        }
    }
}

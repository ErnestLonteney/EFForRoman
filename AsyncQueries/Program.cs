using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace AsyncQueries
{
    internal class Program
    {
        static async Task Main()
        {
           _ = GetAllProducts().ContinueWith(async result =>
            {
                foreach (var product in result.Result)
                {
                    Console.WriteLine(product.Name);
                }

                await BulkUpdateAsync();
            });


            for (int i = 0; i < 1000; i++)
            {
                Console.Write("*");
            }

            Console.ReadKey();
        }


        public static async Task<List<Product>> GetAllProducts()
        {
            using var context = new StoreDbContext();

            return await context.Products.ToListAsync();
        }

        public static async Task BulkUpdateAsync()
        {
            using (var context = new StoreDbContext())
            {
                var productsForUpdate = await context.Products.Where(p => p.Price > 1000).ToListAsync();

                foreach (var product in productsForUpdate)
                {
                    product.Price *= 1.1m;
                }

                context.SaveChanges();

                // FAST 
                await context.Products.Where(p => p.Price > 1000)
                              .ExecuteUpdateAsync(setters =>
                                             setters.SetProperty(
                                                     p => p.Price,
                                                     p => p.Price * 1.1m
                                             )
                                            .SetProperty(p => p.Description, p => "EMPTY"));


                /* UPDATE PRODUCT 
                   SET Price = Price * 0.1,
                       Description = 'EMPTY'
                   WHERE Id IN (SELECT ID FROM PRODUCTS WHERE Price > 1000) */


                await context.Products.Where(p => p.Price < 1000)
                             .ExecuteDeleteAsync();

                /* DELETE PRODUCTS WHERE Price < 1000 */
            }
        }
    }
}

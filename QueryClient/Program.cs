using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace QueryClient
{
    internal class Program
    {
        static void Main()
        {
            using var storeDbContext = new StoreDbContext();

            // LINQ to Entities

            int[] array = [1, 23, 43, 435];

            IEnumerable<int> newArray = array
                .Where(n => n < 100 && n > 10)
                .Take(3)
                .OrderBy(n => n);

            foreach (var item in newArray)
            {
                Console.WriteLine(item);
            }

            // LINQ to Entities

            var orders = storeDbContext.Orders
                 .Where(o => o.Date > new DateTime(2024, 1, 1))
                 .Take(3);

            var orders3 = orders.OrderBy(o => o.Date);


            // foreach (var order in orders)
            // {
            //     Console.WriteLine(order.Date);
            // }

            // Console.WriteLine();

            // Querying data

            var orders2 = storeDbContext.Orders
                  .Include(o => o.Customer)
                  .Include(o => o.Details)
                  .ThenInclude(od => od.Product)
                .Where(o => o.Date > new DateTime(2025, 1, 1));
             ;

            Console.WriteLine(orders2.ToQueryString());

            Order? ordedFirst = orders2.FirstOrDefault();

            if (ordedFirst is not null)
            {
                // Explicit loading
                //storeDbContext.Entry(ordedFirst)
                //    .Collection(o => o.Details)
                //    .Query()
                //    .Where(od => od.Quantity > 10)
                //    .Load();

                //storeDbContext.Entry(ordedFirst)
                //   .Reference(o => o.Customer)
                //   .Load();
            }

            foreach (var order in orders2)
            {
                if (order is not null)
                {
                    Console.WriteLine("Order");
                    Console.WriteLine(order.Date);
                    Console.WriteLine($"Order has been ordered by {order.Customer.FirstName} {order.Customer.LastName}");

                    foreach (var detail in order.Details)
                    {
                        Console.WriteLine($"Product: {detail.Product.Name} - Quantity: {detail.Quantity}");
                        Console.WriteLine($"Total {detail.Product.Price * detail.Quantity:C2}");
                    }
                }
            }
        }
    }
}

using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace Client
{
    internal class Program
    {
        static void Main()
        {
            using StoreDbContext storeDbContext = new();

            //var product = new Product("Monitor", 19.99m);
            //var product2 = new Product("Stylus", 2.99m);

            //var customer = new Customer("John", "Doe")
            //{
            //    DateOfBirth = new DateOnly(1990, 5, 15),
            //    Email = "john@mail.com",
            //    Phone = "123-456-7890",
            //};

            //storeDbContext.Products.AddRange(product, product2);
            //storeDbContext.Customers.Add(customer);

            //var firstOrder = new Order
            //{
            //    Customer = customer,
            //};

            //var orderDetails1 = new OrderDetails
            //{
            //    Order = firstOrder,
            //    Product = product,
            //    Quantity = 2,
            //};

            //var orderDetails2 = new OrderDetails
            //{
            //    Order = firstOrder,
            //    Product = product2,
            //    Quantity = 5,
            //};

            //storeDbContext.OrderDetails.AddRange(orderDetails1, orderDetails2);
            //storeDbContext.SaveChanges();


            // Querying data

            var orders = storeDbContext.Orders
                .Include(o => o.Customer)
                .Include(o => o.Details)
                .ThenInclude(od => od.Product)
                .Where(o => o.Date > new DateTime(2025, 1, 1));

            Console.WriteLine(orders.ToQueryString());

            foreach (var order in orders)
            {
                if (order is not null)
                {
                    Console.WriteLine("Order");
                    Console.WriteLine(order.Date);
                    Console.WriteLine($"Order has been ordered by {order.Customer.FirstName} {order.Customer.LastName}");

                    foreach (var detail in order.Details)
                    {
                        Console.WriteLine($"Product: {detail.Product.Name} - Quantity: {detail.Quantity}");
                        Console.WriteLine($"Total {detail.Product.Price * detail.Quantity}:C2");
                    }
                }
            }
        }
    }
}

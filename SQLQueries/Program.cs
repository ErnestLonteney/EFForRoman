using DataAccess.Entities;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace SQLQueries
{
    public class Program
    {
        public static void Main(string[] args)
        {
            using var db = new StoreDbContext();

            var priceParametr = new SqlParameter("@price", 300);

            IQueryable<Product> products = db.Products.FromSqlRaw("SELECT * FROM Products WHERE Price > @price", priceParametr).OrderBy(p => p.Price);
            Console.WriteLine(products.ToQueryString());
           // IQueryable<Product> products2 = db.Products.Where(p => p.Price > 300).OrderBy(p => p.Price);

            foreach (var product in products)
            {
                Console.WriteLine($"{product.Name} {product.Price}");
            }

            foreach (var product in products)
            {
                Console.WriteLine($"{product.Name} {product.Price}");
            }

            Console.WriteLine(new String('*', 30));

            SqlParameter parameter1 = new SqlParameter("@lowPrice", 100);
            parameter1.DbType = System.Data.DbType.Decimal;
            SqlParameter parameter2 = new SqlParameter("@highPrice", 1000);
            parameter2.DbType = System.Data.DbType.Decimal;
            IQueryable<Product> filteredProducts = db.Products.FromSqlRaw("EXEC [SP_GetSuitableProducts] @lowPrice, @highPrice", parameter1, parameter2);

            foreach (Product product1 in filteredProducts)
            {
                Console.WriteLine($"{product1.Name} {product1.Price}");
            }

            SqlParameter priceIncreaseParametr = new SqlParameter("@procent", 10);
            priceIncreaseParametr.DbType = System.Data.DbType.Decimal;
            SqlParameter higherPriceParametr = new SqlParameter
            {
                ParameterName = "@higherPrice",
                SqlDbType = System.Data.SqlDbType.Decimal,
                Direction = System.Data.ParameterDirection.Output
            };
            SqlParameter lowerPriceParametr = new SqlParameter
            {
                ParameterName = "@lowerPrice",
                SqlDbType = System.Data.SqlDbType.Decimal,
                Direction = System.Data.ParameterDirection.Output
            };

            db.Database.ExecuteSqlRaw("EXEC [dbo].[PricesUp] @procent, @higherPrice OUTPUT, @lowerPrice OUTPUT", priceIncreaseParametr, higherPriceParametr, lowerPriceParametr);

            int countRemovedProducts = db.Database.ExecuteSqlRaw("DELETE PRODUCTS WHERE PRICE < 0");

            Console.WriteLine(higherPriceParametr.Value);
            Console.WriteLine(lowerPriceParametr.Value);

            SqlParameter dateFrom = new SqlParameter("@datefrom", new DateTime(2000, 01, 01));
            SqlParameter dateTo = new SqlParameter("@dateto", DateTime.Now);

            var soldProducts = db.Products.FromSqlRaw("SELECT * FROM GetTheMostPopularProduct(@datefrom, @dateto)", dateFrom, dateTo);

            foreach (Product p in soldProducts)
            {
                Console.WriteLine(p.Name);
                Console.WriteLine(p.Price);
            }
        }
    }
}

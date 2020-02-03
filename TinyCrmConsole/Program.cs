using System;
using System.Linq;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.FileExtensions;
using Microsoft.Extensions.Configuration.Json;

using TinyCrm.Core.Model;
using TinyCrm.Core.Model.Options;
using TinyCrm.Core.Services;

namespace TinyCrmConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", false, true)
                .Build();
            var connString = config.GetValue<string>("connectionString");

            var db = new TinyCrm.Core.Data.TinyCrmDbContext(connString);
            var c = db.Database.EnsureCreated();

            var cust = new Customer()
            {
                Email = "Πνευματικός",
            };

            db.Add(cust);
            db.SaveChanges();

            var results = db.Set<Customer>()
                .Where(c => c.Email.Equals(
                    "πνευματικoς"))
                .FirstOrDefault();

            var productService = new ProductService();

            productService.AddProduct(
                new AddProductOptions() {
                    Id = "123",
                    Price = 13.33M,
                    ProductCategory = ProductCategory.Cameras,
                    Name = "Camera 1"
                });

            productService.UpdateProduct("123",
                new UpdateProductOptions() {
                    Price = 22.22M
                });
        }
    }
}

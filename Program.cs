using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TinyCrm
{
    class Program
    {
        static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                .WriteTo.Console()
                .WriteTo.File($@"{System.IO.Directory.GetCurrentDirectory()}\logs\{DateTime.Now:yyyy-MM-dd}\log-.txt",
                    rollingInterval: RollingInterval.Day)
                .CreateLogger();

            Log.Error("this is an error");

            Console.ReadKey();

            var productService = new TinyCrm.Services.ProductService();
            productService.AddProduct(new Model.Options.AddProductOptions()
            {
                ProductCategory = (Model.ProductCategory)155
            });
        }
    }
}

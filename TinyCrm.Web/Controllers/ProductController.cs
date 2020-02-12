using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using Microsoft.AspNetCore.Mvc;
using TinyCrm.Core.Data;
using TinyCrm.Core.Model;

namespace TinyCrm.Web.Controllers
{
    public class ProductController : Controller
    {
        private IContainer Container { get; set; }
        private TinyCrmDbContext Context { get; set; }
        public ProductController()
        {
             Container = Core.ServiceRegistrator.GetContainer();
             Context = Container.Resolve<TinyCrmDbContext>();
        }

        public IActionResult Index()
        {
            var productList = Context
                .Set<Product>()
                .Take(100)
                .ToList();
            var customerList = Context
                .Set<Customer>()
                .Take(100)
                .ToList();

            var model = new Models.CustomersProductsViewModel()
            {
                Customers = customerList,
                Products = productList
            };

            return View(model);
        }
    }
}
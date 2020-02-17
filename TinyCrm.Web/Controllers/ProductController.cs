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
        private TinyCrmDbContext context_;

        public ProductController(TinyCrmDbContext context)
        {
            context_ = context;
        }

        public IActionResult Index()
        {
            var productList = context_
                .Set<Product>()
                .Take(100)
                .ToList();
            var customerList = context_
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
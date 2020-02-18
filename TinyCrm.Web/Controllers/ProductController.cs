using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using Microsoft.AspNetCore.Mvc;
using TinyCrm.Core;
using TinyCrm.Core.Data;
using TinyCrm.Core.Model;
using TinyCrm.Core.Services;
using TinyCrm.Web.Extensions;

namespace TinyCrm.Web.Controllers
{
    public class ProductController : Controller
    {
        private TinyCrmDbContext context_;
        private IProductService products_;

        public ProductController(
            TinyCrmDbContext context,
            IProductService products)
        {
            context_ = context;
            products_ = products;
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

        [HttpGet("product/{id}")]
        public async Task<IActionResult> Get(
            string id)
        {
            var result = await products_.GetProductByIdAsync(id);

            return result.AsStatusResult();
        }

        [HttpPut("product/{id}")]
        public async Task<IActionResult> Update(string id,
           [FromBody] Core.Model.Options.UpdateProductOptions options)
        {
            var result = await products_.UpdateProductAsync(id, options);

            if (result) {
                var res = await products_.GetProductByIdAsync(id);
                return res.AsStatusResult();
            } else {
                return new ApiResult<Product>()
                {
                    ErrorCode = Core.StatusCode.InternalServerError
                }.AsStatusResult();
            }
        }
    }
}
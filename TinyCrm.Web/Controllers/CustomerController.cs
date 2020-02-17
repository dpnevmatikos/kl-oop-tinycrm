using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TinyCrm.Core.Data;
using TinyCrm.Core.Model;

namespace TinyCrm.Web.Controllers
{
    public class CustomerController : Controller
    {
        private TinyCrmDbContext context_;
        private Core.Services.ICustomerService customers_;

        public CustomerController(
            TinyCrmDbContext context,
            Core.Services.ICustomerService customers)
        {
            context_ = context;
            customers_ = customers;
        }

        public async Task<IActionResult> Index()
        {
            var t = await context_
                .Set<Customer>()
                .Take(100)
                .ToListAsync();

            return View(t);
        }

        public IActionResult List()
        {
            var customerList = context_
                .Set<Customer>()
                .Select(c => new { c.Email, c.VatNumber })
                .Take(100)
                .ToListAsync();

            return Json(customerList);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(
            Models.CreateCustomerViewModel model)
        {
            var result = await customers_.CreateCustomerAsync(
                model?.CreateOptions);

            if (result == null) {
                model.ErrorText = "Oops. Something went wrong";

                return View(model);
            }

            return Ok();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TinyCrm.Core.Data;
using TinyCrm.Core.Model;
using TinyCrm.Web.Extensions;

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

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> SearchCustomers(
            string email, string vatNumber)
        {
            if (string.IsNullOrWhiteSpace(email)) {
                return BadRequest("Mail is required");
            }

            var customerList = await customers_.
                SearchCustomers(
                new Core.Model.Options.SearchCustomerOptions() {
                    VatNumber = vatNumber,
                    Email = email
                })
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

        [HttpPost]
        public async Task<IActionResult> CreateCustomer(
           [FromBody] Core.Model.Options.CreateCustomerOptions options)
        {
            var result = await customers_.CreateCustomerAsync(
                options);

            return result.AsStatusResult();
        }
    }
}
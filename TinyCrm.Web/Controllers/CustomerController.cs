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
    public class CustomerController : Controller
    {
        private IContainer Container { get; set; }
        private TinyCrmDbContext Context { get; set; }
        private Core.Services.ICustomerService customers_;

        public CustomerController()
        {
            Container = TinyCrm.Core.ServiceRegistrator.GetContainer();
            Context = Container.Resolve<TinyCrmDbContext>();
            customers_ = Container.Resolve<Core.Services.ICustomerService>();
        }

        public IActionResult Index()
        {
            var customerList = Context
                .Set<Customer>()
                .Take(100)
                .ToList();

            return View(customerList);
        }

        public IActionResult List()
        {
            var customerList = Context
                .Set<Customer>()
                .Select(c => new { c.Email, c.VatNumber })
                .Take(100)
                .ToList();

            return Json(customerList);
        }



        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(
            Models.CreateCustomerViewModel model)
        {
            var result = customers_.CreateCustomer(
                model?.CreateOptions);

            if (result == null) {
                model.ErrorText = "Oops. Something went wrong";

                return View(model);
            }

            return Ok();
        }
    }
}
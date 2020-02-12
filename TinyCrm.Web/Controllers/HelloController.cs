using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace TinyCrm.Web.Controllers
{
    public class HelloController : Controller
    {
        public IActionResult SayHello()
        {
            var model = new Models.NameViewModel()
            {
                Name = "Dimitris"
            };

            return View();
        }
    }
}
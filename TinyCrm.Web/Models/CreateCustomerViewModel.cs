using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TinyCrm.Web.Models
{
    public class CreateCustomerViewModel
    {
        public Core.Model.Options.CreateCustomerOptions CreateOptions { get; set; }
        public string ErrorText { get; set; }
    }
}

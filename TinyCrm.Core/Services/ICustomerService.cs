using System.Collections.Generic;
using System.Linq;
using TinyCrm.Core.Model;

namespace TinyCrm.Core.Services
{
    public interface ICustomerService
    {
       Customer CreateCustomer(
            Model.Options.CreateCustomerOptions options);

        IQueryable<Model.Customer> SearchCustomers(
            Model.Options.SearchCustomerOptions options);

        bool UpdateCustomer(int id,
            Model.Options.UpdateCustomerOptions options);
    }
}

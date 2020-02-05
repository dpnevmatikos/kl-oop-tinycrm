using System.Collections.Generic;

namespace TinyCrm.Core.Services
{
    public interface ICustomerService
    {
        bool CreateCustomer(
            Model.Options.CreateCustomerOptions options);

        ICollection<Model.Customer> SearchCustomers();
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TinyCrm.Core.Model;
using TinyCrm.Core.Model.Options;

namespace TinyCrm.Core.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly Data.TinyCrmDbContext context_;

        public CustomerService(Data.TinyCrmDbContext context)
        {
            context_ = context ??
                throw new ArgumentException(nameof(context));
        }

        public Customer CreateCustomer(CreateCustomerOptions options)
        {
            if (options == null) {
                return null;
            }

            if (string.IsNullOrWhiteSpace(options.VatNumber) ||
              string.IsNullOrWhiteSpace(options.Email)) {
                return null;
            }

            if (options.VatNumber.Length > 9) {
                return null;
            }

            var exists = SearchCustomers(
                new SearchCustomerOptions()
                {
                    VatNumber = options.VatNumber
                }).Any();

            if (exists) {
                return null;
            }

            var customer = new Customer()
            {
                VatNumber = options.VatNumber,
                Firstname = options.FirstName,
                Lastname = options.LastName,
                Email = options.Email,
                Phone = options.Phone,
                IsActive = true
                
            };

            context_.Add(customer);
            try {
                context_.SaveChanges();
            }catch (Exception ex) {
                return null;
            }

            return customer;
        }

        public IQueryable<Customer> SearchCustomers(
            SearchCustomerOptions options)
        {
            if (options == null) {
                return null;
            }

            var query = context_
                .Set<Customer>()
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(options.VatNumber)) {
                query = query.Where(c =>
                    c.VatNumber == options.VatNumber);
            }

            if (!string.IsNullOrWhiteSpace(options.Email)) {
                query = query.Where(c =>
                    c.Email == options.Email);
            }

            if (options.CustomerId != null) {
                query = query.Where(c =>
                    c.Id == options.CustomerId.Value);
            }

            return query.Take(500);
        }

        public bool UpdateCustomer(int id, UpdateCustomerOptions options)
        {
            throw new NotImplementedException();
        }
    }
}

using System;

namespace TinyCrm.Core.Model.Options
{
    public class SearchCustomerOptions
    {
        public string VatNumber { get; set; }
        public string Email { get; set; }
        public DateTimeOffset CreatedFrom { get; set; }
        public DateTimeOffset CreatedTo { get; set; }
        public int? CustomerId { get; set; }
    }
}

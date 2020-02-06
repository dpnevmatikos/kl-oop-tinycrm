using System;
using System.Collections.Generic;
using System.Text;
using TinyCrm.Core.Model;

namespace TinyCrm.Core.Services
{
    public interface  IOrderService
    {
        public Order CreateOrder(int customerId,
            ICollection<string> productIds);
    }
}

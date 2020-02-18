using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TinyCrm.Core.Model;

namespace TinyCrm.Core.Services
{
    public interface  IOrderService
    {
        public Task<ApiResult<Order>> CreateOrder(int customerId,
            ICollection<string> productIds);
    }
}

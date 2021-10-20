using Ecommerce.Api.Search.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.Api.Search.Interfaces
{
    public interface IOrderService
    {
        Task<(bool isSuccess, IEnumerable<Order> Orders)> GetOrders(int customerId);
    }
}

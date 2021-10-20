using Ecommerce.Api.Orders.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.Api.Orders.Interfaces
{
    public interface IOrderProvider
    {
        Task<(bool isSuccess, IEnumerable<OrderModel> orderModel, string errorMessage)> GetOrders(int customerId);
    }
}

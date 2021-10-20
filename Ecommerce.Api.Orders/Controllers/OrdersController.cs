using Ecommerce.Api.Orders.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.Api.Orders.Controllers
{
    [ApiController]
    [Route("api/orders")]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderProvider _orderProvider;

        public OrdersController(IOrderProvider orderProvider)
        {
            _orderProvider = orderProvider;
        }
        [HttpGet("{customerId}")]
        public async Task<IActionResult> GetOrders(int customerId)
        {
            var result = await _orderProvider.GetOrders(customerId);
            if (result.isSuccess)
                return Ok(result.orderModel);
            else
                return NotFound();
        }

    }
}

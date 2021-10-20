using Ecommerce.Api.Customers.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.Api.Customers.Controllers
{
    [ApiController]
    [Route("api/customers")]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerProvider _customerProvider;

        public CustomersController(ICustomerProvider customerProvider)
        {
            _customerProvider = customerProvider;
        }
        public async Task<IActionResult> GetCustomersAsync()
        {
            var result = await _customerProvider.GetCustomersAsync();
            if (result.isSuccess)
                return Ok(result.customers);
            return NotFound();
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCustomerAsync(int id)
        {
            var result = await _customerProvider.GetCustomer(id);
            if (result.isSuccess)
                return Ok(result.customer);
            return NotFound();
        }

    }
}

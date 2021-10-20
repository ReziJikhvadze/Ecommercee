using Ecommerce.Api.Products.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.Api.Products.Controllers
{
    [ApiController]
    [Route("api/products")]
    public class ProductsController : ControllerBase
    {
        public ProductsController(IProductsProvider productsProvider)
        {
            _productsProvider = productsProvider;
        }

        private readonly IProductsProvider _productsProvider;

        [HttpGet]
        public async Task<IActionResult> GetAllProductsAsync()
        {
            var result = await _productsProvider.GetProductsAsync();
            if (result.isSuccess)
                return Ok(result.products);
            return NotFound(); 
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProduct(int id)
        {
            var result = await _productsProvider.GetProduct(id);
            if (result.isSuccess)
                return Ok(result.products);
            return NotFound();
        }
    }
}

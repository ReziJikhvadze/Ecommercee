using AutoMapper;
using Ecommerce.Api.Products.Db;
using Ecommerce.Api.Products.Interfaces;
using Ecommerce.Api.Products.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.Api.Products.Providers
{
    public class ProductsProvider : IProductsProvider
    {
        private readonly ProductsDbContext _productsDbContext;
        private readonly ILogger<ProductsProvider> _logger;
        private readonly IMapper _mapper;
        public ProductsProvider(ProductsDbContext productsDbContext, ILogger<ProductsProvider> logger, IMapper mapper)
        {
            _productsDbContext = productsDbContext;
            _logger = logger;
            _mapper = mapper;
            SeedData();
        }

        private void SeedData()
        {
            if (!_productsDbContext.Products.Any())
            {
                _productsDbContext.Products.Add(new Product { Id = 1, Name = "Mouse", Price = 5, Inventory = 100 });
                _productsDbContext.Products.Add(new Product { Id = 2, Name = "KeyBoard", Price = 115, Inventory = 200 });
                _productsDbContext.Products.Add(new Product { Id = 3, Name = "Cpu", Price = 150, Inventory = 300 });
                _productsDbContext.Products.Add(new Product { Id = 4, Name = "MotherBoard", Price = 200, Inventory = 500 });

                _productsDbContext.SaveChanges();
            }
        }

        public async Task<(bool isSuccess, IEnumerable<ProductModel> products, string errorMessage)> GetProductsAsync()
        {
            try
            {
                var _products = await _productsDbContext.Products.ToListAsync();
                if (_productsDbContext.Products != null && _productsDbContext.Products.Any())
                {
                    var result = _mapper.Map<IEnumerable<Product>, IEnumerable<ProductModel>>(_products);
                    return (true, result, "");
                }
                return (false, null, "Not Found");
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex.ToString());
                return (false, null, $"{ex.Message}");
            }
        }

        public async Task<(bool isSuccess, ProductModel products, string errorMessage)> GetProduct(int id)
        {
            try
            {
                var _product = await _productsDbContext.Products.FirstOrDefaultAsync(x => x.Id == id);
                if (_productsDbContext.Products != null && _productsDbContext.Products.Any())
                {
                    var result = _mapper.Map<Product, ProductModel>(_product);
                    return (true, result, "");
                }
                return (false, null, "Not Found");
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex.ToString());
                return (false, null, $"{ex.Message}");
            }
        }
    }
}

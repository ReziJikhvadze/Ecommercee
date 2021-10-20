using AutoMapper;
using Ecommerce.Api.Products.Db;
using Ecommerce.Api.Products.Profiles;
using Ecommerce.Api.Products.Providers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Ecommerce.Api.Products.ProductsServiceTest
{
    public class ProductsServiceTest
    {
        [Fact]
        public async Task GetProductsReturnAllProducts()
        {
            var options = new DbContextOptionsBuilder()
                .UseInMemoryDatabase(nameof(GetProductsReturnAllProducts))
                .Options;

            var context = new ProductsDbContext(options);
            CreateProducts(context);
            var productsProfile = new MappingProfiles();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(productsProfile));
            var mapper = new Mapper(configuration);
            var productServiceProvider = new ProductsProvider(context, null, mapper);
            var product = await productServiceProvider.GetProductsAsync();
            Assert.True(product.isSuccess);
            Assert.True(product.products.Any());
            Assert.Empty(product.errorMessage);

        }
        [Fact]
        public async Task GetProductReturnProduct()
        {
            var options = new DbContextOptionsBuilder()
                .UseInMemoryDatabase(nameof(GetProductReturnProduct))
                .Options;

            var context = new ProductsDbContext(options);
            CreateProducts(context);
            var productsProfile = new MappingProfiles();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(productsProfile));
            var mapper = new Mapper(configuration);
            var productServiceProvider = new ProductsProvider(context, null, mapper);
            var product = await productServiceProvider.GetProduct(1);
            Assert.True(product.isSuccess);
            Assert.NotNull(product.products);
            Assert.Empty(product.errorMessage);

        }
        [Fact]
        public async Task GetProductReturnProduct1()
        {
            var options = new DbContextOptionsBuilder()
                .UseInMemoryDatabase(nameof(GetProductReturnProduct1))
                .Options;

            var context = new ProductsDbContext(options);
            CreateProducts(context);
            var productsProfile = new MappingProfiles();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(productsProfile));
            var mapper = new Mapper(configuration);
            var productServiceProvider = new ProductsProvider(context, null, mapper);
            var product = await productServiceProvider.GetProduct(-1);
            Assert.True(product.isSuccess);
            Assert.Null(product.products);

            //Assert.NotEmpty(product.errorMessage);

        }

        private void CreateProducts(ProductsDbContext context)
        {
            for (int i = 1; i <= 10; i++)
            {
                context.Products.Add(
                    new Product
                    {
                        Id = i,
                        Name = Guid.NewGuid().ToString(),
                        Price = (decimal)(i * 3.14),
                        Inventory = i + 10
                    });
            }
            context.SaveChangesAsync();


        }
    }
}

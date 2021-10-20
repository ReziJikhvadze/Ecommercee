using Ecommerce.Api.Products.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.Api.Products.Interfaces
{
    public interface IProductsProvider
    {
        Task<(bool isSuccess, IEnumerable<ProductModel> products, string errorMessage)> GetProductsAsync();
        Task<(bool isSuccess, ProductModel products, string errorMessage)> GetProduct(int id);
    }
}

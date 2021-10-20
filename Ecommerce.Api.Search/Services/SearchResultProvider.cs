using Ecommerce.Api.Search.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.Api.Search
{
    public class SearchResultProvider : ISearchResultProvider
    {
        private readonly IOrderService _orderService;
        private readonly IProductService _productService;
        private readonly ICustomerService _customerService;

        public SearchResultProvider(IOrderService orderService, IProductService productService, ICustomerService customerService)
        {
            _orderService = orderService;
            _productService = productService;
            _customerService = customerService;
        }
        public async Task<(bool isSuccess, dynamic searchResult)> GetSearchResultAsync(int customerId)
        {
            var orderResult = await _orderService.GetOrders(customerId);
            var products = await _productService.GetProductsAsync();
            var customer = await _customerService.GetCustomerAsync(customerId);

            foreach (var order in orderResult.Orders)
            {
                foreach (var item in order.Items)
                {
                    item.ProductName = products.isSuccess ?
                        products.Item2.FirstOrDefault(x => x.Id == item.ProductId)?.Name
                        : "product isn't avaliable";
                }
            }
            if (orderResult.isSuccess)
            {
                var result = new
                {
                    Customer = customer.isSuccess ? customer.Item2.Name : customer.Item2.Name = "Customer isn't available",
                    Orders = orderResult.Orders
                };
                return (true, result);
            }
            return (false, null);
        }

    }
}

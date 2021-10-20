using Ecommerce.Api.Search.Interfaces;
using Ecommerce.Api.Search.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace Ecommerce.Api.Search.Services
{
    public class OrderService : IOrderService
    {
        private readonly IHttpClientFactory _httpClient;
        private readonly ILogger<OrderService> _logger;

        public OrderService(IHttpClientFactory httpClient, ILogger<OrderService> logger)
        {
            _httpClient = httpClient;
            _logger = logger;

        }
        public async Task<(bool isSuccess, IEnumerable<Order> Orders)> GetOrders(int customerId)
        {
            try
            {
                var client = _httpClient.CreateClient("OrdersService");

                var response = await client.GetAsync($"api/orders/{customerId}");
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsByteArrayAsync();
                    var options = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };
                    var result = JsonSerializer.Deserialize<IEnumerable<Order>>(content, options);
                    //if(response.is)
                    return (true, result);
                }
                return (false, null);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return (false, null);
            }
        }
    }
}

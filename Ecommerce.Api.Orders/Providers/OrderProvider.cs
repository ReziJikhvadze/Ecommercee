using AutoMapper;
using Ecommerce.Api.Orders.Db;
using Ecommerce.Api.Orders.Interfaces;
using Ecommerce.Api.Orders.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.Api.Orders.Providers
{
    public class OrderProvider : IOrderProvider
    {
        private readonly IMapper _mapper;
        private readonly OrderDbContext _dbContext;
        private readonly ILogger<OrderProvider> _logger;

        public OrderProvider(IMapper mapper, OrderDbContext dbContext, ILogger<OrderProvider> logger)
        {
            _mapper = mapper;
            _dbContext = dbContext;
            _logger = logger;
            SeedData();
        }


        private void SeedData()
        {
            if (!_dbContext.Orders.Any()) { 
                _dbContext.Orders.Add(new Order()
                {
                    Id = 1,
                    CustomerId = 1,
                    OrderDate = DateTime.Now,
                    Items = new List<OrderItem>()
                    {
                        new OrderItem() { OrderId = 1, ProductId = 1, Quantity = 10, UnitPrice = 10 },
                        new OrderItem() { OrderId = 1, ProductId = 2, Quantity = 10, UnitPrice = 10 },
                        new OrderItem() { OrderId = 1, ProductId = 3, Quantity = 10, UnitPrice = 10 },
                        new OrderItem() { OrderId = 2, ProductId = 2, Quantity = 10, UnitPrice = 10 },
                        new OrderItem() { OrderId = 3, ProductId = 3, Quantity = 1, UnitPrice = 100 }
                    },
                    Total = 100
                });
            _dbContext.Orders.Add(new Order()
            {
                Id = 2,
                CustomerId = 1,
                OrderDate = DateTime.Now.AddDays(-1),
                Items = new List<OrderItem>()
                    {
                        new OrderItem() { OrderId = 1, ProductId = 1, Quantity = 10, UnitPrice = 10 },
                        new OrderItem() { OrderId = 1, ProductId = 2, Quantity = 10, UnitPrice = 10 },
                        new OrderItem() { OrderId = 1, ProductId = 3, Quantity = 10, UnitPrice = 10 },
                        new OrderItem() { OrderId = 2, ProductId = 2, Quantity = 10, UnitPrice = 10 },
                        new OrderItem() { OrderId = 3, ProductId = 3, Quantity = 1, UnitPrice = 100 }
                    },
                Total = 100
            });
            _dbContext.SaveChanges();
        }

    }

    public async Task<(bool isSuccess, IEnumerable<OrderModel> orderModel, string errorMessage)> GetOrders(int customerId)
    {
        try
        {
            var _orders = await _dbContext.Orders.Where(x => x.CustomerId == customerId).Include(y => y.Items).ToListAsync();

            if (_orders != null && _orders.Any())
            {
                var result = _mapper.Map<IEnumerable<Db.Order>,
                    IEnumerable<Models.OrderModel>>(_orders);
                return (true, result, null);
            }
            return (false, null, "Not Found");
        }
        catch (Exception ex)
        {
            _logger?.LogError(ex.ToString());
            return (false, null, ex.Message);
        }
    }
      
    }
}


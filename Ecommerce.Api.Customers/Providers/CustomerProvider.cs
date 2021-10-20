using AutoMapper;
using Ecommerce.Api.Customers.Db;
using Ecommerce.Api.Customers.Interfaces;
using Ecommerce.Api.Customers.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.Api.Customers.Providers
{
    public class CustomerProvider : ICustomerProvider
    {
        private readonly IMapper _mapper;
        private readonly ILogger<CustomerProvider> _logger;
        private readonly CustomerDbContext _customerDbContext;

        public CustomerProvider(IMapper mapper, ILogger<CustomerProvider> logger, CustomerDbContext customerDbContext)
        {
            _mapper = mapper;
            _logger = logger;
            _customerDbContext = customerDbContext;
            SeedData();
        }
        private void SeedData()
        {
            if (!_customerDbContext.Customers.Any())
            {
                _customerDbContext.Customers.Add(new Customer { Id = 1, Name = "Rezo Jikhvadze", Address = "Tbilisi" });
                _customerDbContext.Customers.Add(new Customer { Id = 2, Name = "Amiran Darsalia", Address = "Samegrelo" });
                _customerDbContext.Customers.Add(new Customer { Id = 3, Name = "Ia Darsalia", Address = "Tbilisi" });
                _customerDbContext.Customers.Add(new Customer { Id = 4, Name = "Khatuna Pakhuridze", Address = "Thessaloniki" });
                _customerDbContext.SaveChanges();
            }
        }

        public async Task<(bool isSuccess, CustomerModel customer, string errorMessage)> GetCustomer(int id)
        {
            try
            {
                var customer = await _customerDbContext.Customers.FirstOrDefaultAsync(x => x.Id == id);
                if (customer != null)
                {
                    var result = _mapper.Map<Customer, CustomerModel>(customer);
                    return (true, result, "");
                }
                _logger?.LogInformation("Customer Not Found");
                return (false, null, "Not Found");
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex.ToString());
                return (false, null, ex.ToString());

            }
        }

        public async Task<(bool isSuccess, IEnumerable<CustomerModel> customers, string errorMessage)> GetCustomersAsync()
        {
            try
            {
                var customers = await _customerDbContext.Customers.ToListAsync();
                if (customers != null && customers.Any())
                {
                    var result = _mapper.Map<IEnumerable<Customer>, IEnumerable<CustomerModel>>(customers);
                    return (true, result, "");
                }
                _logger?.LogInformation("Customer Not Found");
                return (false, null, "Not Found");
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex.ToString());
                return (false, null, ex.ToString());

            }
        }
    }
}

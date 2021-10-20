using Ecommerce.Api.Customers.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.Api.Customers.Interfaces
{
    public interface ICustomerProvider
    {
        Task<(bool isSuccess, IEnumerable<CustomerModel> customers, string errorMessage)> GetCustomersAsync();
        Task<(bool isSuccess, CustomerModel customer, string errorMessage)> GetCustomer(int id);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.Api.Search.Interfaces
{
    public interface ISearchResultProvider
    {
        Task<(bool isSuccess, dynamic searchResult)> GetSearchResultAsync(int customerId);
    }
}

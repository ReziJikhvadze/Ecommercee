using Ecommerce.Api.Search.Interfaces;
using Ecommerce.Api.Search.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.Api.Search.Controllers
{
    [ApiController]
    [Route("api/search")]
    public class SearchController : ControllerBase
    {
        private readonly ISearchResultProvider _searchResult;

        public SearchController(ISearchResultProvider searchResult)
        {
            _searchResult = searchResult;
        }
        [HttpPost]
        public async Task<IActionResult> Search(SearchResultModel searchResult)
        {
            var result = await _searchResult.GetSearchResultAsync(searchResult.CustomerId);
            if (result.isSuccess)
                return Ok(result.searchResult);
            else
                return NotFound();
        }
    }
}

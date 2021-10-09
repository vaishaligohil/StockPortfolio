using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using StockPortfolio.Contracts;
using StockPortfolio.Services;
using StockPortfolioDB.Models;

namespace StockPortfolio.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StockController : ControllerBase
    {
        private readonly IStockService _stockService;

        public StockController(IStockService stockService)
        {
            _stockService = stockService;
        }

        [HttpGet]
        public async Task<IEnumerable<Stock>> Get() =>
            await _stockService.Get();

        [HttpPost]
        public async Task<Stock> Add([FromBody]Stock stock) =>
           await _stockService.Add(stock);

        [HttpPut]
        public async Task<Stock> Update([FromBody]Stock stock) =>
            await _stockService.Update(stock);

        [HttpDelete("{Id}")]
        public async Task<bool> Delete(int Id) =>
           await _stockService.Delete(Id);
    }
}

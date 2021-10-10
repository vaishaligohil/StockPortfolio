using StockPortfolio.Contracts;
using StockPortfolioDB.Models;
using StockPortfolioDB.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StockPortfolio.Services
{
    public interface IStockService
    {
        Task<IEnumerable<Stock>> Get();

        Task<Stock> Add(Stock stock);

        Task<Stock> Update(Stock stock);

        Task<bool> Delete(int Id);
    }

    public class StockService : IStockService
    {
        private readonly IStockRepository _stockRepo;

        public StockService(IStockRepository stockRepo)
        {
            _stockRepo = stockRepo;
        }

        public async Task<Stock> Add(Stock stock)
        {
            // Check if symbol already existing then update the existing record by adding contracts and average the price
            var existing = await _stockRepo.GetAllAsync(new Stock { Symbol = stock.Symbol});

            if (existing != null && existing.Count() > 0)
            {
                stock.StockId = existing.First().StockId;
                stock.BuyPrice = (existing.First().BuyPrice + stock.BuyPrice) / 2;
                stock.Contracts = existing.First().Contracts + stock.Contracts;

               return await _stockRepo.UpdateAsync(stock);
            }
            else
            {
                // Else create new stock entry in the portfolio
                return await _stockRepo.AddAsync(stock);
            }
        }

        public async Task<bool> Delete(int Id)
        {
            return await _stockRepo.DeleteAsync(Id);
        }

        public async Task<IEnumerable<Stock>> Get()
        {
            return await _stockRepo.GetAllAsync();
        }

        public async Task<Stock> Update(Stock stock)
        {
            return await _stockRepo.UpdateAsync(stock);
        }
    }
}

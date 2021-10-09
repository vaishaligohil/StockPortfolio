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
            return await _stockRepo.AddAsync(stock);
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

using Microsoft.EntityFrameworkCore;
using StockPortfolioDB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace StockPortfolioDB.Repositories
{
    public interface IStockRepository
    {
        Task<IEnumerable<Stock>> GetAllAsync(Stock filter = null);

        Task<Stock> AddAsync(Stock stock);

        Task<Stock> UpdateAsync(Stock stock);

        Task<bool> DeleteAsync(int stockId);
    }

    public class StockRepository : IStockRepository
    {
        private readonly IStockContextFactory _stockContextFactory;

        public StockRepository(IStockContextFactory stockContextFactory)
        {
            _stockContextFactory = stockContextFactory;
        }

        public async Task<Stock> AddAsync(Stock stock)
        {
            using (var ctx = _stockContextFactory.Get())
            {
                await ctx.Stock.AddAsync(stock);
                await ctx.SaveChangesAsync();
            }
            return await GetByIdAsync(stock.StockId);
        }

        public async Task<bool> DeleteAsync(int stockId)
        {
            using (var ctx = _stockContextFactory.Get())
            {
                var stock = await ctx.Stock.SingleOrDefaultAsync(x => x.StockId == stockId);

                if (stock == null)
                    return false;

                ctx.Stock.Remove(stock);

                return await ctx.SaveChangesAsync() > 0;
            }
        }

        public async Task<IEnumerable<Stock>> GetAllAsync(Stock filter = null)
        {
            using (var ctx = _stockContextFactory.ReadOnly())
            {
                IQueryable<Stock> query = ctx.Stock;

                if (filter != null && filter.Symbol != "")
                {
                    query = query.Where(p => p.Symbol == filter.Symbol);
                }

                return await query.ToListAsync();
            }
        }

        public async Task<Stock> UpdateAsync(Stock stock)
        {
            using (var ctx = _stockContextFactory.Get())
            {
                var existing = await ctx.Stock.SingleOrDefaultAsync(x => x.StockId == stock.StockId);

                if (existing == null)
                {
                    throw new ApiHttpException(HttpStatusCode.NotFound, "NOT_FOUND", "Stock not found");
                }

                ctx.Entry(existing).CurrentValues.SetValues(stock);
                await ctx.SaveChangesAsync();
            }

            return await GetByIdAsync(stock.StockId);
        }

        #region Private Helper Methods
        private async Task<Stock> GetByIdAsync(int stockId)
        {
            using (var ctx = _stockContextFactory.ReadOnly())
            {
                return await ctx.Stock.SingleOrDefaultAsync(s => s.StockId == stockId);
            }
        }
        #endregion
    }
}

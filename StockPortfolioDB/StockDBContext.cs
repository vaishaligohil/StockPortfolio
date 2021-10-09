using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Infrastructure;
using StockPortfolioDB.Models;
using StockPortfolioDB.Configurations;

namespace StockPortfolioDB
{
    public interface IStockContextFactory
    {
        StockDBContext Get();
        StockDBContext ReadOnly();
    }

    public class StockContextFactory : IStockContextFactory
    {
        private readonly DbContextOptions<StockDBContext> _options;

        public StockContextFactory(DbContextOptions<StockDBContext> options)
        {
            _options = options;
        }

        public StockDBContext Get() => new StockDBContext(_options);

        public StockDBContext ReadOnly()
        {
            var ctx = new StockDBContext(_options);

            ctx.ChangeTracker.LazyLoadingEnabled = false;
            ctx.ChangeTracker.AutoDetectChangesEnabled = false;
            ctx.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;

            return ctx;
        }
    }

    public partial class StockDBContext : DbContext
    {
        private static readonly bool _lazyLoadingEnabled = false;

        public StockDBContext(DbContextOptions<StockDBContext> options)
            : base(options)
        {
            base.ChangeTracker.LazyLoadingEnabled = _lazyLoadingEnabled;
        }

        #region DbSet
        public virtual DbSet<Stock> Stock { get; set; }
        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new StockConfiguration());
        }
    }

    public static class DbInitializer
    {
        public static async Task InitializeAsync(DatabaseFacade database, bool runMigrations)
        {
            if (runMigrations)
            {
                await database.MigrateAsync();
            }
        }
    }
}

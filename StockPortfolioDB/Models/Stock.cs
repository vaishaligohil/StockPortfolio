using System;
using System.Collections.Generic;
using System.Text;

namespace StockPortfolioDB.Models
{
    public partial class Stock
    {
        public int StockId { get; set; }
        public string Symbol { get; set; }
        public int Contracts { get; set; }
        public decimal BuyPrice { get; set; }
    }
}

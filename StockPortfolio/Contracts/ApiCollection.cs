using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StockPortfolio.Contracts
{
    public class ApiCollection<T>
    {
        public ApiCollection()
        {
            Items = new List<T>();
        }

        public ApiCollection(List<T> list, long totalHits)
        {
            Items = list ?? new List<T>();
            TotalHits = totalHits;
        }

        public List<T> Items { get; set; }

        public long TotalHits { get; set; }
    }
}

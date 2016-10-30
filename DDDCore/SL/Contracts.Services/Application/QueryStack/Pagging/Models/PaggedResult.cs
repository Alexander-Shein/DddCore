using System.Collections.Generic;

namespace Contracts.Services.Application.QueryStack.Pagging.Models
{
    public class PaggedResult<T>
    {
        public PaggedResult(int page, int pageSize, IEnumerable<T> items, long total)
        {
            Page = page;
            PageSize = pageSize;
            Items = items;
            Total = total;
        }

        public long Total { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }

        public IEnumerable<T> Items { get; set; }
    }
}

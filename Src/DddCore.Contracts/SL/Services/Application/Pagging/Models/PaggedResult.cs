using System.Collections.Generic;
using DddCore.Contracts.SL.Services.Application.RestFull;

namespace DddCore.Contracts.SL.Services.Application.Pagging.Models
{
    public class PaggedResult<T> : IViewModel
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

        public Links Links { get; set; } = new Links();
        public Extends Extends { get; set; } = new Extends();
    }
}
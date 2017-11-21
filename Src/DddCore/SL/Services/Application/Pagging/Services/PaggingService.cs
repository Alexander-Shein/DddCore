using DddCore.Contracts.SL.Services.Application.Pagging.Services;

namespace DddCore.SL.Services.Application.Pagging.Services
{
    public class PaggingService : IPaggingService
    {
        #region Public Methods

        public int NormalizePage(int page, int firstPageNumber = 1)
        {
            if (page < firstPageNumber)
            {
                page = firstPageNumber;
            }

            return page;
        }

        public int NormalizePageSize(int pageSize, int minPageSize = 10, int maxPageSize = 1000)
        {
            if (pageSize < minPageSize)
            {
                pageSize = minPageSize;
            }

            if (pageSize > maxPageSize)
            {
                pageSize = maxPageSize;
            }

            return pageSize;
        }

        #endregion
    }
}
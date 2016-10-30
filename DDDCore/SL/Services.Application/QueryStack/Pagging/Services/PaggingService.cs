using Contracts.Services.Application.QueryStack.Pagging.Services;

namespace Services.Application.QueryStack.Pagging.Services
{
    public class PaggingService : IPaggingService
    {
        #region Public Methods

        public int MormalizePage(int page)
        {
            if (page <= 0)
            {
                page = 1;
            }

            return page;
        }

        public int NormalizePageSize(int pageSize)
        {
            if (pageSize <= 0)
            {
                pageSize = 10;
            }

            if (pageSize > 1000)
            {
                pageSize = 1000;
            }

            return pageSize;
        }

        #endregion
    }
}
using DddCore.Contracts.SL.Services.Infrastructure;

namespace DddCore.Contracts.SL.Services.Application.QueryStack.Pagging.Services
{
    public interface IPaggingService : IInfrastructureService
    {
        /// <summary>
        /// If <param name="page"></param> less than <param name="firstPageNumber"></param> returns <param name="firstPageNumber"></param>.
        /// Else returns <param name="page"></param>
        /// </summary>
        /// <param name="page"></param>
        /// <param name="firstPageNumber"></param>
        /// <returns></returns>
        int NormalizePage(int page, int firstPageNumber = 1);

        /// <summary>
        /// If <param name="pageSize"></param> less than <param name="minPageSize"></param> returns <param name="minPageSize"></param>.
        /// Else if <param name="pageSize"></param> greater than <param name="maxPageSize"></param> returns <param name="maxPageSize"></param>.
        /// Else returns <param name="pageSize"></param>.
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="minPageSize"></param>
        /// <param name="maxPageSize"></param>
        /// <returns></returns>
        int NormalizePageSize(int pageSize, int minPageSize = 10, int maxPageSize = 1000);
    }
}
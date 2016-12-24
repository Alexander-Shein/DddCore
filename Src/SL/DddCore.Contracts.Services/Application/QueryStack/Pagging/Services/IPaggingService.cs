using DddCore.Contracts.Services.Infrastructure;

namespace DddCore.Contracts.Services.Application.QueryStack.Pagging.Services
{
    public interface IPaggingService : IInfrastructureService
    {
        int MormalizePage(int page);
        int NormalizePageSize(int pageSize);
    }
}
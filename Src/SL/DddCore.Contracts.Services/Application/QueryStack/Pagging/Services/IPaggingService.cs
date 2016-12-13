namespace DddCore.Contracts.Services.Application.QueryStack.Pagging.Services
{
    public interface IPaggingService
    {
        int MormalizePage(int page);
        int NormalizePageSize(int pageSize);
    }
}
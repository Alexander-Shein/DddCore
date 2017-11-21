using System;
using System.Threading.Tasks;
using DddCore.Contracts.BLL.Domain.Services;

namespace DddCore.Contracts.SL.Services.Infrastructure.RetryInvocation
{
    public interface ISafeRetry
    {
        Result<T> SafeCall<T>(Func<T> func, int retryCount = 3);
        Result SafeCall(Action action, int retryCount = 3);

        Task<Result<T>> SafeCallAsync<T>(Func<Task<T>> func, int retryCount = 3);
        Task<Result> SafeCallAsync(Func<Task> action, int retryCount = 3);
    }
}

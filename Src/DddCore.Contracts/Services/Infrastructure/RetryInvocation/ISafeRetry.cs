using System;
using System.Threading.Tasks;
using DddCore.Contracts.Domain.Errors;

namespace DddCore.Contracts.Services.Infrastructure.RetryInvocation
{
    public interface ISafeRetry
    {
        (T result, OperationResult operationResult) SafeCall<T>(Func<T> func, int retryCount = 3);
        OperationResult SafeCall(Action action, int retryCount = 3);

        Task<(T result, OperationResult operationResult)> SafeCallAsync<T>(Func<Task<T>> func, int retryCount = 3);
        Task<OperationResult> SafeCallAsync(Func<Task> action, int retryCount = 3);
    }
}

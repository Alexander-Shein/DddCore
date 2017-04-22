using System;
using System.Threading.Tasks;

namespace DddCore.Contracts.Services.Infrastructure.RetryInvocation
{
    public interface ISafeRetry
    {
        T SafeCall<T>(Func<T> func, int retryCount = 3);
        void SafeCall(Action action, int retryCount = 3);

        Task<T> SafeCallAsync<T>(Func<Task<T>> func, int retryCount = 3);
        Task SafeCallAsync(Func<Task> action, int retryCount = 3);
    }
}

using System;

namespace DddCore.Contracts.Services.Infrastructure.RetryInvocation
{
    public interface ISafeRetry
    {
        T SafeCall<T>(Func<T> func, int retryCount = 3);
        void SafeCall(Action action, int retryCount = 3);
    }
}

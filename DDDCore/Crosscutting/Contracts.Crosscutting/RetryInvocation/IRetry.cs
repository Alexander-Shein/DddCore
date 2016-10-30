using System;

namespace Contracts.Crosscutting.RetryInvocation
{
    public interface IRetry
    {
        T SafeCall<T>(Func<T> func, int retryCount = 3) where T : class;
        void SafeCall(Action action, int retryCount = 3);
    }
}

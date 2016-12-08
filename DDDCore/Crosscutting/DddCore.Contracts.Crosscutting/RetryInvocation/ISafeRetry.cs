using System;

namespace DddCore.Contracts.Crosscutting.RetryInvocation
{
    public interface ISafeRetry
    {
        T SafeCall<T>(Func<T> func, int retryCount = 3) where T : class;
        void SafeCall(Action action, int retryCount = 3);
    }
}

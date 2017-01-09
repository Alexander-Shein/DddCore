using System;
using DddCore.Contracts.Crosscutting.RetryInvocation;
using DddCore.Contracts.Services.Infrastructure.RetryInvocation;

namespace DddCore.Crosscutting.RetryInvocation
{
    public class SafeRetry : ISafeRetry
    {
        #region Public Methods

        public T SafeCall<T>(Func<T> func, int retryCount = 3) where T : class
        {
            var i = 0;

            while (true)
            {
                try
                {
                    var result = func();
                    return result;
                }
                catch (Exception)
                {
                    i++;
                    if (i > retryCount)
                    {
                        throw;
                    }
                }
            }
        }

        public void SafeCall(Action action, int retryCount = 3)
        {
            var i = 0;

            while (true)
            {
                try
                {
                    action();
                }
                catch (Exception)
                {
                    i++;
                    if (i > retryCount)
                    {
                        throw;
                    }
                }
            }
        }

        #endregion
    }
}

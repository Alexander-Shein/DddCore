using System;
using Contracts.Crosscutting.RetryInvocation;

namespace Crosscutting.Infrastructure.RetryInvocation
{
    public class Retry : IRetry
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

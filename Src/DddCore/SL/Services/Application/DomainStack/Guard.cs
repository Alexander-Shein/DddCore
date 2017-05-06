using System;
using DddCore.Contracts.SL.Services.Application.DomainStack;

namespace DddCore.SL.Services.Application.DomainStack
{
    public class Guard : IGuard
    {
        #region Public Methods

        public void NotNull(object obj, string message = "")
        {
            if (obj == null)
                throw new ArgumentNullException(message);
        }

        #endregion
    }
}
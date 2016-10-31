using System;
using System.Linq;
using System.Threading.Tasks;
using Contracts.Domain.Entities.BusinessRules;
using Contracts.Services.Application.DomainStack;

namespace Services.Application.DomainStack
{
    public class Guard : IGuard
    {
        #region Public Methods

        public void NotNull(object obj, string message = "")
        {
            if (obj == null)
                throw new ArgumentNullException(message);
        }

        public async Task DomainIsValidAsync(params IValidatable[] domains)
        {
            foreach (var domain in domains)
            {
                var validationResult = await domain.ValidateAsync();

                if (validationResult.IsNotValid)
                {
                    throw validationResult.Errors.First();
                }
            }
        }

        #endregion
    }
}
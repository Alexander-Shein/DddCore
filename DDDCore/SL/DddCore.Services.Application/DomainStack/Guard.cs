using System;
using System.Linq;
using System.Threading.Tasks;
using DddCore.Contracts.Domain.Entities.BusinessRules;
using DddCore.Contracts.Services.Application.DomainStack;

namespace DddCore.Services.Application.DomainStack
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
                    throw new ArgumentException(String.Join($"{Environment.NewLine}", validationResult.Errors.Select(x => x.Description)));
                }
            }
        }

        #endregion
    }
}
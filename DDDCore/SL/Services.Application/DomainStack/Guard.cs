using System;
using System.Linq;
using System.Threading.Tasks;
using Contracts.Domain.Entities.Validation;
using Contracts.Services.Application.DomainStack;

namespace Services.Application.DomainStack
{
    public class Guard : IGuard
    {
        public void NotNull(object obj)
        {
            if (obj == null)
                throw new ArgumentNullException();
        }

        public void DomainIsValid(params IValidation[] domains)
        {
            foreach (var domain in domains)
            {
                if (domain.IsNotValid)
                {
                    throw new ArgumentException(String.Join(Environment.NewLine, domain.ValidationErrors.Select(x => x.Message)));
                }
            }
        }

        public async Task DomainIsValidAsync(params IValidation[] domains)
        {
            foreach (var domain in domains)
            {
                if (await domain.IsNotValidAsync())
                {
                    throw new ArgumentException(String.Join(Environment.NewLine, domain.ValidationErrors.Select(x => x.Message)));
                }
            }
        }
    }
}
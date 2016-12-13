using System;
using System.Linq;
using System.Threading.Tasks;
using DddCore.Contracts.Domain.Entities;
using DddCore.Contracts.Domain.Entities.BusinessRules;
using DddCore.Contracts.Services.Application.DomainStack;

namespace DddCore.Services.Application.DomainStack
{
    public class Guard : IGuard
    {
        #region Private Members

        readonly IBusinessRulesValidatorFactory businessRulesValidatorFactory;

        public Guard(IBusinessRulesValidatorFactory businessRulesValidatorFactory)
        {
            this.businessRulesValidatorFactory = businessRulesValidatorFactory;
        }

        #endregion

        #region Public Methods

        public void NotNull(object obj, string message = "")
        {
            if (obj == null)
                throw new ArgumentNullException(message);
        }

        public async Task AggregateRootIsValidAsync<T, TKey>(T aggregateRoot) where T : IAggregateRootEntity<TKey>
        {
            var businessRulesValidator = businessRulesValidatorFactory.GetBusinessRulesValidator<T>();
            var validationResult = await businessRulesValidator.ValidateAsync(aggregateRoot);

            if (validationResult.IsNotValid)
            {
                throw new ArgumentException(String.Join($"{Environment.NewLine}", validationResult.Errors.Select(x => x.Description)));
            }
        }

        public void AggregateRootIsValid<T, TKey>(T aggregateRoot) where T : IAggregateRootEntity<TKey>
        {
            var businessRulesValidator = businessRulesValidatorFactory.GetBusinessRulesValidator<T>();
            var validationResult = businessRulesValidator.Validate(aggregateRoot);

            if (validationResult.IsNotValid)
            {
                throw new ArgumentException(String.Join($"{Environment.NewLine}", validationResult.Errors.Select(x => x.Description)));
            }
        }

        #endregion
    }
}
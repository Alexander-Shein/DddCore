using System;
using System.Threading.Tasks;
using DddCore.Contracts.BLL.Domain.Entities;
using DddCore.Contracts.BLL.Domain.Entities.BusinessRules;
using DddCore.Contracts.BLL.Errors;
using DddCore.Contracts.SL.Services.Application.DomainStack;

namespace DddCore.SL.Services.Application.DomainStack
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

        public async Task<OperationResult> ValidateBusinessRulesAsync<T, TKey>(T aggregateRoot) where T : IAggregateRootEntity<TKey>
        {
            var businessRulesValidator = businessRulesValidatorFactory.GetBusinessRulesValidator<T>();
            var validationResult = await businessRulesValidator.ValidateAsync(aggregateRoot);

            return validationResult;
        }

        public OperationResult ValidateBusinessRules<T, TKey>(T aggregateRoot) where T : IAggregateRootEntity<TKey>
        {
            var businessRulesValidator = businessRulesValidatorFactory.GetBusinessRulesValidator<T>();
            var validationResult = businessRulesValidator.Validate(aggregateRoot);

            return validationResult;
        }

        #endregion
    }
}
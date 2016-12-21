using System;
using DddCore.Contracts.Domain.Entities.BusinessRules;
using DddCore.Contracts.Domain.Entities.Model;
using DddCore.Crosscutting.DependencyInjection;

namespace DddCore.Domain.Entities.BusinessRules
{
    public class BusinessRulesValidatorFactory : IBusinessRulesValidatorFactory
    {
        #region Private Members

        readonly IServiceProvider serviceProvider;

        #endregion

        public BusinessRulesValidatorFactory(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        #region Public Methods

        public IBusinessRulesValidator<T> GetBusinessRulesValidator<T>() where T : ICrudState
        {
            return serviceProvider.GetService<IBusinessRulesValidator<T>>();
        }

        #endregion
    }
}
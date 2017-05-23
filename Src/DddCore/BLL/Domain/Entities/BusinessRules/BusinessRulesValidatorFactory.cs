using System;
using DddCore.Contracts.BLL.Domain.Entities.BusinessRules;
using DddCore.Contracts.BLL.Domain.Entities.State;
using Microsoft.Extensions.DependencyInjection;

namespace DddCore.BLL.Domain.Entities.BusinessRules
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

        public IBusinessRulesValidator<T> GetBusinessRulesValidator<T>(T instance) where T : ICrudState
        {
            return GetBusinessRulesValidator<T>();
        }

        #endregion
    }
}
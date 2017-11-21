using System;
using DddCore.Contracts.BLL.Domain.BusinessRules;
using Microsoft.Extensions.DependencyInjection;

namespace DddCore.BLL.Domain.Models.BusinessRules
{
    public class BusinessRulesValidatorFactory : IBusinessRulesValidatorFactory
    {
        #region Private Members

        readonly IServiceProvider _serviceProvider;

        #endregion

        public BusinessRulesValidatorFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        #region Public Methods

        public IBusinessRulesValidator<T> GetBusinessRulesValidator<T>()
        {
            return _serviceProvider.GetService<IBusinessRulesValidator<T>>();
        }

        public IBusinessRulesValidator<T> GetBusinessRulesValidator<T>(T instance)
        {
            return GetBusinessRulesValidator<T>();
        }

        #endregion
    }
}
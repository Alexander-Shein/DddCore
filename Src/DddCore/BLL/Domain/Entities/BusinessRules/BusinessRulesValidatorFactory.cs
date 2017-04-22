using System;
using DddCore.Contracts.BLL.Domain.Entities.BusinessRules;
using DddCore.Contracts.BLL.Domain.Entities.Model;
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

        #endregion
    }
}
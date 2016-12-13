using DddCore.Contracts.Crosscutting.Ioc;
using DddCore.Contracts.Domain.Entities.BusinessRules;
using DddCore.Contracts.Domain.Entities.Model;

namespace DddCore.Domain.Entities.BusinessRules
{
    public class BusinessRulesValidatorFactory : IBusinessRulesValidatorFactory
    {
        #region Private Members

        readonly IContainer container;

        #endregion

        public BusinessRulesValidatorFactory(IContainer container)
        {
            this.container = container;
        }

        #region Public Methods

        public IBusinessRulesValidator<T> GetBusinessRulesValidator<T>() where T : ICrudState
        {
            return container.Resolve<IBusinessRulesValidator<T>>();
        }

        #endregion
    }
}
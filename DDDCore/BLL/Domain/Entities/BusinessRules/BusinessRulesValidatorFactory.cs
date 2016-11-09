using Contracts.Crosscutting.Ioc;
using Contracts.Domain.Entities.BusinessRules;
using Contracts.Domain.Entities.Model;

namespace Domain.Entities.BusinessRules
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

        public IBusinessRulesValidator<T> GetValidator<T>() where T : ICrudState
        {
            return container.Resolve<IBusinessRulesValidator<T>>();
        }

        #endregion
    }
}
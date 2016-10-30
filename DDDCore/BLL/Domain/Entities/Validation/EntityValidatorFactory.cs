using Contracts.Crosscutting.IoC;
using Contracts.Domain.Entities.Model;
using Contracts.Domain.Entities.Validation;

namespace Domain.Entities.Validation
{
    public class EntityValidatorFactory : IEntityValidatorFactory
    {
        #region Private Members

        readonly IContainer container;

        #endregion

        public EntityValidatorFactory(IContainer container)
        {
            this.container = container;
        }

        #region Public Methods

        public IEntityValidator<T> GetEntityValidator<T>() where T : ICrudState
        {
            return container.Resolve<IEntityValidator<T>>();
        }

        #endregion
    }
}
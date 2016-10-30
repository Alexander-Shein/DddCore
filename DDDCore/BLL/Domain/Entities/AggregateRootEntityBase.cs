using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;
using Contracts.Domain.Entities;
using Contracts.Domain.Entities.Validation;
using Contracts.Domain.Events;
using Crosscutting.IoC;

namespace Domain.Entities
{
    public abstract class AggregateRootEntityBase<TKey> : EntityBase<TKey>, IAggregateRootEntity<TKey>
    {
        #region Private Members

        EntityValidationResult entityValidationResult;

        #endregion

        #region Public Methods

        public string PublicKey { get; set; }

        public ICollection<IDomainEvent> Events { get; protected set; }

        public byte[] Ts { get; set; }

        public async Task<EntityValidationResult> ValidateAsync()
        {
            if (entityValidationResult == null)
            {
                var validator = GetEntityValidator();
                MethodInfo method = typeof(IEntityValidator<>).GetMethod("ValidateAsync");

                entityValidationResult = await (Task<EntityValidationResult>)method.Invoke(validator, new object[] { this });
            }

            return entityValidationResult;
        }

        #endregion

        #region Protected Methods

        protected virtual object GetEntityValidator()
        {
            var factory = ContainerHolder.Container.Resolve<IEntityValidatorFactory>();

            MethodInfo method = typeof(IEntityValidatorFactory).GetMethod("GetEntityValidator");
            
            var type = GetType();
            
            MethodInfo genericMethod = method.MakeGenericMethod(type);
            var validator = genericMethod.Invoke(factory, null);
            return validator;
        }

        #endregion
    }
}
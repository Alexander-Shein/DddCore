using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;
using Contracts.Domain.Entities;
using Contracts.Domain.Entities.BusinessRules;
using Contracts.Domain.Events;
using Crosscutting.IoC;

namespace Domain.Entities
{
    public abstract class AggregateRootEntityBase<TKey> : EntityBase<TKey>, IAggregateRootEntity<TKey>
    {
        #region Private Members

        BusinessRulesValidationResult entityValidationResult;

        #endregion

        #region Public Methods

        public string PublicKey { get; set; }

        public ICollection<IDomainEvent> Events { get; protected set; }

        public byte[] Ts { get; set; }

        public async Task<BusinessRulesValidationResult> ValidateAsync()
        {
            if (entityValidationResult == null)
            {
                var validator = GetEntityValidator();
                MethodInfo method = typeof(IBusinessRulesValidator<>).GetMethod("ValidateAsync");

                entityValidationResult = await (Task<BusinessRulesValidationResult>)method.Invoke(validator, new object[] { this });
            }

            return entityValidationResult;
        }

        #endregion

        #region Protected Methods

        protected virtual object GetEntityValidator()
        {
            var factory = ContainerHolder.Container.Resolve<IBusinessRulesValidatorFactory>();

            MethodInfo method = typeof(IBusinessRulesValidatorFactory).GetMethod("GetValidator");
            
            var type = GetType();
            
            MethodInfo genericMethod = method.MakeGenericMethod(type);
            var validator = genericMethod.Invoke(factory, null);
            return validator;
        }

        #endregion
    }
}
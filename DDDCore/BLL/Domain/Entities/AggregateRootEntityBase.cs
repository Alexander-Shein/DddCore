using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Contracts.Domain.Entities;
using Contracts.Domain.Entities.BusinessRules;
using Contracts.Domain.Entities.Model;
using Contracts.Domain.Events;
using Crosscutting.Ioc;

namespace Domain.Entities
{
    public abstract class AggregateRootEntityBase<TKey> : EntityBase<TKey>, IAggregateRootEntity<TKey>
    {
        #region Private Members

        BusinessRulesValidationResult entityValidationResult;

        #endregion

        #region Public Methods

        public string PublicKey { get; set; }

        public ICollection<IDomainEvent> Events { get; protected set; } = new List<IDomainEvent>();

        public byte[] Ts { get; set; }

        public async Task<BusinessRulesValidationResult> ValidateAsync()
        {
            if (entityValidationResult == null)
            {
                var validator = GetBusinessRules();
                var type = typeof (IBusinessRulesValidator<>).MakeGenericType(GetType());

                MethodInfo method = type.GetMethod("ValidateAsync");

                entityValidationResult = await (Task<BusinessRulesValidationResult>)method.Invoke(validator, new object[] { this });
            }

            return entityValidationResult;
        }

        public BusinessRulesValidationResult Validate()
        {
            if (entityValidationResult == null)
            {
                var validator = GetBusinessRules();
                var type = typeof(IBusinessRulesValidator<>).MakeGenericType(GetType());

                MethodInfo method = type.GetMethod("Validate");

                entityValidationResult = (BusinessRulesValidationResult)method.Invoke(validator, new object[] { this });
            }

            return entityValidationResult;
        }

        public void WalkEntireGraph(Action<ICrudState> action)
        {
            WalkObjectGraph(this, action);
        }

        public void WalkAggregateRootGraph(Action<ICrudState> action)
        {
            WalkObjectGraph(this, action, null, true);
        }

        #endregion

        #region Protected Methods

        protected virtual object GetBusinessRules()
        {
            var factory =
                ContainerHolder
                    .Container
                    .Resolve<IBusinessRulesValidatorFactory>();

            MethodInfo method = typeof(IBusinessRulesValidatorFactory).GetMethod("GetValidator");
            
            var type = GetType();
            
            MethodInfo genericMethod = method.MakeGenericMethod(type);
            var validator = genericMethod.Invoke(factory, null);
            return validator;
        }

        #endregion

        #region Private Methods

        void WalkObjectGraph<TEntity>(TEntity entity, Action<ICrudState> action, HashSet<object> hashSet = null, bool currentAggregateRootOnly = false) where TEntity : class, ICrudState
        {
            if (hashSet == null)
            {
                hashSet = new HashSet<object>();
            }
            else if (currentAggregateRootOnly)
            {
                if (entity is IAggregateRootEntity<TKey>)
                {
                    hashSet.Add(entity);
                    return;
                }
            }

            if (!hashSet.Add(entity)) return;

            var type = entity.GetType();

            // Set tracking state for child collections
            foreach (
                var prop in
                    type.GetProperties()
                        .Where(p => !typeof(MulticastDelegate).IsAssignableFrom(p.PropertyType.BaseType))) //TODO use caching
            {
                var propValue = prop.GetValue(entity, null);

                // Apply changes to 1-1 and M-1 properties
                var trackableRef = propValue as ICrudState;//TODO :use propertyfactory

                if (trackableRef != null)
                {
                    WalkObjectGraph(trackableRef, action, hashSet);
                    continue;
                }

                // Apply changes to 1-M properties
                var items = propValue as IEnumerable<ICrudState>;//TODO :use propertyfactory
                if (items == null) continue;

                foreach (var item in items.ToList())
                {
                    WalkObjectGraph(item, action, hashSet); //TODO set depth level
                }
            }

            action(entity);
        }

        #endregion
    }
}
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using DddCore.Contracts.Domain.Entities;

namespace DddCore.Domain.Entities
{
    public static class EntityGraphHelper
    {
        #region Private Members

        static readonly IDictionary<Type, object> PropertyGettersContainer = new Dictionary<Type, object>();

        #endregion

        public static IEnumerable<Expression<Func<T, object>>> GetPropertiesWithEntities<T, TKey>(bool excludeAggregateRoots = false) where T : IEntity<TKey>
        {
            var currentType = typeof(T);

            Object propertyGetters;

            if (PropertyGettersContainer.TryGetValue(currentType, out propertyGetters))
            {
                return (IEnumerable<Expression<Func<T, object>>>) propertyGetters;
            }

            var result = new List<Expression<Func<T, object>>>();
            var aggregateRootType = typeof(IAggregateRootEntity<TKey>);
            var entityType = typeof(IEntity<TKey>);

            foreach (var propertyInfo in currentType.GetProperties())
            {
                var propertyType = GetPropertyType(propertyInfo);

                if (!entityType.IsAssignableFrom(propertyType)) continue;
                if (excludeAggregateRoots && aggregateRootType.IsAssignableFrom(propertyType)) continue;

                var lambda = CreatePropertyGetter<T>(currentType, propertyInfo);
                result.Add(lambda);
            }

            var readOnlyResult = result.AsReadOnly();
            PropertyGettersContainer.Add(currentType, readOnlyResult);

            return readOnlyResult;
        }

        #region Private Methods

        static Expression<Func<T, object>> CreatePropertyGetter<T>(Type type, PropertyInfo propertyInfo)
        {
            var parameter = Expression.Parameter(type);
            var property = Expression.Property(parameter, propertyInfo);
            var conversion = Expression.Convert(property, typeof(object));
            var lambda = Expression.Lambda<Func<T, object>>(conversion, parameter);

            return lambda;
        }

        static Type GetPropertyType(PropertyInfo propertyInfo)
        {
            var propertyType = propertyInfo.PropertyType;

            Type enumerableType;
            if (TryGetEnumerableGenericType(propertyType, out enumerableType))
            {
                propertyType = enumerableType;
            }

            return propertyType;
        }

        static bool TryGetEnumerableGenericType(Type type, out Type enumerableType)
        {
            enumerableType = null;
            var genericEnumerableType = typeof(IEnumerable<>);

            foreach (Type intType in type.GetInterfaces())
            {
                if (intType.GetTypeInfo().IsGenericType
                    && intType.GetGenericTypeDefinition() == genericEnumerableType)
                {
                    enumerableType = intType.GetGenericArguments()[0];
                    return true;
                }
            }
            return false;
        }

        #endregion
    }
}

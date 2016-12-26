using System.Collections.Generic;
using System.Reflection;
using Microsoft.Extensions.DependencyModel;
using System.Linq;
using System;

namespace DddCore.Crosscutting
{
    public static class AssemblyUtility
    {
        public static IEnumerable<T> GetInstances<T>()
        {
            var assignType = typeof(T);

            return
                GetTypes<T>()
                    .Select(type => (T)Activator.CreateInstance(type));
        }

        public static IEnumerable<Type> GetTypes<T>()
        {
            var assignType = typeof(T);
            return GetTypes(assignType);
        }

        public static IEnumerable<Type> GetTypes(Type assignType)
        {
            return
                GetAllTypes()
                    .Where(x => IsAssignableFrom(x, assignType));
        }

        #region Private Members

        static bool IsAssignableFrom(Type type, Type contract)
        {
            if (contract == type) return false;

            var typeInfo = type.GetTypeInfo();

            bool isInterfaceOrAbstract = type.GetTypeInfo().IsInterface || type.GetTypeInfo().IsAbstract;

            if (isInterfaceOrAbstract) return false;

            return IsAssignableToGenericType(type, contract) || contract.IsAssignableFrom(type);
        }

        static IEnumerable<Type> GetAllTypes()
        {
            var types =
                DependencyContext
                    .Default
                    .CompileLibraries
                    .Where(x => String.IsNullOrEmpty(x.Path))
                    .Select(x => Assembly.Load(new AssemblyName(x.Name)))
                    .SelectMany(x => x.GetTypes());

            return types;
        }

        static bool IsAssignableToGenericType(Type givenType, Type genericType)
        {
            var interfaceTypes = givenType.GetInterfaces();

            foreach (var it in interfaceTypes)
            {
                if (it.GetTypeInfo().IsGenericType && it.GetGenericTypeDefinition() == genericType)
                    return true;
            }

            if (givenType.GetTypeInfo().IsGenericType && givenType.GetGenericTypeDefinition() == genericType)
                return true;

            Type baseType = givenType.GetTypeInfo().BaseType;
            if (baseType == null) return false;

            return IsAssignableToGenericType(baseType, genericType);
        }

        #endregion
    }
}
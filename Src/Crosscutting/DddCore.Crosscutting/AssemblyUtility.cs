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
                    .Where(x => IsNotInterfaceOrAbstract(x, assignType) && IsAssignableFrom(x, assignType));
        }

        #region Private Members

        static bool IsNotInterfaceOrAbstract(Type type, Type contract)
        {
            if (contract == type) return false;

            var typeInfo = type.GetTypeInfo();

            if (typeInfo.IsInterface || typeInfo.IsAbstract) return false;

            return true;
        }

        static bool IsAssignableFrom(Type type, Type contract)
        {
            return type.IsAssignableFromGenericType(contract) || contract.IsAssignableFrom(type);
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

        #endregion
    }
}
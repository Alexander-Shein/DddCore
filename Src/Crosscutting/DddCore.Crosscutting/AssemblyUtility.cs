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
                DependencyContext
                    .Default
                    .CompileLibraries
                    .Where(x => String.IsNullOrEmpty(x.Path))
                    .Select(x => Assembly.Load(new AssemblyName(x.Name)))
                    .SelectMany(x => x.GetTypes())
                    .Where(x => assignType.IsAssignableFrom(x) && x != assignType)
                    .Select(type => (T)Activator.CreateInstance(type));
        }

        public static IEnumerable<Tuple<Type, Type>> GetInterfaceAndInstanceTypes<T>()
        {
            var assignType = typeof(T);

            return
                DependencyContext
                    .Default
                    .CompileLibraries
                    .Where(x => String.IsNullOrEmpty(x.Path))
                    .Select(x => Assembly.Load(new AssemblyName(x.Name)))
                    .SelectMany(x => x.GetTypes())
                    .Where(x => assignType.IsAssignableFrom(x) && x != assignType)
                    .Select(type => Tuple.Create(assignType, type));
        }

        public static IEnumerable<Tuple<Type, Type>> GetInterfaceAndInstanceTypes(Type assignType)
        {
            var isGeneric = assignType.GetTypeInfo().IsGenericType;

            return
                DependencyContext
                    .Default
                    .CompileLibraries
                    .Where(x => String.IsNullOrEmpty(x.Path))
                    .Select(x => Assembly.Load(new AssemblyName(x.Name)))
                    .SelectMany(x => x.GetTypes())
                    .Where(x => (IsAssignableToGenericType(x, assignType) || assignType.IsAssignableFrom(x)) && x != assignType)
                    .Select(type => Tuple.Create(isGeneric ? type.GetTypeInfo().GetInterfaces().FirstOrDefault(i => i.GetGenericTypeDefinition() == assignType) ?? assignType : assignType, type));
        }

        #region Private Members

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
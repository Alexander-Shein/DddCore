using System;
using System.Linq;
using System.Reflection;

namespace DddCore.Crosscutting
{
    internal static class TypeExtensions
    {
        /// <summary>
        /// Type.IsAssignableFrom equivalent for opened generic types
        /// </summary>
        /// <param name="genericType"></param>
        /// <param name="givenType"></param>
        /// <returns></returns>
        public static bool IsAssignableFromGenericType(this Type genericType, Type givenType)
        {
            while (true)
            {
                var interfaceTypes = givenType.GetInterfaces();

                if (interfaceTypes.Any(it => it.GetTypeInfo().IsGenericType && it.GetGenericTypeDefinition() == genericType))
                {
                    return true;
                }

                var givenTypeInfo = givenType.GetTypeInfo();

                if (givenTypeInfo.IsGenericType && givenType.GetGenericTypeDefinition() == genericType)
                    return true;

                var baseType = givenTypeInfo.BaseType;
                if (baseType == null) return false;

                givenType = baseType;
            }
        }
    }
}
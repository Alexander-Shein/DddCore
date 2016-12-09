using System;
using System.Collections.Generic;
using System.Reflection;

namespace DddCore.Crosscutting.Configuration
{
    public static class AssemblyUtility
    {
        public static IEnumerable<T> GetInstances<T>()
        {
            return null;

            //var assignType = typeof(T);
            //
            //return AppDomain.CurrentDomain.GetAssemblies()
            //    //.Where(x => x.FullName.StartsWith("Dal"))
            //    .SelectMany(y => y.GetTypes()
            //        .Where(x => assignType.IsAssignableFrom(x) && x != assignType))
            //        .Select(type => (T)Activator.CreateInstance(type));
        }
    }
}

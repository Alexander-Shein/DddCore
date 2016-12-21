using System.Collections.Generic;
using System.Reflection;
using Microsoft.Extensions.DependencyModel;
using System.Linq;
using System;

namespace DddCore.Crosscutting.Configuration
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
    }
}

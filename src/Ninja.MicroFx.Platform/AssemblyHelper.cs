using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Ninja.MicroFx.Platform
{
    public static class AssemblyHelper
    {
        public static List<T> GetTypes<T>()
        {
            var modules = Assembly.GetEntryAssembly().GetTypes()
                .Where(t => typeof (T).IsAssignableFrom(t) && !t.IsInterface)
                .Select(t=> (T)Activator.CreateInstance(t))
                .ToList();

            return modules;
        }
    }
}
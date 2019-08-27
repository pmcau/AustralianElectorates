using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

static class ReflectionHelpers
{
    public static bool IsCollection(this Type type)
    {
        return type.GetInterfaces()
            .Any(x => x.IsGenericType &&
                      x.GetGenericTypeDefinition() == typeof(ICollection<>));
    }
}
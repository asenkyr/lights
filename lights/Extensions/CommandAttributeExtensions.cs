using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using lights.Attributes;

namespace lights.Extensions
{
    internal static class CommandAttributeExtensions
    {
        public static IEnumerable<MethodInfo> WithAttribute<TAttribute>(this IEnumerable<MethodInfo> methods)
            where TAttribute : Attribute
        {
            return methods.WithAttribute<TAttribute>(attribute => true);
        }

        public static IEnumerable<MethodInfo> WithAttribute<TAttribute>(this IEnumerable<MethodInfo> methods,
            Func<TAttribute, bool> attributeFilter)
            where TAttribute : Attribute
        {
            return methods.Where(method =>
            {
                var attributes = method.GetCustomAttributes(typeof(TAttribute), false);
                if (attributes.Length == 0)
                    return false;

                return attributeFilter((TAttribute)attributes.First());
            });
        }

        public static TAttribute GetAttribute<TAttribute>(this MethodInfo method)
        {
            var attribute = method
                .GetCustomAttributes(typeof(TAttribute), false)
                .First();

            return (TAttribute) attribute;
        }
    }
}
